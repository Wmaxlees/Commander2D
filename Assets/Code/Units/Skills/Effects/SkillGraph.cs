using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

namespace Commander2D.Units.Skills.Effects {
  [CreateAssetMenu(fileName = "NewSkillGraph", menuName = "Commander2D/SkillGraph", order = 1)]
  public class SkillGraph : ScriptableObject {
    public Start rootNode;

    public List<SkillGraphNode> nodes = new List<SkillGraphNode>();

    [SerializeField]
    private string skillName;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int cooldown;

    [SerializeField]
    private int actionPointCost;

    private UnitID casterID;

    /// <summary>
    /// Method <c>GetName</c> provides the name of the skill.
    /// </summary>
    /// <returns>The human readable name of the skill.</returns>
    public string GetName() {
      return this.skillName;
    }

    /// <summary>
    /// Method <c>GetIcon</c> provides the icon of the skill.
    /// </summary>
    /// <returns>The icon of the skill.</returns>
    public Sprite GetIcon() {
      return this.icon;
    }

    /// <summary>
    /// Method <c>GetCooldown</c> provides the number of turns the skill will be
    /// on cooldown.
    /// </summary>
    /// <returns>The skill's cooldown.</returns>
    public int GetCooldown() {
      return this.cooldown;
    }

    /// <summary>
    /// Method <c>GetActionCost</c> provides the number of action points
    /// the skill needs to be executed
    /// </summary>
    /// <returns>The required action points.</returns>
    public int GetActionCost() {
      return this.actionPointCost;
    }

    public SkillGraphNode CreateRootNode() {
      this.rootNode = ScriptableObject.CreateInstance<Start>();
      this.rootNode.name = "Start";
      this.rootNode.guid = GUID.Generate().ToString();

      AssetDatabase.AddObjectToAsset(this.rootNode, this);
      AssetDatabase.SaveAssets();

      return this.rootNode;
    }

    public SkillGraphNode CreateNode(System.Type type) {
      SkillGraphNode node = ScriptableObject.CreateInstance(type) as SkillGraphNode;
      node.name = type.Name;
      node.guid = GUID.Generate().ToString();
      this.nodes.Add(node);

      AssetDatabase.AddObjectToAsset(node, this);
      AssetDatabase.SaveAssets();

      return node;
    }

    public void DeleteNode(SkillGraphNode node) {
      this.nodes.Remove(node);

      AssetDatabase.RemoveObjectFromAsset(node);
      AssetDatabase.SaveAssets();
    }

    public void AddChild(SkillGraphNode parent, SkillGraphNode child) {
      parent.AddChild(child);
    }

    internal SkillGraphNode GetSecondTargetProvider(VisualEffect node) {
      return node.GetSecondTargetProvider();
    }

    public void RemoveChild(SkillGraphNode parent, SkillGraphNode child) {
      parent.RemoveChild(child);
    }

    public List<SkillGraphNode> GetChildren(SkillGraphNode parent) {
      return parent.GetChildren();
    }

    public void SetTargetProvider(SkillEffect receiver, InputNode provider) {
      receiver.SetTarget(provider);
    }
    public void SetTargetProvider(VisualEffect receiver, InputNode provider) {
      receiver.SetTarget(provider);
    }

    public SkillGraphNode GetTargetProvider(SkillEffect node) {
      return node.GetTargetProvider();
    }

    public SkillGraphNode GetTargetProvider(VisualEffect node) {
      return node.GetTargetProvider();
    }

    internal void SetSecondTargetProvider(VisualEffect receiver, InputNode provider) {
      receiver.SetSecondTarget(provider);
    }
  }
}