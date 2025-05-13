using UnityEngine;

namespace GameManager.Runtime {
    public class MenuState : IGameState {
        public void Enter(GameManager game) {
            game.ShowUI(game.mainMenuUI);
            Time.timeScale = 0.0f;
        }

        public void Exit(GameManager game) {
            
        }

        public void Update(GameManager game) {
            // if(Input.GetKeyDown(KeyCode.Space))
            //     game.ChangeState(new PlayingState());
        }
    }
}