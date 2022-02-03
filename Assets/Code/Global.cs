namespace Commander2D {
  /// <summary>
  /// Static class <c>Global</c> contains a bunch of static global variables.
  /// </summary>
  static class Global {
    /// <summary>
    /// Static readonly property <c>MAX_PLAYER_UNITS</c> is the maximum number of 
    /// units a player can have active at one time.
    /// </summary>
    public static readonly int MAX_PLAYER_UNITS = 6;

    /// <summary>
    /// Static readonly property <c>MAX_ACTION_POINTS</c> is the maximum number of
    /// action points a unit gets on its turn.
    /// </summary>
    public static readonly int MAX_ACTION_POINTS = 4;

    /// <summary>
    /// Static readonly property <c>DEFAULT_ACTION_ACTION_POINT_COST</c> is how many
    /// action points the default attack action costs.
    /// </summary>
    public static readonly int DEFAULT_ATTACK_ACTION_POINT_COST = 2;
  }
}