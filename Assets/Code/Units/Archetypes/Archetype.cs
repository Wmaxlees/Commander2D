using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

using Commander2D.Units.Skills.Effects;

namespace Commander2D.Units.Archetypes {

  /// <summary>
  /// Interface <c>IArchetype</c> defines an interface for any player unit archetype.
  /// </summary>
  [CreateAssetMenu(fileName = "NewArchetype", menuName = "Commander2D/Archetype", order = 1)]
  public class Archetype : ScriptableObject {
    [SerializeField]
    private SkillGraph[] skills = new SkillGraph[4];

    /// <summary>
    /// Method <c>GetSkills</c> returns the set of skills the given archetype has access to.
    /// </summary>
    /// <returns>A read-only collection of <c>ISkills</c></returns>
    public ReadOnlyCollection<SkillGraph> GetSkills() {
      return Array.AsReadOnly(this.skills);
    }

    // TODO: Add in two(?) passive auras per class.
  }
}