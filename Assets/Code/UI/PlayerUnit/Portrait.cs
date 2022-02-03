using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Commander2D.Units;

namespace Commander2D.UI.PlayerUnit {
  /// <summary>
  /// Class <c>Portrait</c> provides control over a player unit UI portrait.
  /// </summary>
  public class Portrait : MonoBehaviour {
    /// <summary>
    /// Property <c>spriteMap</c> maps the Enum value of <c>Species</c> to it's portrait image.
    /// </summary>
    [SerializeField]
    private Sprite[] spriteMap;
    
    /// <summary>
    /// Property <c>portrait</c> is a handle to the actual UI portrait image element.
    /// </summary>
    private Image portrait;

    /// <summary>
    /// Method <c>Awake</c> is called automatically on object initialization.
    /// </summary>
    private void Awake() {
      portrait = gameObject.GetComponent<Image>();
      portrait.enabled = false;
    }
  }
}