using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

using Commander2D.TurnBased;
using Commander2D.Units.Skills.Effects;
using Commander2D.Units.Skills;
using Commander2D.Units;

namespace Commander2D {
  /// <summary>
  /// Class <c>SkillController</c> provides a singleton instance of a class that handles
  /// all skill execution including things like obtaining targets for effects, etc.
  /// </summary>
  public class SkillController : MonoBehaviour {
    /// <summary>
    /// Static property <c>s_SkillController</c> is the singleton instance of the skill controller.
    /// </summary>
    private static SkillController s_SkillController;

    /// <summary>
    /// Property <c>currentSkill</c> is a handle to the skill that is being executed.
    /// </summary>
    private SkillGraphRunner skillGraphRunner;

    [SerializeField]
    private HitIndicatorLibrary projectilePrefabs;

    /// <summary>
    /// Static method <c>GetInstance</c> returns a handle to the singleton instance of <c>SkillController</c>.
    /// </summary>
    /// <returns>The singleton instance.</returns>
    public static SkillController GetInstance() {
      if (s_SkillController == null) {
        s_SkillController = new SkillController();
      }

      return s_SkillController;
    }

    /// <summary>
    /// Method <c>Awake</c> is called automatically on object creation.
    /// </summary>
    private void Awake() {
      s_SkillController = this;

      Debug.Assert(this.projectilePrefabs != null, "SkillController does not have an attached projectilePrefabs handle.");
    }

    /// <summary>
    /// Method <c>UsePlayerSkill</c> should be called any time a player wants to use a
    /// skill. This initiates the process of resolving all the effects of that skill.
    /// </summary>
    /// <param name="casterID">The unit ID of the caster.</param>
    /// <param name="skill">The skill the unit is casting.</param>
    public void UsePlayerSkill(UnitID casterID, SkillGraph skill) {
      this.skillGraphRunner = ScriptableObject.CreateInstance<SkillGraphRunner>();
      this.skillGraphRunner.SetSkillGraph(casterID, ScriptableObject.Instantiate<SkillGraph>(skill));
    }

    private void Update() {
      if (this.skillGraphRunner) {
        SkillGraphNode.State state = this.skillGraphRunner.Run();

        if (state == SkillGraphNode.State.Failure) {
          Debug.Log("Failed to execute skill!");
        } else if (state == SkillGraphNode.State.Running) {
          return;
        } else if (state == SkillGraphNode.State.Success) {
          TurnBasedController.GetInstance().UseActionPoints(this.skillGraphRunner.GetActionCost());
          this.skillGraphRunner = null;
        }
      } 
    }

    internal void ClearSkill() {
      this.skillGraphRunner = null;
    }
  }
}