namespace Commander2D.Units {

  public static class Extensions {
    public static UnitID playerUnitThreshold = UnitID.PlayerUnit6;
    /// <summary>
    /// Method <c>IsPlayerUnit</c> returns whether a <c>UnitID</c> corresponds
    /// to a player unit or not.
    /// </summary>
    /// <param name="unitID">The unit's ID.</param>
    /// <returns><c>true</c> if the <c>UnitID</c> is a player unit ID.</returns>
    public static bool IsPlayerUnit(this UnitID unitID) {
      return unitID <= playerUnitThreshold;
    }

    public static string ToLabel(this ArchetypeID species) {
      switch (species) {
        case ArchetypeID.Chopper:
          return "Chopper";

        case ArchetypeID.Chunker:
          return "Chunker";

        case ArchetypeID.Pyromaniac:
          return "Pyromaniac";

        default:
          return "None";
      }
    }
  }

  /// <summary>
  /// Enum <c>UnitID</c> provides unique handles for each active unit.
  /// </summary>
  public enum UnitID : int {
    PlayerUnit1 = 0,
    PlayerUnit2 = 1,
    PlayerUnit3 = 2,
    PlayerUnit4 = 3,
    PlayerUnit5 = 4,
    PlayerUnit6 = 5,
    EnemyUnit1 = 11,
    EnemyUnit2 = 12,
    EnemyUnit3 = 13,
    EnemyUnit4 = 14,
    EnemyUnit5 = 15,
    EnemyUnit6 = 16
  }

}