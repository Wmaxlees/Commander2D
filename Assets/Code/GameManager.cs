using Commander2D.TurnBased;
using Commander2D.UI;
using Commander2D.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commander2D {
  /// <summary>
  /// Class <c>GameManager</c> controls the whole game and keeps track of its state.
  /// </summary>
  public class GameManager : MonoBehaviour {
    /// <summary>
    /// Static property <c>s_GameManager</c> is the singleton instance of <c>GameManager</c>.
    /// </summary>
    private static GameManager s_GameManager;

    /// <summary>
    /// Static method <c>GetInstance</c> provides a handle for the singleton instance.
    /// </summary>
    /// <returns>Singleton instance of <c>GameManager</c>.</returns>
    public static GameManager GetInstance() {
      if (s_GameManager == null) {
        s_GameManager = new GameManager();
      }

      return s_GameManager;
    }

    /// <summary>
    /// Property <c>gameState</c> tracks the current state the game is in.
    /// </summary>
    private GameState gameState;

    /// <summary>
    /// Method <c>Awake</c> is called every time the object is created.
    /// </summary>
    private void Awake() {
      s_GameManager = this;
    }

    /// <summary>
    /// Method <c>GetGameState</c> returns the current state of the game.
    /// </summary>
    /// <returns>The current state of the game.</returns>
    public GameState GetGameState() {
      return gameState;
    }

    /// <summary>
    /// Method <c>SetGameState</c> sets the current state of the game.
    /// </summary>
    /// <param name="gameState">The new state of the game.</param>
    public void SetGameState(GameState gameState) {
      this.gameState = gameState;
      HUDManager.GetInstance().UpdateGameStateIndicator(gameState);
    }

    /// <summary>
    /// Enum <c>GameState</c> are all the different possible states the game could be in.
    /// </summary>
    public enum GameState {
      Unknown,
      Normal,
      TargetSelect,
      Paused,
    }

    /// <summary>
    /// Method <c>Start</c> is called once at the start of the scene.
    /// </summary>
    void Start() {
      // TODO: Get the initial state in a better way
      UnitController.GetInstance().LoadTestPlayers();
      this.SetGameState(GameState.Normal);
      TurnBasedController.GetInstance().SetTurnBasedOn(true);
    }
  }
}