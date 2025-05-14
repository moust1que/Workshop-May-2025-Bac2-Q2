using ScriptableObjectArchitecture.Runtime;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Save.Runtime {
    using BBehaviour.Runtime;

    public class SaveManager : BBehaviour {
        public SaveManager instance;

        public DictionaryVariable factDictionary;
        public List<Button> buttons = new();

        private void Awake() {
            instance = this;
        }

        private void Start() {
            Save.factDictionary = factDictionary;
            Save.buttons = buttons;
        }
    }
}