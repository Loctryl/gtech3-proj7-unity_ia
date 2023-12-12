using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeView : UnityEditor.Experimental.GraphView.Node {
	public Action<NodeView> onNodeSelected;
	public Node node;
	public Port input;
	public Port output;

	public NodeView(Node node) {
		this.node = node;
		this.title = node.name;
		this.viewDataKey = node.guid;

		style.left = node.position.x;
		style.top = node.position.y;

		CreateInputPorts();
		CreateOutputPorts();
	}

	private void CreateInputPorts() {
		if (node is ActionNode) {
			input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
		}
		else if (node is CompositeNode) {
			input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
		}
		else if (node is DecoratorNode) {
			input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
		} else if (node is RootNode) {
		}


		if (input != null) {
			input.portName = "";
			inputContainer.Add(input);
		}
	}

	private void CreateOutputPorts() {
		if (node is ActionNode) {
		}
		else if (node is CompositeNode) {
			output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
		}
		else if (node is DecoratorNode) {
			output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
		} else if (node is RootNode) {
			output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
		}
		
		if (output != null) {
			output.portName = "";
			outputContainer.Add(output);
		}
	}
	
	public override void SetPosition(Rect newPos) {
		base.SetPosition(newPos);
		Undo.RecordObject(node, "Behaviour Tree (Set Position)");
		node.position.x = newPos.xMin;
		node.position.y = newPos.yMin;
		EditorUtility.SetDirty(node);
	}

	public override void OnSelected() {
		base.OnSelected();
		if (onNodeSelected != null) {
			onNodeSelected.Invoke(this);
		}
	}
}