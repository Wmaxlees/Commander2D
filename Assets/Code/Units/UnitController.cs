using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Commander2D.Board;
using Commander2D.Units.Enemies;
using Commander2D.Units.Skills.Buffs;
using Commander2D.Units.Skills.Effects;

namespace Commander2D.Units {
  /// <summary>
  /// Class <c>UnitController</c> provides a singleton instance with functions for interacting
  /// with the active units.
  /// </summary>
  public class UnitController : MonoBehaviour {
    [SerializeField]
    private GameObject PyromaniacPrefab;

    [SerializeField]
    private GameObject ChunkerPrefab;

    /// <summary>
    /// Property <c>enemyUnitPrefab</c> is a handle to the enemy prefab to duplicate when
    /// spawning new enemies.
    /// </summary>
    [SerializeField]
    protected GameObject enemyUnitPrefab;

    /// <summary>
    /// Property <c>unts</c> is the list of units in the scene.
    /// </summary>
    protected IDictionary<UnitID, Unit> units = new Dictionary<UnitID, Unit>();

    /// <summary>
    /// Method <c>Awake</c> is called automatically at object creation.
    /// </summary>
    private void Awake() {
      s_UnitController = this;

      Debug.Assert(ChunkerPrefab != null, "Missing handle to Chunker prefab in UnitController.");
      Debug.Assert(PyromaniacPrefab != null, "Missing handle to Pyromaniac prefab in UnitController.");

      Debug.Assert(enemyUnitPrefab != null, "Missing handle to enemyUnitPrefab in UnitController.");
    }

    internal void ApplyDamage(UnitID unitID, int intensity, SkillEffect.DamageType damageType) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to apply damage to.");
        return;
      }

