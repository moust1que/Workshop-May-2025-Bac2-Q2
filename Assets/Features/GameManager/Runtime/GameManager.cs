using UnityEngine;

namespace GameManager.Runtime {
    using System.Collections.Generic;
    using BBehaviour.Runtime;

    public class GameManager : BBehaviour {
        #region Private Variables
            private IGameState currentState;

            private Camera mainCamera;
        #endregion

        #region Public Variables
            public static GameManager instance;

            public Transform spawnPoint;

            public List<GameObject> listUI;
            // public GameObject gameplayUI;
            // public GameObject pauseUI;
            // public GameObject saveMenuUI;
        #endregion

        void Start() {
            instance = this;

            ChangeState(new MenuState());

            mainCamera = Camera.main;
            mainCamera.transform.position = spawnPoint.position;
            mainCamera.transform.rotation = spawnPoint.rotation;
        }

        void Update() {
            currentState.Update(this);
        }

        public void ChangeState(IGameState newState) {
            currentState?.Exit(this);
            currentState = newState;
            currentState?.Enter(this);
        }

        public void ShowUI(GameObject ui) {
            foreach(GameObject menu in listUI) {
                menu.SetActive(false);
            }
            // mainMenuUI.SetActive(false);
            // gameplayUI.SetActive(false);
            // pauseUI.SetActive(false);
            // saveMenuUI.SetActive(false);
            
            if(ui != null)
                ui.SetActive(true);
        }
    }
}