using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileHighlighter : MonoBehaviour
{
  private Grid gridLayout;
  private Tilemap tilemap;

  private void Awake() {
    gridLayout = gameObject.GetComponentInParent<Grid>();
    tilemap = gridLayout.GetComponentInChildren<Tilemap>();
  }

  private void Start() {
    Vector3Int cellPosition = this.gridLayout.WorldToCell(this.transform.position);
    this.transform.position = this.gridLayout.GetCellCenterWorld(cellPosition);
  }

  public void HandleMouseMove(InputAction.CallbackContext context) {
    Vector2 mousePosition = context.ReadValue<Vector2>();
    Vector3 mousePosition3D = new Vector3(mousePosition.x, mousePosition.y, 0.0f);
    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition3D);
    Vector3Int mouseGridLocation = gridLayout.WorldToCell(mouseWorldPosition);

    if (this.tilemap.HasTile(this.tilemap.WorldToCell(mouseWorldPosition) * new Vector3Int(1, 1, 0))) {
      this.transform.position = this.gridLayout.GetCellCenterWorld(mouseGridLocation);
    }
  }
}
