using UnityEngine;

namespace GameManager.Runtime {
    public class SaveMenuState : IGameState {
        public void Enter(GameManager game) {
            game.ShowUI(game.listUI[3]);
        }

        public void Exit(GameManager game) {

        }

        public void Update(GameManager game) {
            if(Input.GetKeyDown(KeyCode.Escape))
                game.ChangeState(new PlayingState());
        }
    }
}