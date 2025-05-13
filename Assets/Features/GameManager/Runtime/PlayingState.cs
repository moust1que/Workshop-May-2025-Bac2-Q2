using UnityEngine;
using UnityEngine.UIElements;

namespace GameManager.Runtime {
    public class PlayingState : IGameState {
        private Button toggleInventoryButton;

        public void Enter(GameManager game) {
            game.ShowUI(game.gameplayUI);
            Time.timeScale = 1.0f;

            UIDocument uiDocument = game.gameplayUI.GetComponent<UIDocument>();
            if(uiDocument == null) return;

            VisualElement root = uiDocument.rootVisualElement;
            toggleInventoryButton = root.Q<Button>("BtnToggle");
            if(toggleInventoryButton == null) return;

            toggleInventoryButton.clicked += OnToggleInventoryButtonClicked;
        }

        public void Exit(GameManager game) {
            if(toggleInventoryButton == null) return;

            toggleInventoryButton.clicked -= OnToggleInventoryButtonClicked;
        }

        public void Update(GameManager game) {
            if(Input.GetKeyDown(KeyCode.Escape))
                game.ChangeState(new PauseState());
        }

        private void OnToggleInventoryButtonClicked() {
            UIEvents.RaiseInventoryToggle();
        }
    }
}