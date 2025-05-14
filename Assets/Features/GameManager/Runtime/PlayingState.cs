using UnityEngine;

namespace GameManager.Runtime {
    public class PlayingState : IGameState {
        public void Enter(GameManager game) {
            game.ShowUI(game.listUI[1]);
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