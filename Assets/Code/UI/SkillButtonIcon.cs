using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Commander2D.UI {
  /// <summary>
  /// Class <c>SkillButtonIcon</c> provides an interface for interacting with the image of a skill button.
  /// </summary>
  public class SkillButtonIcon : MonoBehaviour {
    /// <summary>
    /// Property <c>image</c> is the actual UI image.
    /// </summary>
    private Image image;

    private void Awake() {
      this.image = this.gameObject.GetComponent<Image>();

      Debug.Assert(this.image != null, "The SkillButtonIcon does not have an Image component.");
    }

    /// <summary>
    /// Method <c>SetIcon</c> sets the image of the skill button.
    /// </summary>
    /// <param name="sprite">The image to use for the button.</param>
    public void SetIcon(Sprite sprite) {
      this.image.sprite = sprite;
    }
  }
}