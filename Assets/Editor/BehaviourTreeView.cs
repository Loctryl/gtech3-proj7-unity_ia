using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine;

public class BehaviourTreeView : GraphView {
	public Action<NodeView> onNodeSelected;

	public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> {
	}

	private BehaviourTree tree;
	public static BehaviourTree b;

	public BehaviourTreeView() {
		Insert(0, new GridBackground());

		this.AddManipulator(new ContentZoomer());
		this.AddManipulator(new ContentDragger());
		this.AddManipulator(new SelectionDragger());
		this.AddManipulator(new RectangleSelector());


		var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTreeEditor.uss");
		styleSheets.Add(styleSheet);

		Undo.undoRedoPerformed += OnUndoRedo;
	}

	private void OnUndoRedo() {
		if (tree != null) {
			PopulateView(tree);
			AssetDatabase.SaveAssets();
		}
	}

	NodeView FindNodeView(Node node) {
		return GetNodeByGuid(node.guid) as NodeView;
	}

	internal void PopulateView(BehaviourTree tree) {
		this.tree = tree;
		b = tree;
		
		graphViewChanged -= OnGraphViewChanged;
		DeleteElements(graphElements);
		graphViewChanged += OnGraphViewChanged;

		if (tree.rootNode == null) {
			tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
			EditorUtility.SetDirty(tree);
			AssetDatabase.SaveAssets();
		}

		tree.nodes.ForEach(n => CreateNodeView(n));

		tree.nodes.ForEach(n => {
			var children = tree.GetChildren(n);
			children.ForEach(child => {
				NodeView parentView = FindNodeView(n);
				NodeView childView = FindNodeView(child);

				Edge edge = parentView.output.ConnectTo(childView.input);
				AddElement(edge);
			});
		});
	}

	public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
		return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node)
			.ToList();
	}

	private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange) {
		if (graphViewChange.elementsToRemove != null) {
			graphViewChange.elementsToRemove.ForEach(elem => {
				NodeView nodeView = elem as NodeView;
				if (nodeView != null) {
					tree.DeleteNode(nodeView.node);
				}

				Edge edge = elem as Edge;
				if (edge != null) {
					NodeView parentView = edge.output.node as NodeView;
					NodeView childView = edge.input.node as NodeView;
					tree.RemoveChild(parentView.node, childView.node);
				}
			});
		}

		if (graphViewChange.edgesToCreate != null) {
			graphViewChange.edgesToCreate.ForEach(edge => {
				NodeView parentView = edge.output.node as NodeView;
				NodeView childView = edge.input.node as NodeView;
				tree.AddChild(parentView.node, childView.node);
			});
		}

		if (graphViewChange.movedElements != null)
			nodes.ForEach((n) => {
				NodeView view = n as NodeView;
				view.SortChildren();
			});
		return graphViewChange;
	}

	public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
		//base.BuildContextualMenu(evt);
		var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
		foreach (var type in types) {
			evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
		}

		types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
		foreach (var type in types) {
			evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
		}

		types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
		foreach (var type in types) {
			evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
		}
	}

	void CreateNode(System.Type type) {
		Node node = tree.CreateNode(type);
		CreateNodeView(node);
	}

	void CreateNodeView(Node node) {
		NodeView nodeView = new NodeView(node);
		nodeView.onNodeSelected = onNodeSelected;
		AddElement(nodeView);
	}
}