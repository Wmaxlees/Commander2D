using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Commander2D {
  /// <summary>
  /// Class <c>CameraController</c> provides functionality for interacting with the camera.
  /// </summary>
  public class CameraController : MonoBehaviour {
    /// <summary>
    /// Property <c>settingsContainer</c> provides a handle to the current settings, which
    /// contain things like camera move sensitivity.
    /// </summary>
    public SettingsContainer settingsContainer;

    /// <summary>
    /// Method <c>HandleCameraMove</c> is called by the event system when the user wants to
    /// move the camera with the arrow keys (or keybound equivalent).
    /// </summary>
    /// <param name="context">The context of the event.</param>
    public void HandleCameraMove(InputAction.CallbackContext context) {
      Vector2 cameraMove = context.ReadValue<Vector2>();
      Vector3 cameraMove3D = new Vector3(cameraMove.x, cameraMove.y, 0.0f) * (settingsContainer.GetFloatSetting(SettingIdentifiers.CameraSideScrollSensitivity) * Time.deltaTime);

      Camera.main.transform.position = Camera.main.transform.position + cameraMove3D;
    }
  }
}