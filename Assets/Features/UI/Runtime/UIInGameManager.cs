using UnityEngine;
using System;

namespace UI.Runtime {
    using System.Collections.Generic;
    using BBehaviour.Runtime;

    public class UIInGameManager : BBehaviour {
        [Serializable]
        public class SpecialUI {
            public GameObject ui;
            public bool Enabled;
        }

        [SerializeField] private List<SpecialUI> specialUIs = new();

        public static UIInGameManager instance;

        private void Awake() {
            instance = this;
        }

        public void HideAllChild() {
            foreach(Transform child in transform) {
                child.gameObject.SetActive(false);
            }
        }

        public void ShowAllChildSpecial() {
            foreach(SpecialUI ui in specialUIs) {
                if(!ui.Enabled) continue;
                ui.ui.SetActive(true);
            }
        }

        public void EnableUI(int id) {
            specialUIs[id].Enabled = true;
            specialUIs[id].ui.SetActive(true);
        }
    }
}