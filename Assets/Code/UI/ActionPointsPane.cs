using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Commander2D.UI {
  /// <summary>
  /// Class <c>ActionPointsPane</c> provides ways to interact with the ActionPoints HUD element.
  /// </summary>
  public class ActionPointsPane : MonoBehaviour {
    /// <summary>
    /// Property <c>pipImages</c> provides handles to the individual pipImages.
    /// </summary>
    [SerializeField]
    private Image[] pipImages = new Image[Global.MAX_ACTION_POINTS];

    /// <summary>
    /// Property <c>activePipSprite</c> provides the sprite to display when a pip is available.
    /// </summary>
    [SerializeField]
    private Sprite activePipSprite;

    /// <summary>
    /// Property <c>inactivePipSprite</c> provides the sprite to display when a pip is used.
    /// </summary>
    [SerializeField]
    private Sprite inactivePipSprite;

    /// <summary>
    /// Method <c>Awake</c> is called automatically on object creation.
    /// </summary>
    private void Awake() {
      Debug.Assert(this.activePipSprite != null, "ActionPointsPane is missing the activePipSprite.");
      Debug.Assert(this.inactivePipSprite != null, "ActionPointsPane is missing the inactivePipSprite.");

      foreach (Image pipImage in this.pipImages) {
        Debug.Assert(pipImage != null, "Missing a pip image handle in ActionPointsPane.");
      }
    }

    /// <summary>
    /// Method <c>SetActivePips</c> sets the number of pips that should display as available in the HUD.
    /// </summary>
    /// <param name="activePips">The number of available pips.</param>
    public void SetActivePips(int activePips) {
      foreach (Image pipImage in pipImages) {
        pipImage.sprite = activePipSprite;
      }

      if (activePips < 4) {
        pipImages[0].sprite = inactivePipSprite;
      }

      if (activePips < 3) {
        pipImages[1].sprite = inactivePipSprite;
      }

      if (activePips < 2) {
        pipImages[2].sprite = inactivePipSprite;
      }

      if (activePips < 1) {
        pipImages[3].sprite = inactivePipSprite;
      }
    }
  }
}