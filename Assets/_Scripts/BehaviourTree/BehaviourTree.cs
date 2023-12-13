using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    public Node rootNode;
    public Node.State treeState = Node.State.Running;
    public List<Node> nodes = new List<Node>();

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
        node.guid = GUID.Generate().ToString();
        
        Undo.RecordObject(this, "Behaviour Tree (CreateNode)");
        nodes.Add(node);

        if (!Application.isPlaying) {
            AssetDatabase.AddObjectToAsset(node, this);
        }
        Undo.RegisterCreatedObjectUndo(node, "BehaviourTree (CreateNode)");
        AssetDatabase.SaveAssets();
        return node;
    }

    public void DeleteNode(Node node) {
        Undo.RecordObject(this, "Behaviour Tree (DeleteNode)");
        nodes.Remove(node);
        //AssetDatabase.RemoveObjectFromAsset(node);
        Undo.DestroyObjectImmediate(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(Node parent, Node child) {
        DecoratorNode decoratorNode = parent as DecoratorNode;
        if (decoratorNode) {
            Undo.RecordObject(decoratorNode, "Behaviour Tree (AddChild)");
            decoratorNode.child = child;
            EditorUtility.SetDirty(decoratorNode);
        }
        
        RootNode rootNode = parent as RootNode;
        if (rootNode) {
            Undo.RecordObject(rootNode, "Behaviour Tree (AddChild)");
            rootNode.child = child;
            EditorUtility.SetDirty(rootNode);
        }
        
        CompositeNode compositeNode = parent as CompositeNode;
        if (compositeNode) {
            Undo.RecordObject(compositeNode, "Behaviour Tree (AddChild)");
            compositeNode.children.Add(child);
            EditorUtility.SetDirty(compositeNode);
        }
    }
    
    public void RemoveChild(Node parent, Node child) {
        DecoratorNode decoratorNode = parent as DecoratorNode;
        if (decoratorNode) {
            Undo.RecordObject(decoratorNode, "Behaviour Tree (RemoveChild)");
            decoratorNode.child = null;
            EditorUtility.SetDirty(decoratorNode);
        }
        
        RootNode rootNode = parent as RootNode;
        if (rootNode) {
            Undo.RecordObject(rootNode, "Behaviour Tree (RemoveChild)");
            rootNode.child = null;
            EditorUtility.SetDirty(rootNode);
        }
        
        CompositeNode compositeNode = parent as CompositeNode;
        if (compositeNode) {
            Undo.RecordObject(compositeNode, "Behaviour Tree (RemoveChild)");
            compositeNode.children.Remove(child);
            EditorUtility.SetDirty(compositeNode);
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
}
