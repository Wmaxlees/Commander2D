//C# Example (LookAtPointEditor.cs)
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

using Commander2D.Units.Skills;

namespace Commander2D.UnityGUI {

  [CustomEditor(typeof(Skill))]
  [CanEditMultipleObjects]
  public class SkillEditor : Editor {
    SerializedProperty skillName;
    SerializedProperty icon;
    SerializedProperty cooldown;
    SerializedProperty actionPointCost;
    SerializedProperty skillID;

    void OnEnable() {
      this.skillName = this.serializedObject.FindProperty("skillName");
      this.icon = this.serializedObject.FindProperty("icon");
      this.cooldown = this.serializedObject.FindProperty("cooldown");
      this.actionPointCost = this.serializedObject.FindProperty("actionPointCost");
      this.skillID = this.serializedObject.FindProperty("skillID");
    }

    public override void OnInspectorGUI() {
      this.serializedObject.Update();
      EditorGUILayout.BeginVertical();
      EditorGUILayout.PropertyField(this.skillName);
      EditorGUILayout.PropertyField(this.icon);
      EditorGUILayout.PropertyField(this.cooldown);
      EditorGUILayout.PropertyField(this.actionPointCost);
      EditorGUILayout.PropertyField(this.skillID);
      EditorGUILayout.EndVertical();

      this.serializedObject.ApplyModifiedProperties();
    }
  }
}