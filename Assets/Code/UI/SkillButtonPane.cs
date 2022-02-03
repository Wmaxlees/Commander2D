using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

using Commander2D.Units;
using Commander2D.Units.Skills.Effects;

namespace Commander2D.UI {
  /// <summary>
  /// Class <c>SkillButtonPane</c> provides a handle for the whole set of skill buttons.
  /// </summary>
  public class SkillButtonPane : MonoBehaviour {
    /// <summary>
    /// Property <c>skillButtonGroups</c> provides handles for all the skill button groups in the UI.
    /// </summary>
    private SkillButtonGroup[] skillButtonGroups = new SkillButtonGroup[Global.MAX_PLAYER_UNITS];

    /// <summary>
    /// Method <c>Awake</c> is called automatically on object creation.
    /// </summary>
    void Awake() {
      this.skillButtonGroups = this.gameObject.GetComponentsInChildren<SkillButtonGroup>();

      Debug.Assert(this.skillButtonGroups.Length == Global.MAX_PLAYER_UNITS, "Only found " + this.skillButtonGroups.Length + " SkillButtonGroups for the skill pane.");

      for (int i = 0; i < Global.MAX_PLAYER_UNITS; ++i) {
        Debug.Assert(this.skillButtonGroups[i] != null, "Could not attach SkillButtonGroup " + i + " to SkillButtonPane.");
      }
    }

    /// <summary>
    /// Method <c>SetSkillGroup</c> provides the set of skills for a player unit.
    /// </summary>
    /// <param name="unitID">The player unit to set.</param>
    /// <param name="skills">The skills.</param>
    public void SetSkillGroup(UnitID unitID, ReadOnlyCollection<SkillGraph> skills) {
      this.skillButtonGroups[(int)unitID].SetSkills(unitID, skills);
    }
  }
}