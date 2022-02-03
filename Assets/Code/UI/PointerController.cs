using Commander2D.Board;
using Commander2D.TurnBased;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander2D.UI {
  /// <summary>
  /// Class <c>CursorController</c> provides ways of interacting with the cursor.
  /// </summary>
  public class PointerController : MonoBehaviour {
    /// <summary>
    /// Property <c>defaultPointer</c> provides the default pointer texture.
    /// </summary>
    [SerializeField]
    private Texture2D defaultPointer;

    /// <summary>
    /// Property <c>targetPointer</c> provides the pointer for targeting an enemy.
    /// </summary>
    [SerializeField]
    private Texture2D targetPointer;
    /// <summary>
    /// Property <c>cannotTargetPointer</c> provides the pointer for when an enemy cannot be targeted.
    /// </summary>
    [SerializeField]
    private Texture2D cannotTargetPointer;

    /// <summary>
    /// Property <c>movePointer</c> provides the pointer for moving to the mouse location.
    /// </summary>
    [SerializeField]
    private Texture2D movePointer;

    /// <summary>
    /// Property <c>cannotMovePointer</c> provides the pointer for when the unit cannot move to the mouse location.
    /// </summary>
    [SerializeField]
    private Texture2D cannotMovePointer;

    /// <summary>
    /// Property <c>isOverEnemy</c> tracks whether the pointer is hovering over an enemy.
    /// </summary>
    private bool isOverEnemy;

    /// <summary>
    /// Method <c>Awake</c> is called automatically on object creation.
    /// </summary>
    private void Awake() {
      Debug.Assert(defaultPointer != null, "No defaultPointer texture assigned to PointerController.");
      Debug.Assert(targetPointer != null, "No targetPointer texture assigned to PointerController.");
      Debug.Assert(cannotTargetPointer != null, "No cannotTargetPointer texture assigned to PointerController.");
      Debug.Assert(movePointer != null, "No movePointer texture assigned to PointerController.");
      Debug.Assert(cannotTargetPointer != null, "No cannotTargetPointer texture assigned to PointerController.");
    }

    private void Start() {
      Cursor.SetCursor(this.defaultPointer, Vector2.zero, CursorMode.Auto);
    }

    public void SetPointerOverEnemy(bool isOverEnemy) {
      this.isOverEnemy = isOverEnemy;
    }

    private void Update() {
      if (!SelectionController.GetInstance().IsCurrentTurnUnitSoloSelected()) {
        Cursor.SetCursor(this.defaultPointer, Vector2.zero, CursorMode.Auto);
        return;
      }

      switch (GameManager.GetInstance().GetGameState()) {
        case GameManager.GameState.TargetSelect:
          if (this.isOverEnemy) {
            Cursor.SetCursor(this.targetPointer, Vector2.zero, CursorMode.Auto);
          } else {
            Cursor.SetCursor(this.cannotTargetPointer, Vector2.zero, CursorMode.Auto);
          }
          break;

        case GameManager.GameState.Normal:
          Vector3 mouseWorldPosition = GameBoard.GetInstance().MouseToWorldPosition();
          if (this.isOverEnemy) {
            if (TurnBasedController.GetInstance().CanCurrentUnitDefaultAttack(mouseWorldPosition)) {
              Cursor.SetCursor(this.targetPointer, Vector2.zero, CursorMode.Auto);
            } else {
              Cursor.SetCursor(this.cannotTargetPointer, Vector2.zero, CursorMode.Auto);
            }
          } else if (GameBoard.GetInstance().MouseOnTile()) {
            if (TurnBasedController.GetInstance().CanCurrentUnitMoveTo(mouseWorldPosition)) {
              Cursor.SetCursor(this.movePointer, Vector2.zero, CursorMode.Auto);
            } else {
              Cursor.SetCursor(this.cannotMovePointer, Vector2.zero, CursorMode.Auto);
            }
          } else {
            Cursor.SetCursor(this.defaultPointer, Vector2.zero, CursorMode.Auto);
          }
          break;

        default:
          Cursor.SetCursor(this.defaultPointer, Vector2.zero, CursorMode.Auto);
          break;
      }
    }
  }
}