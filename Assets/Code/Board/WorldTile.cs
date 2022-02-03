using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Commander2D.Board {
  /// <summary>
  /// Class <c>WorldTile</c> overloads the <c>Tile</c> to include information needed for
  /// Commander2D.
  /// </summary>
  public class WorldTile : Tile {
    /// <summary>
    /// Property <c>walkable</c> defines whether units can walk on the tile.
    /// </summary>
    [SerializeField]
    private bool walkable = true;

    /// <summary>
    /// Property <c>blockProjectiles</c> defines whether projectiles can pass through the tile.
    /// </summary>
    [SerializeField]
    private bool blockProjectiles = false;

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a WorldTile Asset
    [MenuItem("Assets/Create/WorldTile")]
    public static void CreateWorldTile() {
      string path = EditorUtility.SaveFilePanelInProject("Save World Tile", "New World Tile", "Asset", "Save World Tile", "Assets");
      if (path == "")
        return;
      AssetDatabase.CreateAsset(CreateInstance<WorldTile>(), path);
    }
#endif
  }
}