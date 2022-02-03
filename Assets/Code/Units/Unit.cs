using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;

using Commander2D.UI;
using Commander2D.Units.Skills.Buffs;
using System;

namespace Commander2D.Units {
  public abstract class Unit : MonoBehaviour, IPointerClickHandler {
    /// <summary>
    /// Property <c>stats</c> is the stat block of the unit.
    /// </summary>
    protected UnitStats stats;

    /// <summary>
    /// Property <c>unitID</c> is the unique ID for the unit.
    /// </summary>
    [SerializeField]
    protected UnitID unitID;

    /// <summary>
    /// Property <c>buffs</c> is the set of buffs that are currently
    /// applied to the unit.
    /// </summary>
    protected List<Buff> buffs = new List<Buff>();

    public void ApplyBuff(Buff buff) {
      this.buffs.Add(buff);
    }

    public void OnPointerClick(PointerEventData eventData) {
      InputController.GetInstance().NotifyUnitClicked(this.unitID);
    }

    public void TickBuffs() {
      List<Buff> toRemove = new List<Buff>();

      foreach (Buff buff in this.buffs) {
        bool expended = buff.Tick(this.unitID);

        if (expended) {
          toRemove.Add(buff);
        }
      }

      foreach (Buff removeBuff in toRemove) {
        this.buffs.Remove(removeBuff);
      }
    }

    public void ApplyDamage(int intensity, Skills.Effects.Damage.DamageType damageType) {
      int totalDamage = this.stats.ApplyDamage(intensity, damageType);
      HUDManager.GetInstance().SpawnDamageText(this.unitID, totalDamage, false);
      HUDManager.GetInstance().SetUnitHealth(this.unitID, this.GetModifiedStats().GetMaxHP(), this.GetModifiedStats().GetCurrentHP());
    }

    protected UnitStats GetModifiedStats() {
      UnitStats result = new UnitStats(this.stats);
      foreach (Buff buff in this.buffs) {
        result = buff.ModifyStats(result);
      }

      return result;
    }

    /// <summary>
    /// Method <c>GetInitiative</c> returns the initiative of the
    /// unit.
    /// </summary>
    /// <returns>The unit's initiative.</returns>
    public float GetInitiative() {
      return this.GetModifiedStats().GetInitiative();
    }

    /// <summary>
    /// Method <c>GetHealthPercent</c> returns the current health
    /// percentage of the unit.
    /// </summary>
    /// <returns>The current health percent.</returns>
    public float GetHealthPercent() {
      return (float)this.GetModifiedStats().GetCurrentHP() / (float)this.GetModifiedStats().GetMaxHP();
    }

    /// <summary>
    /// Method <c>GetMovementSpeed</c> returns the movement speed of the
    /// unit.
    /// </summary>
    /// <returns>The movement speed of the unit.</returns>
    public float GetMovementSpeed() {
      return this.GetModifiedStats().GetMovementSpeed();
    }

    /// <summary>
    /// Abstract method <c>GetPortrait</c> returns the units portrait.
    /// </summary>
    /// <returns>The portrait as a <c>Sprite</c>.</returns>
    public abstract Sprite GetPortrait();

    /// <summary>
    /// Method <c>GetUnitID</c> returns this units unique ID.
    /// </summary>
    /// <returns></returns>
    public UnitID GetUnitID() {
      return unitID;
    }

    public float GetBuffTimeMultiplier() {
      return this.stats.GetBuffTimeMultiplier();
    }

    /// <summary>
    /// Method <c>SetUnitID</c> sets the unique ID of this unit.
    /// </summary>
    /// <param name="unitID">The unit ID.</param>
    public void SetUnitID(UnitID unitID) {
      this.unitID = unitID;
    }

    internal void ApplyHeal(int intensity) {
      this.stats.ApplyHeal(intensity);
      HUDManager.GetInstance().SpawnDamageText(this.unitID, intensity, true);
      HUDManager.GetInstance().SetUnitHealth(this.unitID, this.GetModifiedStats().GetMaxHP(), this.GetModifiedStats().GetCurrentHP());
    }
  }
}