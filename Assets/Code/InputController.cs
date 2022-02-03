using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using Commander2D.Board;
using Commander2D.TurnBased;
using Commander2D.UI;
using Commander2D.Units;

namespace Commander2D {
  /// <summary>
  /// Class <c>InputController</c> provides functionality for handling input from the user.
  /// </summary>
  public class InputController : MonoBehaviour {
    private static InputController s_InputController;

    public static InputController GetInstance() {
      if (InputController.s_InputController == null) {
        InputController.s_InputController = new InputController();
      }

      return InputController.s_InputController;
    }

    public void Awake() {
      InputController.s_InputController = this;
    }

    public delegate void UnitClickHandler(UnitID unitID);

    public List<UnitClickHandler> unitClickHandlers = new List<UnitClickHandler>();

    public void AddNotifyOnUnitClick(UnitClickHandler handler) {
      this.unitClickHandlers.Add(handler);
    }

    public void NotifyUnitClicked(UnitID unitID) {
      foreach (UnitClickHandler handler in this.unitClickHandlers) {
        handler(unitID);
      }
    }

    public void RemoveNotifyOnUnitClick(UnitClickHandler handler) {
      this.unitClickHandlers.Remove(handler);
    }

    /// <summary>
    /// Method <c>HandleSpacePress</c> handles when the user presses the space bar.
    /// </summary>
    /// <param name="context">The context of the event.</param>
    public void HandleSpacePress(InputAction.CallbackContext context) {
      if (!context.started) {
        return;
      }

      if (!TurnBasedController.GetInstance().CurrentActorIsPlayerUnit()) {
        return;
      }

      TurnBasedController.GetInstance().EndCurrentUnitTurn();
    }

    /// <summary>
    /// Method <c>HandleMouseClick</c> handles when the user clicks with the mouse.
    /// </summary>
    /// <param name="context">The context of the event.</param>
    public void HandleMouseClick(InputAction.CallbackContext context) {
      if (GameManager.GetInstance().GetGameState() != GameManager.GameState.Normal) {
        return;
      }

      if (TurnBasedController.GetInstance().IsTurnBasedOn()) {
        GSTurnBasedHandleMouseClick(context);
      } else {
        GSNormalHandleMouseClick(context);
      }
    }

    public void HandleEscapePressed(InputAction.CallbackContext context) {
      switch (GameManager.GetInstance().GetGameState()) {
        case GameManager.GameState.TargetSelect:
          SkillController.GetInstance().ClearSkill();
          break;
      }

      GameManager.GetInstance().SetGameState(GameManager.GameState.Normal);
    }

    /// <summary>
    /// Method <c>GSTurnBasedHandleMouseClick</c> is a helper function that executes the
    /// mouse click logic for when the game state is TurnBased.
    /// </summary>
    /// <param name="context">The context of the mouse click event.</param>
    private void GSTurnBasedHandleMouseClick(InputAction.CallbackContext context) {
      if (context.performed && GameBoard.GetInstance().MouseOnTile()) {
        Vector3 targetLocation = GameBoard.GetInstance().MouseToWorldPosition();

        UnitID currentActorID = TurnBasedController.GetInstance().GetCurrentActorID();
        if (!currentActorID.IsPlayerUnit()) {
          return;
        }

        if (!SelectionController.GetInstance().IsSoloSelected(currentActorID)) {
          return;
        }

        if (TurnBasedController.GetInstance().CanCurrentUnitMoveTo(targetLocation)) {
          UnitController.GetInstance().MoveUnitTo(currentActorID, targetLocation);
          TurnBasedController.GetInstance().CurrentUnitMoved(targetLocation);
        }
      }
    }

    /// <summary>
    /// Method <c>GSNormalHandleMouseClick</c> is a helper function that executes the
    /// mouse click logic for when the game state is not turn based.
    /// </summary>
    /// <param name="context">The context of the mouse click event.</param>
    private void GSNormalHandleMouseClick(InputAction.CallbackContext context) {
      if (context.performed && GameBoard.GetInstance().MouseOnTile()) {
        Vector3 targetLocation = GameBoard.GetInstance().MouseToWorldPosition();
        foreach (UnitID playerUnitID in Enum.GetValues(typeof(UnitID))) {
          if (SelectionController.GetInstance().IsSelected(playerUnitID)) {
            UnitController.GetInstance().MoveUnitTo(playerUnitID, targetLocation);
          }
        }
      }
    }
  }
}