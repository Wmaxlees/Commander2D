using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

using Commander2D.Units.Skills.Effects;

namespace Commander2D.UnityGUI {

  // From https://www.youtube.com/watch?v=nKpM98I7PeM&ab_channel=TheKiwiCoder
  public class SkillGraphEditor : EditorWindow {
    SkillGraphView skillGraphView;

    [MenuItem("Window/Commander2D/SkillEditor")]
    public static void ShowExample() {
      SkillGraphEditor wnd = GetWindow<SkillGraphEditor>();
      wnd.titleContent = new GUIContent("SkillGraphEditor");
    }

    public void CreateGUI() {
      // Each editor window contains a root VisualElement object
      VisualElement root = this.rootVisualElement;

      // Import UXML
      var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Code/UnityGUI/SkillEffectEditor.uxml");
      visualTree.CloneTree(root);

      // A stylesheet can be added to a VisualElement.
      // The style will be applied to the VisualElement and all of its children.
      var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Code/UnityGUI/SkillEffectEditor.uss");
      root.styleSheets.Add(styleSheet);

      this.skillGraphView = root.Query<SkillGraphView>();

      this.OnSelectionChange();
    }

    private void OnSelectionChange() {
      SkillGraph graph = Selection.activeObject as SkillGraph;
      if (graph) { 
        this.skillGraphView.PopulateView(graph);
        this.skillGraphView.visible = true;
      } else {
        this.skillGraphView.visible = false;
      }
    }
  }
}