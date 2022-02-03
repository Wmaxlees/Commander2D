using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

using Commander2D.Board;
using Commander2D.Units;

public class PlayerUnitBody : MonoBehaviour {
  private ArchetypeID archetype;
  private Direction direction;
  private bool isMoving;

  private Animator animator;

  public void Awake() {
    this.animator = this.gameObject.GetComponent<Animator>();

    Debug.Assert(this.animator != null, "PlayerUnitBody is missing an Animator instance reference.");
  }

  /// <summary>
  /// Method <c>SetDirection</c> updates the direction the body is facing and updates the sprite to match.
  /// </summary>
  /// <param name="direction">The direction to face.</param>
  public void SetDirection(Direction direction) {
    this.direction = direction;
    this.UpdateAnimation();
  }

  /// <summary>
  /// Method <c>SetArchetype</c> sets the archetype of the body and updates the sprite to match.
  /// </summary>
  /// <param name="species">The species.</param>
  public void SetArchetype(ArchetypeID archetype) {
    // this.spriteLibrary.spriteLibraryAsset = this.animationLibrary.GetSpriteLibraryAsset(archetype);
    this.archetype = archetype;
    this.UpdateAnimation();
  }

  private void UpdateAnimation() {
    string suffix = this.archetype.ToLabel() + "_" + this.direction.ToLabel();

    if (this.isMoving) {
      this.animator.Play("Run_" + suffix);
    } else {
      Debug.Log(suffix);
      this.animator.Play("Idle_" + suffix);
    }
  }

  public void SetMoving(bool isMoving) {
    if (this.isMoving != isMoving) {
      this.isMoving = isMoving;
      this.UpdateAnimation();
    }
  }

  public void SetActive(bool active) {
    this.GetComponent<SpriteRenderer>().enabled = active;
  }
}
