using UnityEngine;
using UnityEngine.UIElements;

namespace GameManager.Runtime {
    public class PlayingState : IGameState {
        private Button toggleInventoryButton;

        public void Enter(GameManager game) {
            game.ShowUI(game.gameplayUI);
            Time.timeScale = 1.0f;
        }

        public void Exit(GameManager game) {

        }

        public void Update(GameManager game) {
            if(Input.GetKeyDown(KeyCode.Escape))
                game.ChangeState(new PauseState());
        }
    }
}