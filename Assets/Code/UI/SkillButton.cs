using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Commander2D.Units.Skills.Effects;
using Commander2D.Units;
using Commander2D.TurnBased;

namespace Commander2D.UI {
  /// <summary>
  /// Class <c>SkillButton</c> provides a controller for skill buttons.
  /// </summary>
  public class SkillButton : MonoBehaviour, IPointerClickHandler {
    /// <summary>
    /// Property <c>skillButtonIcon</c> provides a handle to the icon.
    /// </summary>
    private SkillButtonIcon skillButtonIcon;

    /// <summary>
    /// Property <c>playerUnitID</c> tracks which player unit this skill corresponds to.
    /// </summary>
    private UnitID playerUnitID;

    /// <summary>
    /// Property <c>skill</c> tracks the skill that is triggered by the button.
    /// </summary>
    private SkillGraph skill;

    /// <summary>
    /// Method <c>Awake</c> is called automatically on object creation.
    /// </summary>
    private void Awake() {
      skillButtonIcon = gameObject.GetComponentInChildren<SkillButtonIcon>();

      Debug.Assert(this.skillButtonIcon != null, "SkillButton missing child skillButtonIcon.");
    }

    /// <summary>
    /// Method <c>SetSkill</c> sets the skill the button triggers.
    /// </summary>
    /// <param name="playerUnitID">The player ID of the player who would use the skill.</param>
    /// <param name="skill">The skill that would be triggered.</param>
    public void SetSkill(UnitID playerUnitID, SkillGraph skill) {
      this.playerUnitID = playerUnitID;
      Debug.Log("Setting player ID: " + this.playerUnitID);
      Debug.Log(skill);
      skillButtonIcon.SetIcon(skill.GetIcon());
      this.skill = skill;
    }

    /// <summary>
    /// Method <c>OnPointerClick</c> is automatically called when the user clicks the skill button.
    /// </summary>
    /// <param name="pointerEventData">Information about the mouse click.</param>
    public void OnPointerClick(PointerEventData pointerEventData) {
      if (TurnBasedController.GetInstance().IsTurnBasedOn()) {
        UnitID unitID = TurnBasedController.GetInstance().GetCurrentActorID();

        if (unitID != this.playerUnitID) {
          return;
        }

        if (TurnBasedController.GetInstance().GetAvailableActionPoints() < skill.GetActionCost()) {
          return;
        }
      }

      SkillController.GetInstance().UsePlayerSkill(this.playerUnitID, skill);
    }
  }
}