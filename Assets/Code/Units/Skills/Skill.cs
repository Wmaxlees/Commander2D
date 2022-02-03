using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;

using Commander2D.Units.Skills.Effects;

namespace Commander2D.Units.Skills {
  /// <summary>
  /// Interface <c>ISkill</c> defines the methods required to define a skill.
  /// </summary>
  [CreateAssetMenu(fileName = "NewSkill", menuName = "Commander2D/Skill", order = 1)]
  public class Skill : ScriptableObject {
    [SerializeField]
    private string skillName;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int cooldown;

    [SerializeField]
    private int actionPointCost;

    [SerializeField]
    private SkillID skillID;

    [SerializeField]
    public SkillGraph effectGraph;

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

    /// <summary>
    /// Method <c>GetEffects</c> provides the effects that are preformed when the skill
    /// is used.
    /// </summary>
    /// <returns>The list of skill effects triggered by the ability.</returns>
    public IList<SkillEffect> GetEffects() {
      return null;
    }

    public SkillID GetSkillID() {
      return this.skillID;
    }
  }

  public enum SkillID {
    None,

    Chunker_BodyCheck,
    Chunker_Headbutt,
    Chunker_Headlock,
    Chunker_OpenHandSlap,
    Chunker_Punch,
    Chunker_ThrowRock,
    Chunker_QuickSnack,

    Pyromaniac_Sear,
    Pyromaniac_Ignite,
    Pyromaniac_ChokingSmoke,
  }
}