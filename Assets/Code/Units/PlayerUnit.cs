using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Commander2D.Board;
using Commander2D.UI;
using Commander2D.Units.Archetypes;

namespace Commander2D.Units {
  /// <summary>
  /// Class <c>PlayerUnit</c> provides a representation of a unit controllable by the player.
  /// </summary>
  public class PlayerUnit : Unit {
    /// <summary>
    /// Property <c>archetype</c> tracks the archetype of the player unit.
    /// </summary>
    [SerializeField]
    private Archetype archetype;

    /// <summary>
    /// Property <c>isActive</c> tracks whether the unit is actually active right now (there is
    /// a unit loaded).
    /// </summary>
    private bool isActive; // Is the unit active?

    /// <summary>
    /// Property <c>moveTarget</c> tracks the location the unit is in the process of moving to.
    /// </summary>
    private Vector2 moveTarget;

    private PlayerUnitSprite playerUnitSprite;

    private void Awake() {
      this.playerUnitSprite = this.gameObject.GetComponentInChildren<PlayerUnitSprite>();

      Debug.Assert(this.playerUnitSprite != null, "PlayerUnit " + this.name + " does not have an attached PlayerUnitSprite in one of it's children.");
      Debug.Assert(this.archetype != null, "PlayerUnit " + this.name + " does not have an attached Archetype.");
    }

    private void Start() {
      HUDManager.GetInstance().SetUnitHealth(this.unitID, this.GetModifiedStats().GetMaxHP(), this.GetModifiedStats().GetCurrentHP());
      HUDManager.GetInstance().SetSkillButtons(this.unitID, archetype.GetSkills());
    }

    /// <summary>
    /// Method <c>SetArchetype</c> sets the archetype of the player unit.
    /// </summary>
    /// <param name="a"></param>
    public void SetArchetype(ArchetypeID a) {
      switch (a) {
        case ArchetypeID.Chopper:
          archetype = new Archetype();
          break;

        case ArchetypeID.Chunker:
          archetype = new Archetype();
          break;

        case ArchetypeID.Pyromaniac:
          archetype = new Archetype();
          break;

        default:
          Debug.LogError(string.Format("Attempted to set a player unit's archetype with unknown archetype: {0}", a));
          break;
      }

      
    }

    /// <summary>
    /// Method <c>SetMoveTarget</c> sets the location that the unit should move towards.
    /// </summary>
    /// <param name="target"></param>
    public void SetMoveTarget(Vector3 target) {
      this.moveTarget = new Vector2(target.x, target.y);

      Direction dir = this.GetDirection(this.moveTarget);
      this.playerUnitSprite.SetDirection(dir);
    }

    /// <summary>
    /// Method <c>Update</c> is automatically called once per game loop.
    /// </summary>
    void Update() {
      if (stats != null) {
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        position2D = Vector2.MoveTowards(position2D, this.moveTarget, this.GetModifiedStats().GetMovementSpeed() * Time.deltaTime);
        transform.position = new Vector3(position2D.x, position2D.y, transform.position.z);

        this.playerUnitSprite.SetMoving((position2D - this.moveTarget).SqrMagnitude() > 0.0);
      }
    }

    private Direction GetDirection(Vector2 target) {
      Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
      Vector2 diff = target - position2D;

      if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y)) {
        if (diff.x > 0) {
          return Direction.RIGHT;
        } else {
          return Direction.LEFT;
        }
      } else {
        if (diff.y > 0) {
          return Direction.UP;
        } else {
          return Direction.DOWN;
        }
      }
    }

    /// <summary>
    /// Method <c>SetStats</c> sets the stat block for the unit.
    /// </summary>
    /// <param name="stats">The stats to set.</param>
    public void SetStats(UnitStats stats) {
      this.stats = stats;
      HUDManager.GetInstance().SetUnitHealth(this.unitID, this.GetModifiedStats().GetMaxHP(), this.GetModifiedStats().GetCurrentHP());
    }

    /// <summary>
    /// Method <c>SetActive</c> sets whether the player unit is actually active and populated.
    /// </summary>
    /// <param name="active">Whether the player unit exists.</param>
    public void SetActive(bool active) {
      this.isActive = active;
      this.playerUnitSprite.SetActive(active);
      HUDManager.GetInstance().SetPlayerUnitActive(this.unitID, this.isActive);
    }

    /// <summary>
    /// Method <c>IsActive</c> returns whether the unit is active or not.
    /// </summary>
    /// <returns>Whether the unit is active.</returns>
    public bool IsActive() {
      return isActive;
    }

    // TODO: This is temporary. Remove it when we have a better portrait resolution system.
    /// <summary>
    /// Method <c>GetPortrait</c> returns the sprite for the unit's portrait.
    /// </summary>
    /// <returns>The portrait of the unit.</returns>
    public override Sprite GetPortrait() {
      return null;
    }

    public void SendAnimationRequest(string animationName) {
      this.playerUnitSprite.SendAnimationRequest(animationName);
    }

    public bool IsPlayingAnimation(string animationName) {
      return this.playerUnitSprite.IsPlayingAnimation(animationName);
    }
  }

  /// <summary>
  /// Enum <c>Archetype</c> allows differentiation between player archetypes.
  /// </summary>
  public enum ArchetypeID {
    None,
    Chopper,
    Chunker,
    Pyromaniac
  }
}