using System;
using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Commander2D.UI;
using Commander2D.Units;

namespace Commander2D.TurnBased {
  /// <summary>
  /// Class <c>TurnBasedController</c> defines a singleton instance that allows interaction
  /// with the turn-based system.
  /// </summary>
  public class TurnBasedController : MonoBehaviour {
    /// <summary>
    /// Property <c>turnBasedOn</c> tracks whether turn-based is on.
    /// </summary>
    private bool turnBasedOn;

    /// <summary>
    /// Property <c>turnOrder</c> keeps track of the order units will take their turn.
    /// </summary>
    private List<Tuple<float, UnitID>> turnOrder;

    /// <summary>
    /// Property <c>currentUnitTurn</c> tracks the current turn.
    /// </summary>
    private UnitTurn currentUnitTurn;

    /// <summary>
    /// Method <c>Awake</c> is called automatically when the object is created.
    /// </summary>
    private void Awake() {
      s_TurnBasedController = this;
    }

    /// <summary>
    /// Method <c>SetTurnBasedOn</c> sets the state of turn based on or off.
    /// </summary>
    /// <param name="turnBasedOn">Whether turn based is on or off.</param>
    public void SetTurnBasedOn(bool turnBasedOn) {
      if (this.turnBasedOn == turnBasedOn) {
        return;
      }

      this.turnBasedOn = turnBasedOn;
      HUDManager.GetInstance().SetTurnBasedIndicator(this.turnBasedOn);

      if (this.turnBasedOn) {
        GenerateTurnOrder();
      }
    }

    /// <summary>
    /// Method <c>IsTurnBasedOn</c> returns whether turn based is on or off.
    /// </summary>
    /// <returns>Whether turn based is on.</returns>
    public bool IsTurnBasedOn() {
      return turnBasedOn;
    }

    /// <summary>
    /// Method <c>GenerateTurnOrder</c> creates a brand new turn order. Should
    /// only be called once per turn-based being activated.
    /// </summary>
    private void GenerateTurnOrder() {
      this.turnOrder = UnitController.GetInstance().GetUnitInitiatives();
      StartNewTurn();
    }

    /// <summary>
    /// Method <c>StartNewTurn</c> performs all the actions necessary for the
    /// next turn to begin.
    /// </summary>
    private void StartNewTurn() {
      SortTurnOrder();

      this.currentUnitTurn = new UnitTurn(this.turnOrder[0].Item2);

      UnitController.GetInstance().TickBuffs(this.turnOrder[0].Item2);

      if (this.turnOrder[0].Item2.IsPlayerUnit()) {
        SelectionController.GetInstance().SoloSelect(this.turnOrder[0].Item2);
      }

      HUDManager.GetInstance().SetActionPointsRemaining(this.GetAvailableActionPoints());

      this.UpdateTurnOrderHUD();
    }

    private void UpdateTurnOrderHUD() {
      HUDManager.GetInstance().SetTurnOrder(this.turnOrder);
    }

    /// <summary>
    /// Method <c>GetCurrentActorID</c> returns the <c>UnitID</c> of the currently acting Unit.
    /// </summary>
    /// <returns>The unitID of the unit who is currently taking a turn.</returns>
    public UnitID GetCurrentActorID() {
      Debug.Assert(this.turnOrder.Count > 0, "Attempting to get the first actor in turn order when there is no turn order.");

      return turnOrder[0].Item2;
    }

    public bool CurrentActorIsPlayerUnit() {
      return turnOrder[0].Item2.IsPlayerUnit();
    }

    /// <summary>
    /// Method <c>EndCurrentPlayerTurn</c> attempts to immediately end the current player
    /// unit's turn.
    /// </summary>
    public void EndCurrentUnitTurn() {
      float initiative = turnOrder[0].Item1;
      UnitID unitID = turnOrder[0].Item2;

      turnOrder[0] = new Tuple<float, UnitID>(initiative + UnitController.GetInstance().GetUnitInitiative(unitID), unitID);

      StartNewTurn();
    }

