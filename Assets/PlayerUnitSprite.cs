using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Commander2D.Board;
using Commander2D.Units;

public class PlayerUnitSprite : MonoBehaviour {
  private ArchetypeID archetype;
  private Direction direction;
  private bool isMoving;

  private Animator animator;

  public void Awake() {
    this.animator = this.gameObject.GetComponent<Animator>();

    Debug.Assert(this.animator != null, "PlayerUnitSprite " + this.name + " is missing an Animator instance reference.");
  }

  /// <summary>
  /// Method <c>SetDirection</c> updates the direction the sprite is facing and updates the sprite to match.
  /// </summary>
  /// <param name="direction">The direction to face.</param>
  public void SetDirection(Direction direction) {
    this.direction = direction;
    this.UpdateAnimation();
  }

  private void UpdateAnimation() {
    string suffix = this.direction.ToLabel();

    if (this.isMoving) {
      this.animator.Play("Run_" + suffix);
    } else {
      this.animator.Play("Still_" + suffix);
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

  public void SendAnimationRequest(string animationName) {
    string animDir = this.GetDirectionalAnimationName(animationName);
    this.animator.Play(animDir, -1, 0);
  }

  public bool IsPlayingAnimation(string animationName) {
    return this.GetDirectionalAnimationName(animationName) == this.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
  }

  private string GetDirectionalAnimationName(string animationName) {
    return animationName + "_" + this.direction.ToLabel();
  }
}
