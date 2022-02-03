using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

using Commander2D.Units;
using Commander2D.Units.Skills.Effects;

namespace Commander2D.UI {
  /// <summary>
  /// Class <c>SkillButtonGroup</c> wraps a group of skill buttons.
  /// </summary>
  public class SkillButtonGroup : MonoBehaviour {
    /// <summary>
    /// Property <c>skillButtons</c> contains handles to all the skill buttons in the button group.
    /// </summary>
    private SkillButton[] skillButtons = new SkillButton[4];

    /// <summary>
    /// Method <c>Awake</c> is called automatically on object creation.
    /// </summary>
    private void Awake() {
      skillButtons = gameObject.GetComponentsInChildren<SkillButton>();
    }

    /// <summary>
    /// Method <c>SetSkills</c> sets all the skills for the skill button group.
    /// </summary>
    /// <param name="playerUnitID">The player ID linked to the skills.</param>
    /// <param name="skills">The set of skills for the button group.</param>
    public void SetSkills(UnitID playerUnitID, ReadOnlyCollection<SkillGraph> skills) {
      for (int i = 0; i < 4; ++i) {
        skillButtons[i].SetSkill(playerUnitID, skills[i]);
      }
    }
  }
}