      this.units[unitID].ApplyDamage(intensity, damageType);
    }

    public void LoadTestPlayers() {
      this.LoadTestPlayer1();
      this.LoadTestPlayer2();

      Spawn<BlueSlime>("blue_slime1", new Vector3(-1.0f, 0.8f, 0.0f));
    }

    public void LoadTestPlayer1() {
      GameObject go = Instantiate(this.PyromaniacPrefab, Vector3.zero, Quaternion.identity, GameBoard.GetInstance().transform);
      go.name = "playerunit1";
      PlayerUnit pu = go.GetComponent<PlayerUnit>();
      pu.SetUnitID(UnitID.PlayerUnit1);
      pu.SetStats(new UnitStats.UnitStatsBuilder()
        .Haste(15)
        .Speed(10)
        .Tenacity(8)
        .Mastery(4)
        .Build()
      );
      pu.SetActive(true);
      this.units[UnitID.PlayerUnit1] = pu;
    }

    public void LoadTestPlayer2() {
      GameObject go = Instantiate(this.ChunkerPrefab, Vector3.zero, Quaternion.identity, GameBoard.GetInstance().transform);
      go.name = "playerunit2";
      PlayerUnit pu = go.GetComponent<PlayerUnit>();
      pu.SetUnitID(UnitID.PlayerUnit2);
      pu.SetStats(new UnitStats.UnitStatsBuilder()
        .Haste(15)
        .Speed(10)
        .Tenacity(8)
        .Mastery(4)
        .Build()
      );
      pu.SetActive(true);
      this.units[UnitID.PlayerUnit2] = pu;
    }

    /// <summary>
    /// Method <c>Spawn</c> creates a new enemy unit and puts it on the field in the location
    /// specified.
    /// </summary>
    /// <typeparam name="T">They type of <c>EnemyUnit</c> to spawn.</typeparam>
    /// <param name="name">The name of the unit.</param>
    /// <param name="location">The location to spawn the unit.</param>
    public void Spawn<T>(string name, Vector3 location) where T : EnemyUnit {
      GameObject go = Instantiate(enemyUnitPrefab, location, Quaternion.identity, GameBoard.GetInstance().transform);
      go.name = name;
      EnemyUnitPrefab eup = go.GetComponent<EnemyUnitPrefab>();
      eup.SetEnemyType<T>();

      UnitID newID = this.GetAvailableID();

      eup.SetUnitID(newID);

      this.units.Add(newID, eup.GetEnemyUnit());
    }

    public void MoveUnitTo(UnitID unitID, Vector3 location) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to apply damage to.");
        return;
      }

      PlayerUnit pu = this.units[unitID] as PlayerUnit;
      if (pu) {
        pu.SetMoveTarget(location);
      } else {
        Debug.Log("Trying to move unitID " + unitID + " but it is an enemy unit.");
      }
    }

    /// <summary>
    /// Static property <c>s_UnitController</c> is the singleton instance of <c>UnitController</c>.
    /// </summary>
    private static UnitController s_UnitController;

    /// <summary>
    /// Static method <c>GetInstance()</c> provides a reference to the singleton instance.
    /// </summary>
    /// <returns>The singleton instance.</returns>
    public static UnitController GetInstance() {
      if (s_UnitController == null) {
        s_UnitController = new UnitController();
      }

      return s_UnitController;
    }

    /// <summary>
    /// Method <c>GetUnitMovementSpeed</c> returns the specified unit's movement speed.
    /// </summary>
    /// <param name="unitID">The unit's ID.</param>
    /// <returns>The unit's movement speed.</returns>
    public float GetUnitMovementSpeed(UnitID unitID) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to get movement speed for.");
        return 0.0f;
      }

      return this.units[unitID].GetMovementSpeed();
    }

    /// <summary>
    /// Method <c>GetUnitLocation</c> returns the specified unit's location.
    /// </summary>
    /// <param name="unitID">The unit's ID.</param>
    /// <returns>The unit's location.</returns>
    public Vector3 GetUnitLocation(UnitID unitID) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to get the location of.");
        return Vector3.zero;
      }

      return this.units[unitID].gameObject.transform.position;
    }

    /// <summary>
    /// Method <c>GetUnitInitiatives</c> gets the initiatives for all active units on
    /// the field. The list is unordered.
    /// </summary>
    /// <returns>A list of tuples where the first entry is the initiative and the second
    /// is the unit's id.</returns>
    public List<Tuple<float, UnitID>> GetUnitInitiatives() {
      List<Tuple<float, UnitID>> result = new List<Tuple<float, UnitID>>();

      foreach (KeyValuePair<UnitID, Unit> unit in this.units) {
        result.Add(new Tuple<float, UnitID>(unit.Value.GetInitiative(), unit.Key));
      }

      return result;
    }

    /// <summary>
    /// Method <c>GetUnitIntiative</c> gets a single unit's initiative.
    /// </summary>
    /// <param name="unitID">The unit's ID.</param>
    /// <returns>The unit's initiative.</returns>
    public float GetUnitInitiative(UnitID unitID) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to get initiative of.");
        return 0.0f;
      }

      return this.units[unitID].GetInitiative();
    }

    /// <summary>
    /// Method <c>GetUnitPortrait</c> returns the unit's portrait.
    /// </summary>
    /// <param name="unitID">The unit's ID.</param>
    /// <returns>The unit's portrait.</returns>
    public Sprite GetUnitPortrait(UnitID unitID) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to get the portrait of.");
        return null;
      }

      return null;
    }

    public void ApplyBuff(UnitID unitID, Buff buff) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to apply a buff to.");
        return;
      }

      this.units[unitID].ApplyBuff(buff);
    }

    public void TickBuffs(UnitID unitID) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to tick buffs of.");
        return;
      }

      this.units[unitID].TickBuffs();
    }

    public float GetUnitHealthPercentage(UnitID unitID) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to get the health percentage of.");
        return 0.0f;
      }

      return this.units[unitID].GetHealthPercent();
    }

    public void SendAnimationRequest(UnitID unitID, string animationName) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to send an animation to.");
        return;
      }

      PlayerUnit pu = this.units[unitID] as PlayerUnit;
      if (pu) {
        pu.SendAnimationRequest(animationName);
      } else {
        Debug.Log("Attempting to animate an enemy unit but that isn't implemented: " + unitID);
      }
    }

    public bool IsPlayingAnimation(UnitID unitID, string animationName) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to check an animation of.");
        return false;
      }

      PlayerUnit pu = this.units[unitID] as PlayerUnit;
      if (pu) {
        return pu.IsPlayingAnimation(animationName);
      } else {
        Debug.Log("Attempting to animate an enemy unit but that isn't implemented: " + unitID);
        return false;
      }
    }

    public float GetBuffTimeMultiplier(UnitID unitID) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to get buff time multiplier of.");
        return 100.0f;
      }

      return this.units[unitID].GetBuffTimeMultiplier();
    }

    public void ApplyHeal(UnitID unitID, int intensity) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to apply healing to.");
        return;
      }

      this.units[unitID].ApplyHeal(intensity);
    }

    internal void SetTarget(UnitID unitID, UnitID forcedTarget) {
      if (!this.units.ContainsKey(unitID)) {
        Debug.Log("Do not have unitID " + unitID + " to send an animation to.");
        return;
      }

      EnemyUnit eu = this.units[unitID] as EnemyUnit;
      if (eu) {
        eu.SetTarget(forcedTarget);
      } else { 
        Debug.LogWarning("Cannot apply forced targeting to player unit. What would that even look like? " + unitID);
      }
    }

    // TODO: This needs work
    /// <summary>
    /// Method <c>GetAvailableID</c> returns the next free Enemy UnitID.
    /// </summary>
    /// <returns>An available UnitID</returns>
    private UnitID GetAvailableID() {
      foreach (UnitID unitID in Enum.GetValues(typeof(UnitID))) {
        if (unitID.IsPlayerUnit()) {
          continue;
        }

        if (!this.units.ContainsKey(unitID)) {
          return unitID;
        }
      }

      return UnitID.EnemyUnit6;
    }
  }
}