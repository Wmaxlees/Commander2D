using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Commander2D {

  /// <summary>
  /// Enum <c>SettingsIdentifiers</c> provides unique identifiers for each of the
  /// settings contained in the <c>SettingsContainer</c>.
  /// </summary>
  public enum SettingIdentifiers {
    CameraZoomSensitivity,
    CameraSideScrollSensitivity
  }

  /// <summary>
  /// Class <c>SettingsContainer</c> bundles all the user accessable settings into a single class.
  /// </summary>
  public class SettingsContainer : MonoBehaviour {
    /// <summary>
    /// Property <c>cameraZoomSensitivity</c> controls how quickly the camera can zoom in and out.
    /// </summary>
    private float cameraZoomSensitivity = -1000.0f;

    /// <summary>
    /// Property <c>cameraSideScrollSensitivity</c> controls how quickly the camera can move in the
    /// cardinal directions.
    /// </summary>
    private float cameraSideScrollSensitivity = 2.0f;

    /// <summary>
    /// Method <c>GetFloatSetting</c> provides the value of a float setting.
    /// </summary>
    /// <param name="id">The setting identifier.</param>
    /// <returns>The setting value.</returns>
    public float GetFloatSetting(SettingIdentifiers id) {
      switch (id) {
        case SettingIdentifiers.CameraZoomSensitivity:
          return this.cameraZoomSensitivity;
        case SettingIdentifiers.CameraSideScrollSensitivity:
          return this.cameraSideScrollSensitivity;
        default:
          Debug.LogErrorFormat("failed to get float setting for setting id {0}.", id);
          break;
      }

      return 0.0f;
    }
  }
}
