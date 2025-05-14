using UnityEngine;

namespace GameManager.Runtime {
    public class LoadSaveMenuState : IGameState {
        public void Enter(GameManager game) {
            game.ShowUI(game.listUI[4]);
        }

        public void Exit(GameManager game) {
            
        }

        public void Update(GameManager game) {
            if(Input.GetKeyDown(KeyCode.Escape))
                game.ChangeState(new MenuState());
        }
    }
}