using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Commander2D.Units.Skills.Effects;

namespace Commander2D.Units {
  public class UnitStats {
    /// <summary>
    /// Property <c>haste</c> tracks the unit's Haste stat which determines how quickly
    /// they are prepared to fight and how often they can take a turn.
    /// </summary>
    private int haste;

    /// <summary>
    /// Property <c>speed</c> tracks the unit's Speed stat which determines how quickly
    /// they can move.
    /// </summary>
    private int speed;

    /// <summary>
    /// Property <c>tenacity</c> tracks the unit's Tenacity stat which determines how long
    /// they can keep fighting (HP).
    /// </summary>
    private int tenacity;

    /// <summary>
    /// Property <c>mastery</c> tracks the unit's Mastery stat which determines how long
    /// their buffs and debuffs last.
    /// </summary>
    private int mastery;

    /// <summary>
    /// Property <c>currentHP</c> tracks the units current hit point total.
    /// </summary>
    private int currentHP;

    /// <summary>
    /// Property <c>resistance</c> tracks the percentage of damage from each type of damage
    /// the unit can resist. Negative numbers mean those types of skills deal that percent
    /// more damage rather than less.
    /// </summary>
    private Dictionary<SkillEffect.DamageType, int> resistance;

    internal int ApplyDamage(int intensity, SkillEffect.DamageType damageType) {
      float pctDamage = this.resistance[damageType] + 100.0f;
      int totalDamage = Mathf.CeilToInt(((float)intensity * pctDamage) / 100.0f);

      this.currentHP -= totalDamage;

      return totalDamage;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    private UnitStats() {
      this.resistance = new Dictionary<SkillEffect.DamageType, int>();

      foreach (SkillEffect.DamageType damageType in Enum.GetValues(typeof(SkillEffect.DamageType))) {
        this.resistance[damageType] = 0;
      }
    }

    public UnitStats(UnitStats other) {
      this.haste = other.haste;
      this.speed = other.speed;
      this.tenacity = other.tenacity;

      this.currentHP = other.currentHP;

      this.resistance = new Dictionary<SkillEffect.DamageType, int>();
      foreach (KeyValuePair<SkillEffect.DamageType, int> kv in other.resistance) {
        this.resistance.Add(kv.Key, kv.Value);
      }
    }

    public void ModifyResistance(SkillEffect.DamageType damageType, int amount) {
      this.resistance[damageType] += amount;
    }

    /// <summary>
    /// Method <c>GetMaxHP</c> calculates the unit's maximum HP.
    /// </summary>
    /// <returns>The unit's maximum health points.</returns>
    public int GetMaxHP() {
      return tenacity * 2;
    }

    /// <summary>
    /// Method <c>GetCurrentHP</c> returns the unit's current HP.
    /// </summary>
    /// <returns>The unit's current health points.</returns>
    public int GetCurrentHP() {
      return currentHP;
    }

    /// <summary>
    /// Method <c>GetInitiative</c> calculates the unit's initiative.
    /// </summary>
    /// <returns>The unit's initative.</returns>
    public float GetInitiative() {
      if (haste < 5) {
        return 0.0f;
      }

      return 1.0f / (haste - 4);
    }

    public float GetBuffTimeMultiplier() {
      return Mathf.Ceil(Mathf.Pow(this.mastery * .5f, 1.4f));
    }

    internal void ApplyHeal(int intensity) {
      this.currentHP = Mathf.Min(this.GetMaxHP(), this.currentHP + intensity);
    }

    /// <summary>
    /// Method <c>GetMovementSpeed</c> calculates the unit's movement speed.
    /// </summary>
    /// <returns>The unit's movement speed.</returns>
    public float GetMovementSpeed() {
      return (float)(1.0f / (1.0f + Math.Exp(-(0.1f * ((float)speed - 20)))) * 30);
    }

    public class UnitStatsBuilder {
      private readonly UnitStats newUnitStats = new UnitStats();

      public UnitStatsBuilder Haste(int haste) {
        this.newUnitStats.haste = haste;
        return this;
      }

      public UnitStatsBuilder Speed(int speed) {
        this.newUnitStats.speed = speed;
        return this;
      }

      public UnitStatsBuilder Tenacity(int tenacity) {
        this.newUnitStats.tenacity = tenacity;
        return this;
      }

      public UnitStatsBuilder Mastery(int mastery) {
        this.newUnitStats.mastery = mastery;
        return this;
      }

      public UnitStatsBuilder Resistance(int resistance, SkillEffect.DamageType damageType) {
        this.newUnitStats.resistance[damageType] = resistance;
        return this;
      }

      public UnitStats Build() {
        this.newUnitStats.currentHP = this.newUnitStats.GetMaxHP();
        return this.newUnitStats;
      }
    }
  }
}