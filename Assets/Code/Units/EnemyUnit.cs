using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using Commander2D.Board;
using Commander2D.TurnBased;
using Commander2D.UI;
using System;

namespace Commander2D.Units {
  /// <summary>
  /// Class <c>EnemyUnit</c> provides an abstract class for all enemies seen in
  /// the game.
  /// </summary>
  public abstract class EnemyUnit : Unit {
    /// <summary>
    /// Property <c>spriteRenderer</c> provides a handle to the enemy's sprite.
    /// </summary>
    protected SpriteRenderer spriteRenderer;

    protected UnitID targetID;
    protected Vector2 targetLocation;
    protected bool isMoving = false;

    /// <summary>
    /// Method <c>Start</c> is called automatically when the scene starts.
    /// </summary>
    private void Start() {
      spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

      HUDManager.GetInstance().SetUnitHealth(this.unitID, this.GetModifiedStats().GetMaxHP(), this.GetModifiedStats().GetCurrentHP());
    }

    protected virtual void Update() {
      if (stats != null) {
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        position2D = Vector2.MoveTowards(position2D, this.targetLocation, this.GetModifiedStats().GetMovementSpeed() * Time.deltaTime);
        transform.position = new Vector3(position2D.x, position2D.y, transform.position.z);
      }
    }

    /// <summary>
    /// Method <c>GetSprite</c> returns the field Sprite for the enemy.
    /// </summary>
    /// <returns>The field sprite.</returns>
    public abstract Sprite GetSprite();

    protected bool isMyTurn() {
      return this.unitID == TurnBasedController.GetInstance().GetCurrentActorID();
    }

    internal void SetTarget(UnitID forcedTarget) {
      this.targetID = forcedTarget;
    }
  }
}