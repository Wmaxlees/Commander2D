using Commander2D.Board;
using Commander2D.Units;
using System;
using System.Collections;
using UnityEngine;

namespace Commander2D.TurnBased {
  /// <summary>
  /// Class <c>UnitTurn</c> tracks information about a single unit's turn.
  /// </summary>
  public class UnitTurn {
    /// <summary>
    /// Property <c>actionPointsUsedOnActions</c> tracks the number of action points
    /// used to perform skills.
    /// </summary>
    private int actionPointsUsedOnSkills = 0;

    /// <summary>
    /// Property <c>totalDistanceMoved</c> tracks the total distance moved this turn.
    /// </summary>
    private float totalDistanceMoved = 0.0f;

    /// <summary>
    /// Property <c>unitID</c> tracks the current active unit.
    /// </summary>
    private UnitID unitID;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="unitID">The unit who is taking the turn.</param>
    public UnitTurn(UnitID unitID) {
      this.unitID = unitID;
    }

    /// <summary>
    /// Method <c>UseActionPoints</c> reduces the available action points by the
    /// specified number.
    /// </summary>
    /// <param name="actionPoints">The number of action points to remove.</param>
    public void UseActionPoints(int actionPoints) {
      this.actionPointsUsedOnSkills += actionPoints;
    }

    /// <summary>
    /// Method <c>GetActionPointsUsed</c> returns the total number of action points
    /// that have been used this turn.
    /// </summary>
    /// <returns>The used action points.</returns>
    public int GetActionPointsUsed() {
      return this.actionPointsUsedOnSkills + GetActionPointsForDistance(totalDistanceMoved);
    }

    /// <summary>
    /// Method <c>CanUnitMoveToLocation</c> returns whether the current active user can
    /// move to the target position given the remaining action points.
    /// </summary>
    /// <param name="location">The target location.</param>
    /// <returns><c>true</c> if the unit can move to the target position.</returns>
    public bool CanUnitMoveToLocation(Vector3 location) {
      float distance = Vector3.Magnitude(GameBoard.GetInstance().GetBoardDistance(UnitController.GetInstance().GetUnitLocation(unitID), location));
      int distanceActionPoints = GetActionPointsForDistance(totalDistanceMoved + distance);
      return distanceActionPoints + actionPointsUsedOnSkills <= Global.MAX_ACTION_POINTS;
    }

    /// <summary>
    /// Method <c>CanUnitDefaultAttack</c> returns whether the current active user can
    /// move to the target position and attack the enemy unit there given the 
    /// remaining action points.
    /// </summary>
    /// <param name="location">The target location.</param>
    /// <returns><c>true</c> if the unit can move to the target position and default attack.</returns>
    public bool CanUnitDefaultAttack(Vector3 location) {
      float distance = Vector3.Magnitude(GameBoard.GetInstance().GetBoardDistance(UnitController.GetInstance().GetUnitLocation(unitID), location));
      int distanceActionPoints = GetActionPointsForDistance(totalDistanceMoved + distance);
      return distanceActionPoints + actionPointsUsedOnSkills + Global.DEFAULT_ATTACK_ACTION_POINT_COST <= Global.MAX_ACTION_POINTS;
    }

    /// <summary>
    /// Method <c>UnitMovedTo</c> updates the turn with the new movement the unit did.
    /// </summary>
    /// <param name="location">The new location of the unit.</param>
    public void UnitMovedTo(Vector3 location) {
      float distance = Vector3.Magnitude(GameBoard.GetInstance().GetBoardDistance(UnitController.GetInstance().GetUnitLocation(unitID), location));
      totalDistanceMoved += distance;
    }

    /// <summary>
    /// Method <c>GetActionPointsForDistance</c> returns the number of action points needed
    /// by the current actor to move the specified amount of points.
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    private int GetActionPointsForDistance(float distance) {
      return (int)Math.Ceiling(distance / UnitController.GetInstance().GetUnitMovementSpeed(this.unitID));
    }

    public float GetMaxDistanceForRemainingActionPoints() {
      return UnitController.GetInstance().GetUnitMovementSpeed(this.unitID) * (Global.MAX_ACTION_POINTS - this.GetActionPointsUsed());
    }
  }
}