using UnityEditor;
using UnityEngine;

namespace Commander2D.Board {
  public static class Extensions {
    public static string ToLabel(this Direction direction) {
      switch (direction) {
        case Direction.UP:
          return "Up";

        case Direction.DOWN:
          return "Down";

        case Direction.LEFT:
          return "Left";

        case Direction.RIGHT:
          return "Right";

        default:
          return "Down";
      }
    }
  }

  public enum Direction {
    UP,
    DOWN,
    LEFT,
    RIGHT
  }
}