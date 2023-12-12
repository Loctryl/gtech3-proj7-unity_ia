using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeEditor : EditorWindow {
	private BehaviourTreeView treeView;
	private InspectorView inspectorView;

	[MenuItem("Window/Behaviour Tree Editor/Editor")]
	public static void OpenWindow() {
		BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
		wnd.titleContent = new GUIContent("BehaviourTreeEditor");
	}

	[OnOpenAsset]
	public static bool OnOpenAsset(int instanceId, int line) {
		if (Selection.activeObject is BehaviourTree) {
			OpenWindow();
			return true;
		}
		return false;
	}

	public void CreateGUI() {
		// Each editor window contains a root VisualElement object
		VisualElement root = rootVisualElement;

		var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehaviourTreeEditor.uxml");
		visualTree.CloneTree(root);

		var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTreeEditor.uss");
		root.styleSheets.Add(styleSheet);

		treeView = root.Q<BehaviourTreeView>();
		inspectorView = root.Q<InspectorView>();
		treeView.onNodeSelected = OnNodeSelectionChanged;

		OnSelectionChange();
	}

	private void OnSelectionChange() {
		BehaviourTree tree = Selection.activeObject as BehaviourTree;
		if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID())) {
			treeView.PopulateView(tree);
		}
	}

	private void OnNodeSelectionChanged(NodeView node) {
		inspectorView.UpdateSelection(node);
	}
}