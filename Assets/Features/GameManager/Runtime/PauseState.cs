using UnityEngine;

namespace GameManager.Runtime {
    public class PauseState : IGameState {
        public void Enter(GameManager game) {
            game.ShowUI(game.listUI[2]);
            Time.timeScale = 0.0f;
        }

        public void Exit(GameManager game) {
            
        }

        public void Update(GameManager game) {
            if(Input.GetKeyDown(KeyCode.Escape))
                game.ChangeState(new PlayingState());
        }
    }
}