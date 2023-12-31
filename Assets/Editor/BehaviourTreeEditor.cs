using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeEditor : EditorWindow {
	private BehaviourTreeView treeView;
	private InspectorView inspectorView;
	private IMGUIContainer blackBoardView;

	private SerializedObject treeObject;
	private SerializedProperty blackBoardProperty;

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
		blackBoardView = root.Q<IMGUIContainer>();
		blackBoardView.onGUIHandler = () => {
			treeObject.Update();
			EditorGUILayout.PropertyField(blackBoardProperty);
			treeObject.ApplyModifiedProperties();
		};
		
		treeView.onNodeSelected = OnNodeSelectionChanged;
		OnSelectionChange();
	}

	private void OnEnable() {
		EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
		EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
	}

	private void OnDisable() {
		EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;

	}
	
	private void OnPlayModeStateChanged(PlayModeStateChange obj) {
		switch (obj) {
			case PlayModeStateChange.EnteredEditMode:
				OnSelectionChange();
				break;
			case PlayModeStateChange.ExitingEditMode:
				break;
			case PlayModeStateChange.EnteredPlayMode:
				OnSelectionChange();
				break;
			case PlayModeStateChange.ExitingPlayMode:
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
		}
	}


	private void OnSelectionChange() {
		BehaviourTree tree = Selection.activeObject as BehaviourTree;
		if (!tree && Selection.activeGameObject) {
			BehaviourTreeRunner runner = Selection.activeGameObject.GetComponent<BehaviourTreeRunner>();
			if (runner) tree = runner.tree;
		}
		
		if(Application.isPlaying && tree)
			treeView.PopulateView(tree);
		else if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID())) {
			treeView.PopulateView(tree);
		}

		if (tree != null) {
			treeObject = new SerializedObject(tree);
			blackBoardProperty = treeObject.FindProperty("blackBoard");
		}
	}

	private void OnNodeSelectionChanged(NodeView node) {
		inspectorView.UpdateSelection(node);
	}
}