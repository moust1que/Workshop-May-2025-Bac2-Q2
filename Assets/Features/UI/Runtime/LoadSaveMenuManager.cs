using UnityEngine;

namespace UI.Runtime {
    using GameManager.Runtime;
    using Save.Runtime;

    public class LoadSaveMenuManager : MonoBehaviour {
        public void Back() {
            GameManager.instance.ChangeState(new MenuState());
        }

        public void TryLoadFromSlot(string slotName) {
            if(Save.ExistInSlot(slotName)) {
                LoadFromSlot(slotName);
            }
        }

        public void LoadFromSlot(string slotName) {
            GameManager.instance.ChangeState(new PlayingState());
            Save.LoadFromFile(slotName);
        }
    }
}