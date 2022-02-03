using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;
using UnityEngine.UI;

using Commander2D.UI.PlayerUnit;
using Commander2D.Units;
using Commander2D.Units.Skills.Effects;

namespace Commander2D.UI {
  /// <summary>
  /// Class <c>HUDManager</c> provides a singleton that allows components in Commander2D to interact with
  /// the HUD.
  /// </summary>
  public class HUDManager : MonoBehaviour {
    /// <summary>
    /// Static property <c>s_HUDManager</c> is the singleton instance of <c>HUDManager</c>.
    /// </summary>
    private static HUDManager s_HUDManager;

    /// <summary>
    /// Static method <c>GetInstance</c> provides a handle to the singleton instance of <c>HUDManager</c>.
    /// </summary>
    /// <returns></returns>
    public static HUDManager GetInstance() {
      if (s_HUDManager == null) {
        s_HUDManager = new HUDManager();
      }

      return s_HUDManager;
    }

    private HUDManager() { }

    /// <summary>
    /// Property <c>turnBasedIndicator</c> provides a handle to the textbox that displays turn based status.
    /// </summary>
    [SerializeField]
    private Text turnBasedIndicator;

    /// <summary>
    /// Property <c>gameStateIndicator</c> provides a handle ot the textbox that displays game state.
    /// </summary>
    [SerializeField]
    private Text gameStateIndicator;

    /// <summary>
    /// Property <c>playerUnitInfoPaneController</c> provides a handle to the player profile HUD element.
    /// </summary>
    private PlayerUnitInfoPaneController playerUnitInfoPaneController;

    /// <summary>
    /// Property <c>pointerController</c> provides a handle for the system that manages the pointer.
    /// </summary>
    [SerializeField]
    private PointerController pointerController;

    /// <summary>
    /// Property <c>SkillButtonPane</c> provides a handle for the skill buttons displayed in the HUD.
    /// </summary>
    [SerializeField]
    private SkillButtonPane skillButtonPane;

    /// <summary>
    /// Property <c>TurnOrderPane</c> provides a handle to the turn order HUD element.
    /// </summary>
    [SerializeField]
    private TurnOrderPane turnOrderPane;

    /// <summary>
    /// Property <c>actionPointsPane</c> provides a handle to the action points HUD element.
    /// </summary>
    [SerializeField]
    private ActionPointsPane actionPointsPane;

    [SerializeField]
    private DamageText damageText;

    /// <summary>
    /// Method <c>Awake</c> is called automatically on object creation.
    /// </summary>
    private void Awake() {
      s_HUDManager = this;

      this.playerUnitInfoPaneController = this.gameObject.GetComponentInChildren<PlayerUnitInfoPaneController>();

      Debug.Assert(this.playerUnitInfoPaneController != null, "HUD does not have a PlayerUnitInfoPaneController object as a child.");
      Debug.Assert(this.pointerController != null, "HUD does not have a defined PointerController attached.");
      Debug.Assert(this.skillButtonPane != null, "HUDController does not contain a reference to the SkillButtonPane.");
      Debug.Assert(this.turnOrderPane != null, "HUDController does not have a handle to the TurnOrderPane.");
      Debug.Assert(this.actionPointsPane != null, "HUDController does not have a handle to the ActionPointsPane.");
      Debug.Assert(this.damageText != null, "HUDController does not have a handle to the DamageText prefab.");
    }

    /// <summary>
    /// Method <c>SetTurnBasedIndicator</c> turns on or off the turn based indicator.
    /// </summary>
    /// <param name="on">Whether the turn based indicator should be on.</param>
    public void SetTurnBasedIndicator(bool on) {
      string indicator;
      if (on) {
        indicator = "Turn-Based: On";
      } else {
        indicator = "Turn-Based: Off";
      }
      turnBasedIndicator.text = indicator;
    }

    /// <summary>
    /// Method <c>SetPlayerUnitSelectorIndicator</c> sets the status of the unit selector indicator for a particular player unit.
    /// </summary>
    /// <param name="unitID">The player unit.</param>
    /// <param name="on">Whether the indicator should be on.</param>
    public void SetPlayerUnitSelectorIndicator(UnitID unitID, bool on) {
      playerUnitInfoPaneController.SetPlayerUnitSelectorIndicator(unitID, on);
    }

    /// <summary>
    /// Method <c>SetUnitHealth</c> sets the HUD health bar fill amount for the particular unit.
    /// </summary>
    /// <param name="unitID">The unit.</param>
    /// <param name="maxHP">The unit's maximum health.</param>
    /// <param name="currentHP">The unit's current health.</param>
    public void SetUnitHealth(UnitID unitID, int maxHP, int currentHP) {
      if (unitID.IsPlayerUnit()) {
        playerUnitInfoPaneController.SetPlayerUnitHealth(unitID, maxHP, currentHP);
      }
      
      this.turnOrderPane.UpdateHealth(unitID, currentHP, maxHP);
    }

    /// <summary>
    /// Method <c>SetPlayerUnitActive</c> sets whether the particular player unit is active.
    /// </summary>
    /// <param name="unitID">The player unit.</param>
    /// <param name="active">Whether the player unit is active.</param>
    public void SetPlayerUnitActive(UnitID unitID, bool active) {
      playerUnitInfoPaneController.SetPlayerUnitActive(unitID, active);
    }

    /// <summary>
    /// Method <c>UpdateGameStateIndicator</c> updates the HUD game state indicator.
    /// </summary>
    /// <param name="gameState">The new game state.</param>
    public void UpdateGameStateIndicator(GameManager.GameState gameState) {
      gameStateIndicator.text = "" + gameState;
    }

    /// <summary>
    /// Method <c>SetPointerOverEnemy</c> notifies the HUD whether the pointer is hovering over an enemy.
    /// </summary>
    /// <param name="isOverEnemy">Whether the pointer is over an enemy.</param>
    public void SetPointerOverEnemy(bool isOverEnemy) {
      this.pointerController.SetPointerOverEnemy(isOverEnemy);
    }

    /// <summary>
    /// Method <c>SetSkillButtons</c> sets the HUD skill buttons for the given playerID.
    /// </summary>
    /// <param name="playerUnitID">The player unit's ID.</param>
    /// <param name="skills">The set of skills for the skill buttons.</param>
    public void SetSkillButtons(UnitID playerUnitID, ReadOnlyCollection<SkillGraph> skills) {
      this.skillButtonPane.SetSkillGroup(playerUnitID, skills);
    }

    /// <summary>
    /// Method <c>SetTurnOrder</c> sets the correct unit order UI turn order pane. 
    /// </summary>
    /// <param name="unitOrder">The order the units should be in.</param>
    public void SetTurnOrder(IList<Tuple<float, UnitID>> unitOrder) {
      this.turnOrderPane.SetTurnOrder(unitOrder);
    }

    /// <summary>
    /// Method <c>SetActivePointsRemaining</c> sets the UI action points.
    /// </summary>
    /// <param name="remainingPips">The points the current actor can still use.</param>
    public void SetActionPointsRemaining(int remainingPips) {
      this.actionPointsPane.SetActivePips(remainingPips);
    }

    public void SpawnDamageText(UnitID unitID, int damage, bool heal) {
      DamageText newDamage = Instantiate<DamageText>(this.damageText, this.gameObject.transform);
      newDamage.SetDamage(damage, heal);
      newDamage.SetCreateTimeNow();
      newDamage.transform.position = UnitController.GetInstance().GetUnitLocation(unitID);
    }
  }
}