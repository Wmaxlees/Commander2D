using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;


using Commander2D.TurnBased;
using Commander2D.UI;
using Commander2D.UI.PlayerUnit;
using Commander2D.Units;

namespace Commander2D {
  /// <summary>
  /// Class <c>SelectionController</c> provides a singleton instance that allows interaction
  /// and tracking of unit selection.
  /// </summary>
  public class SelectionController : MonoBehaviour {
    /// <summary>
    /// Property <c>playerUnitSelected</c> tracks whether each of the player units is selected.
    /// </summary>
    private bool[] playerUnitSelected = new bool[Global.MAX_PLAYER_UNITS];

    // TODO: We should probably be querying the input manager for whether the shift key is pressed.
    /// <summary>
    /// Property <c>actionMap</c> defines an action map that we can use to track input events.
    /// </summary>
    private InputActionMap actionMap = new InputActionMap("HUD");

    /// <summary>
    /// Property <c>shiftKeyDownAction</c> is an action that tracks when the shift key is pressed.
    /// </summary>
    private InputAction shiftKeyDownAction;

    /// <summary>
    /// Method <c>Awake</c> is called when the object is created.
    /// </summary>
    private void Awake() {
      s_SelectionController = this;

      shiftKeyDownAction = actionMap.AddAction("shiftKeyDown");
      shiftKeyDownAction.AddBinding("<Keyboard>/shift");

      actionMap.Enable();
    }

    /// <summary>
    /// Method <c>PlayerUnitSelected</c> notifies the system that the unit was selected.
    /// </summary>
    /// <param name="playerUnitID">The unit's ID.</param>
    public void PlayerUnitSelected(UnitID playerUnitID) {
      bool shiftKeyDown = shiftKeyDownAction.ReadValue<float>() == 1;

      TogglePlayerUnit(playerUnitID, !shiftKeyDown);
    }

    /// <summary>
    /// Method <c>TogglePlayerUnit</c> toggles the selection of a particular unit.
    /// </summary>
    /// <param name="playerUnitID">The unit's ID.</param>
    /// <param name="deselectOthers">Whether the other player units should be deselected.</param>
    private void TogglePlayerUnit(UnitID playerUnitID, bool deselectOthers) {
      if (deselectOthers) {
        DeselectAllPlayerUnits();
      }

      playerUnitSelected[(int)playerUnitID] = !playerUnitSelected[(int)playerUnitID];
      UpdateSelectedUI();
    }

    /// <summary>
    /// Method <c>DeselectAllPlayerUnits</c> deselects all units.
    /// </summary>
    private void DeselectAllPlayerUnits() {
      for (int i = 0; i < Global.MAX_PLAYER_UNITS; ++i) {
        playerUnitSelected[i] = false;
      }
    }

    /// <summary>
    /// Method <c>IsSelected</c> returns whether a unit is currently selected.
    /// </summary>
    /// <param name="playerUnitID">The unit's ID.</param>
    /// <returns><c>true</c> if that unit is selected.</returns>
    public bool IsSelected(UnitID playerUnitID) {
      return playerUnitSelected[(int)playerUnitID];
    }

    /// <summary>
    /// Method <c>UpdateSelectedUI</c> makes a call to the HUD manager to update the UI to match
    /// the current unit selections.
    /// </summary>
    private void UpdateSelectedUI() {
      foreach (UnitID unitID in Enum.GetValues(typeof(UnitID))) {
        if ((int)unitID > Global.MAX_PLAYER_UNITS) {
          break;
        }

        HUDManager.GetInstance().SetPlayerUnitSelectorIndicator(unitID, playerUnitSelected[(int)unitID]);
      }
    }

    /// <summary>
    /// Method <c>IsAnyUnitSelected</c> returns whether there are any selected units.
    /// </summary>
    /// <returns><c>true</c> if at least one unit is selected.</returns>
    public bool IsAnyUnitSelected() {
      for (int i = 0; i < Global.MAX_PLAYER_UNITS; ++i) {
        if (playerUnitSelected[i]) {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Method <c>IsSoloSelected</c> returns whether a particular unit is the only unit selected.
    /// </summary>
    /// <param name="playerUnitID">The unit's ID.</param>
    /// <returns><c>true</c> if the unit is selected and no other units are selected.</returns>
    public bool IsSoloSelected(UnitID playerUnitID) {
      for (int i = 0; i < Global.MAX_PLAYER_UNITS; ++i) {
        if (i != (int)playerUnitID && playerUnitSelected[i]) {
          return false;
        }
      }

      return playerUnitSelected[(int)playerUnitID];
    }

    /// <summary>
    /// Method <c>SoloSelect</c> selects a unit and deselects all other units.
    /// </summary>
    /// <param name="playerUnitID">The unit's ID.</param>
    public void SoloSelect(UnitID playerUnitID) {
      TogglePlayerUnit(playerUnitID, true);
    }

    /// <summary>
    /// Static property <c>s_SelectedController</c> is the singleton instance of <c>SelectionController</c>.
    /// </summary>
    private static SelectionController s_SelectionController;

    /// <summary>
    /// Static method <c>GetInstance</c> returns a handle to the singleton instance of the <c>SelectionController</c>.
    /// </summary>
    /// <returns>A handle to the singleton instance.</returns>
    public static SelectionController GetInstance() {
      if (s_SelectionController == null) {
        s_SelectionController = new SelectionController();
      }

      return s_SelectionController;
    }

    /// <summary>
    /// Method <c>IsCurrentTurnUnitSoloSelected</c> returns whether the unit who is currently up in the turn order
    /// is the only unit that is selected.
    /// </summary>
    /// <returns><c>true</c> if the unit who's turn it is is the only unit selected.</returns>
    public bool IsCurrentTurnUnitSoloSelected() {
      UnitID currentActorID = TurnBasedController.GetInstance().GetCurrentActorID();

      if (!currentActorID.IsPlayerUnit()) {
        return false;
      }

      return IsSoloSelected(currentActorID);
    }

    /// <summary>
    /// Method <c>OnEnable</c> is called automatically when the GameObject is enabled.
    /// </summary>
    void OnEnabled() {
      actionMap.Enable();
    }

    /// <summary>
    /// Method <c>OnDisable</c> is called automatically when the GameObject is disabled.
    /// </summary>
    void OnDisable() {
      actionMap.Disable();
    }
  }
}