using UnityEngine;

using Commander2D.Units.Skills.Effects;

namespace Commander2D.Units.Skills.Buffs {
  public class Resistance : Buff {
    private SkillEffect.DamageType damageType;
    private int intensity;

    public Resistance(int duration, SkillEffect.DamageType damageType, int intensity) : base(duration) {
      this.damageType = damageType;
      this.intensity = intensity;
    }

    public override UnitStats ModifyStats(UnitStats stats) {
      UnitStats result = new UnitStats(stats);

      result.ModifyResistance(this.damageType, intensity);

      return result;
    }

    public override bool Tick(UnitID unitID) {
      return UpdateTime();
    }
  }
}