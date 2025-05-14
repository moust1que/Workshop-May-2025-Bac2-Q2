using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.Runtime {
    using BBehaviour.Runtime;
    using GameManager.Runtime;

    public class PauseMenuManager : BBehaviour {
        public Button buttonPrefab;
        public VerticalLayoutGroup layoutGroup;

        private void OnEnable() {
            foreach(Transform child in layoutGroup.transform) {
                Destroy(child.gameObject);
            }

            CreateButton("Resume", Color.white, () => Resume());
            CreateButton("Save", Color.white, () => OpenSaveMenu());
            CreateButton("Settings", Color.white, () => OpenSettingsMenu());
            CreateButton("Exit", Color.white, () => Exit());
        }

        private Button CreateButton(string label, Color background, UnityEngine.Events.UnityAction onClick) {
            // Instancie le prefab sous le layout
            Button btn = Instantiate(buttonPrefab, layoutGroup.transform, false);
            btn.name = label.Replace(" ", string.Empty);

            // Couleur de fond / cible graphique
            if (btn.targetGraphic is Image img) {
                img.color = background;
            }

            // Mise à jour du texte (TMP_Text prioritaire, sinon Text)
            if(btn.GetComponentInChildren<TMP_Text>() is TMP_Text tmp) {
                tmp.text = label;
            } else if(btn.GetComponentInChildren<Text>() is Text uiText) {
                uiText.text = label;
            } else {
                Verbose($"Le prefab '{buttonPrefab.name}' ne contient ni TMP_Text ni UI.Text.", VerboseType.Warning);
            }

            // Callback clic
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(onClick);

            return btn;
        }

        void Resume() {
            GameManager.instance.ChangeState(new PlayingState());
        }

        void OpenSaveMenu() {
            GameManager.instance.ChangeState(new SaveMenuState());
        }

        void OpenSettingsMenu() {
            Verbose("Open Settings Menu", VerboseType.Log);
        }

        void Exit() {
            Verbose("Exit", VerboseType.Log);
            Application.Quit();
        }
    }
}