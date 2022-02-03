using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

using Commander2D.Board;

namespace Commander2D.Units.Skills.Effects {

  /// <summary>
  /// Class <c>SkillEffect</c> is the abstract base class for all things that
  /// are triggered by skills that have effects on the field.
  /// </summary>
  public abstract class SkillEffect : SkillGraphNode {
    /// <summary>
    /// Property <c>intensity</c> has different meanings for different effects.
    /// </summary>
    [SerializeField]
    protected int intensity;

    /// <summary>
    /// Property <c>duration</c> has different meanings for different effects.
    /// </summary>
    [SerializeField]
    protected int duration;

    /// <summary>
    /// Property <c>targetType</c> defines what can be effected by the skill effect.
    /// </summary>
    [SerializeField]
    protected TargetType targetType;

    /// <summary>
    /// Property <c>damageType</c> defines what type of damage the skill applies (if any).
    /// </summary>
    [SerializeField]
    protected DamageType damageType;

    /// <summary>
    /// Property <c>casterID</c> is the <c>UnitID</c> of the unit that triggered the skill effect.
    /// </summary>
    protected UnitID casterID;

    /// <summary>
    /// Property <c>unitTarget</c> is the unit the skill effect will apply to. If this
    /// is set, property <c>locationTarget</c> should not be set.
    /// </summary>
    protected UnitID unitTarget;
    /// <summary>
    /// Property <c>locationTarget</c> is the location the skill effect will apply to. If
    /// this is set, property <c>unitTarget</c> should not be set.
    /// </summary>
    protected Vector3 locationTarget;

    protected VisualEffect preHitVisualEffect;
    protected VisualEffect postHitVisualEffect;

    [SerializeField]
    protected InputNode targetProvider;

    /// <summary>
    /// Enum <c>TargetType</c> defines the different types of targets a skill effect can have.
    /// </summary>
    public enum TargetType {
      Unknown,
      Self,
      Unit,
      Ground
    }

    /// <summary>
    /// Enum <c>DamageType</c> defines the types of damage that a skill can apply.
    /// </summary>
    public enum DamageType {
      None,
      Physical,
      Fire,
    }

    /// <summary>
    /// Method <c>GetTargetType</c> returns this <c>SkillEffect</c>s target type.
    /// </summary>
    /// <returns>The target type of this <c>SkillEffect</c></returns>
    public TargetType GetTargetType() {
      return targetType;
    }

    /// <summary>
    /// Method <c>ExecuteSkill</c> applies the effect to the supplied target.
    /// </summary>
    public virtual IEnumerator ExecuteSkill() {
      yield return null;
    }

    public void SetTarget(InputNode targetProvider) {
      this.targetProvider = targetProvider;
    }

    public SkillGraphNode GetTargetProvider() {
      return this.targetProvider;
    }

    /// <summary>
    /// Method <c>CanCastOn</c> determines whether the <c>targetLocation</c> can be the
    /// target of this <c>SkillEffect</c>.
    /// </summary>
    /// <param name="targetLocation">The location in question.</param>
    /// <returns><c>true</c> if the location can be targeted, <c>false</c> otherwise.</returns>
    public bool CanCastOn(Vector3 targetLocation) {
      throw new System.NotImplementedException();
    }

    /// <summary>
    /// Method <c>SetCasterID</c> sets the <c>UnitID</c> casting this effect.
    /// </summary>
    /// <param name="casterID">The <c>UnitID</c> casting this effect.</param>
    public void SetCasterID(UnitID casterID) {
      this.casterID = casterID;
    }

    /// <summary>
    /// Method <c>SetTarget</c> sets the target to a unit.
    /// </summary>
    /// <param name="target">The <c>UnitID</c> to target.</param>
    public void SetTarget(UnitID target) {
      this.unitTarget = target;
    }

    /// <summary>
    /// Method <c>SetTarget</c> sets the target to a location.
    /// </summary>
    /// <param name="target">The location to target.</param>
    public void SetTarget(Vector3 target) {
      this.locationTarget = target;
    }
  }
}