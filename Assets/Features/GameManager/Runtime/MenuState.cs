using UnityEngine;

namespace GameManager.Runtime {
    public class MenuState : IGameState {
        public void Enter(GameManager game) {
            game.ShowUI(game.listUI[0]);
            Time.timeScale = 0.0f;
        }

        public void Exit(GameManager game) {
            
        }

        public void Update(GameManager game) {
            
        }
    }
}