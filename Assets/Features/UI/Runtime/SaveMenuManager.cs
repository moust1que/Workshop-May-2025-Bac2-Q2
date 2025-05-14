namespace UI.Runtime {
    using BBehaviour.Runtime;
    using GameManager.Runtime;
    using Save.Runtime;
    using UnityEngine;

    public class SaveMenuManager : BBehaviour {
        public Canvas confirmOverrideCanvas;

        string curSlotToOverride;

        public void Back() {
            GameManager.instance.ChangeState(new PauseState());
        }

        public void TrySaveToSlot(string slotName) {
            if(Save.ExistInSlot(slotName)) {
                curSlotToOverride = slotName;
                confirmOverrideCanvas.gameObject.SetActive(true);
            } else {
                SaveToSlot(slotName);
            }
        }

        public void SaveToSlot(string slotName) {
            Save.SaveToFile(slotName);
        }

        public void ConfirmOverride() {
            SaveToSlot(curSlotToOverride);
            confirmOverrideCanvas.gameObject.SetActive(false);
            curSlotToOverride = null;
        }

        public void CancelOverride() {
            confirmOverrideCanvas.gameObject.SetActive(false);
            curSlotToOverride = null;
        }
    }
}