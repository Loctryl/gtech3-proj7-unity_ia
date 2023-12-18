using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    public Node rootNode;
    public Node.State treeState = Node.State.Running;
    public List<Node> nodes = new List<Node>();
    public BlackBoard blackBoard = new BlackBoard();

    public Node.State Update()
    {
        if (rootNode.state == Node.State.Running)
        {
            treeState = rootNode.Update();
        }
        return treeState;
    }

    public Node CreateNode(System.Type type) {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
#if UNITY_EDITOR
        node.guid = UnityEditor.GUID.Generate().ToString();
        
        UnityEditor.Undo.RecordObject(this, "Behaviour Tree (CreateNode)");
        nodes.Add(node);

        if (!Application.isPlaying) {
            UnityEditor.AssetDatabase.AddObjectToAsset(node, this);
        }
        UnityEditor.Undo.RegisterCreatedObjectUndo(node, "BehaviourTree (CreateNode)");
        UnityEditor.AssetDatabase.SaveAssets();
#endif
        return node;
    }

    public void DeleteNode(Node node) {
#if UNITY_EDITOR
        UnityEditor.Undo.RecordObject(this, "Behaviour Tree (DeleteNode)");
        nodes.Remove(node);
        //AssetDatabase.RemoveObjectFromAsset(node);
        UnityEditor.Undo.DestroyObjectImmediate(node);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    public void AddChild(Node parent, Node child) {
        DecoratorNode decoratorNode = parent as DecoratorNode;
        if (decoratorNode) {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(decoratorNode, "Behaviour Tree (AddChild)");
            decoratorNode.child = child;
            UnityEditor.EditorUtility.SetDirty(decoratorNode);
#endif
        }
        
        RootNode rootNode = parent as RootNode;
        if (rootNode) {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(rootNode, "Behaviour Tree (AddChild)");
            rootNode.child = child;
            UnityEditor.EditorUtility.SetDirty(rootNode);
#endif
        }
        
        CompositeNode compositeNode = parent as CompositeNode;
        if (compositeNode) {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(compositeNode, "Behaviour Tree (AddChild)");
            compositeNode.children.Add(child);
            UnityEditor.EditorUtility.SetDirty(compositeNode);
#endif
        }
    }
    
    public void RemoveChild(Node parent, Node child) {
        DecoratorNode decoratorNode = parent as DecoratorNode;
        if (decoratorNode) {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(decoratorNode, "Behaviour Tree (RemoveChild)");
            decoratorNode.child = null;
            UnityEditor.EditorUtility.SetDirty(decoratorNode);
#endif
        }
        
        RootNode rootNode = parent as RootNode;
        if (rootNode) {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(rootNode, "Behaviour Tree (RemoveChild)");
            rootNode.child = null;
            UnityEditor.EditorUtility.SetDirty(rootNode);
#endif
        }
        
        CompositeNode compositeNode = parent as CompositeNode;
        if (compositeNode) {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(compositeNode, "Behaviour Tree (RemoveChild)");
            compositeNode.children.Remove(child);
            UnityEditor.EditorUtility.SetDirty(compositeNode);
#endif
        }
    }
    
    public List<Node> GetChildren(Node parent) {
        List<Node> children = new List<Node>();
        
        DecoratorNode decoratorNode = parent as DecoratorNode;
        if (decoratorNode && decoratorNode.child != null) {
            children.Add(decoratorNode.child);
        }
        
        RootNode rootNode = parent as RootNode;
        if (rootNode && rootNode.child != null) {
            children.Add(rootNode.child);
        }
        
        CompositeNode compositeNode = parent as CompositeNode;
        return compositeNode ? compositeNode.children : children;
    }

    public void Traverse(Node node, System.Action<Node> visiter) {
        if (node) {
            visiter.Invoke(node);
            var children = GetChildren(node);
            children.ForEach((n) => Traverse(n,visiter));
        }
    }

    public BehaviourTree Clone() {
        BehaviourTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        tree.nodes = new List<Node>();
        Traverse(tree.rootNode, (n) => {
            tree.nodes.Add(n);
        });
        return tree;
    }

    public void Bind() {
        Traverse(rootNode, node => {
            node.blackBoard = blackBoard;
        });
    }
}
