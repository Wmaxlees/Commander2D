using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

namespace Commander2D.Board {
  /// <summary>
  /// Class <c>GameBoard</c> provides tools for determining information about the game board.
  /// </summary>
  public class GameBoard : MonoBehaviour {
    /// <summary>
    /// Property <c>gridLayout</c> provides a handle to the actual board layout.
    /// </summary>
    private Grid gridLayout;

    /// <summary>
    /// Property <c>tilemap</c> provides a handle to the set of tiles on the board layout.
    /// </summary>
    private Tilemap tilemap;

    /// <summary>
    /// Static property <c>s_GameBoard</c> is the singleton instance of <c>GameBoard</c>.
    /// </summary>
    private static GameBoard s_GameBoard;

    /// <summary>
    /// Static method <c>GetInstance</c> provides the singleton instance of <c>GameBoard</c>.
    /// </summary>
    /// <returns>The singleton instance of <c>GameBoard</c>.</returns>
    public static GameBoard GetInstance() {
      if (s_GameBoard == null) {
        s_GameBoard = new GameBoard();
      }

      return s_GameBoard;
    }

    /// <summary>
    /// Method <c>Awake</c> is automatically called when the object is created.
    /// </summary>
    private void Awake() {
      s_GameBoard = this;

      gridLayout = gameObject.GetComponent<Grid>();
      tilemap = gameObject.GetComponentInChildren<Tilemap>();

      Debug.Assert(gridLayout != null, "No GridLayout is attached to the GameBoard.");
      Debug.Assert(tilemap != null, "No Tilemap is attached to the GameBoard.");
    }

    /// <summary>
    /// Method <c>MouseOnTile</c> returns whether the mouse pointer is currently over a tile.
    /// </summary>
    /// <returns>Whether the mouse pointer is over a tile.</returns>
    public bool MouseOnTile() {
      Vector2 mousePosition = Mouse.current.position.ReadValue();
      Vector3 mousePosition3D = new Vector3(mousePosition.x, mousePosition.y, 0.0f);
      Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition3D);

      return tilemap.HasTile(tilemap.WorldToCell(mouseWorldPosition) * new Vector3Int(1, 1, 0));
    }

    // public Vector3 MouseToTileCenter() {
    //   Vector2 mousePosition = Mouse.current.position.ReadValue();
    //   Vector3 mousePosition3D = new Vector3(mousePosition.x, mousePosition.y, 0.0f);
    //   Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition3D);
    //   Vector3Int mouseGridLocation = gridLayout.WorldToCell(mouseWorldPosition);
    //   return gridLayout.GetCellCenterWorld(mouseGridLocation);
    // }

    /// <summary>
    /// Method <c>MouseToWorldPostion</c> converts the mouse position to the world position.
    /// </summary>
    /// <returns>The world position of the mouse.</returns>
    public Vector3 MouseToWorldPosition() {
      Vector2 mousePosition = Mouse.current.position.ReadValue();
      Vector3 mousePosition3D = new Vector3(mousePosition.x, mousePosition.y, 0.0f);
      return Camera.main.ScreenToWorldPoint(mousePosition3D);
    }

    /// <summary>
    /// Method <c>GetBoardDistance</c> determines how far away an object is in board space.
    /// </summary>
    /// <param name="a">The first location.</param>
    /// <param name="b">The second location.</param>
    /// <returns>The distance in board space.</returns>
    public Vector3 GetBoardDistance(Vector3 a, Vector3 b) {
      a = new Vector3(a.x, a.y, 0.0f);
      b = new Vector3(b.x, b.y, 0.0f);
      Vector3 diff = gridLayout.WorldToCell(a) - gridLayout.WorldToCell(b);

      return diff;
    }
  }
}