    /// <summary>
    /// Method <c>SortTurnOrder</c> sorts the turn order by initiative.
    /// </summary>
    private void SortTurnOrder() {
      turnOrder.Sort(delegate (Tuple<float, UnitID> t1, Tuple<float, UnitID> t2) {
        return Math.Sign(t1.Item1 - t2.Item1);
      });
    }

    /// <summary>
    /// Method <c>GetAvailableActionPoints</c> returns the number of remaining
    /// action points for the current turn.
    /// </summary>
    /// <returns>The number of remaining action points.</returns>
    public int GetAvailableActionPoints() {
      return Global.MAX_ACTION_POINTS - currentUnitTurn.GetActionPointsUsed();
    }

    /// <summary>
    /// Method <c>UseActionPoints</c> removes the specified number of action points from
    /// the current turn.
    /// </summary>
    /// <param name="actionPoints">The number of action points to remove.</param>
    public void UseActionPoints(int actionPoints) {
      currentUnitTurn.UseActionPoints(actionPoints);

      HUDManager.GetInstance().SetActionPointsRemaining(this.GetAvailableActionPoints());
    }

    /// <summary>
    /// Method <c>CanCurrentUnitMoveTo</c> determines whether the current actor can move
    /// to the specified location given their remaining action points.
    /// </summary>
    /// <param name="location">The target location.</param>
    /// <returns><c>true</c> if the current user can move to the location.</returns>
    public bool CanCurrentUnitMoveTo(Vector3 location) {
      return currentUnitTurn.CanUnitMoveToLocation(location);
    }

    /// <summary>
    /// Method <c>CanCurrentUnitDefaultAttack</c> determines whether the current actor can
    /// move within range of the specified range and default attack a unit on that location
    /// given the remaining action points.
    /// </summary>
    /// <param name="location">The location of the enemy.</param>
    /// <returns><c>true</c> if the current actor can move+default attack at the location.</returns>
    public bool CanCurrentUnitDefaultAttack(Vector3 location) {
      return currentUnitTurn.CanUnitDefaultAttack(location);
    }

    /// <summary>
    /// Method <c>CurrentUnitMoved</c> notifies the turn controller that the current unit moved
    /// and the action points should be updated.
    /// </summary>
    /// <param name="location">The location the unit moved to.</param>
    public void CurrentUnitMoved(Vector3 location) {
      currentUnitTurn.UnitMovedTo(location);
      HUDManager.GetInstance().SetActionPointsRemaining(this.GetAvailableActionPoints());
    }

    public Vector2 GetClosestLocationCanMoveTowards(UnitID targetID, float desiredDistance) {
      Vector3 tl3 = UnitController.GetInstance().GetUnitLocation(targetID);
      Vector2 targetLocation = new Vector2(tl3.x, tl3.y);

      Vector3 cl3 = UnitController.GetInstance().GetUnitLocation(this.turnOrder[0].Item2);
      Vector2 currentLocation = new Vector2(cl3.x, cl3.y);

      Vector2 desiredLocation = Vector2.MoveTowards(targetLocation, currentLocation, desiredDistance);

      float maxDistance = this.currentUnitTurn.GetMaxDistanceForRemainingActionPoints();

      return Vector2.MoveTowards(currentLocation, desiredLocation, maxDistance);
    }

    /// <summary>
    /// Static property <c>s_TurnBasedController</c> is the singleton instance of the TurnBasedController.
    /// </summary>
    private static TurnBasedController s_TurnBasedController;

    /// <summary>
    /// Static method <c>GetInstance</c> returns the singleton instance of TurnBasedController.
    /// </summary>
    /// <returns></returns>
    public static TurnBasedController GetInstance() {
      if (s_TurnBasedController == null) {
        s_TurnBasedController = new TurnBasedController();
      }

      return s_TurnBasedController;
    }
  }
}