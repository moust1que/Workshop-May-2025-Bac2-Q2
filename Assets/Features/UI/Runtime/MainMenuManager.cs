using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.Runtime {
    using BBehaviour.Runtime;
    using Save.Runtime;
    using GameManager.Runtime;

    public class MainMenuManager : BBehaviour {
        [Header("UI References")]
        [Tooltip("Prefab contenant un Button + Image + TMP_Text")]
        public Button buttonPrefab;
        public VerticalLayoutGroup layoutGroup;

        private void OnEnable() {
            foreach (Transform child in layoutGroup.transform) {
                Destroy(child.gameObject);
            }

            if(Save.Exists())
                CreateButton("Continue", Color.white, () => Verbose("Continue", VerboseType.Log));

            CreateButton("New Game", Color.white, () => CreateNewGame());
            if(Save.Exists())
                CreateButton("Load Game", Color.white, () => Verbose("Load Game", VerboseType.Log));

            CreateButton("Settings", Color.white, () => Verbose("Open Settings", VerboseType.Log));
            CreateButton("Exit", Color.white, () => Exit());

            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
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

        void Exit() {
            Verbose("Exit", VerboseType.Log);
            Application.Quit();
        }

        void CreateNewGame() {
            GameManager.instance.ChangeState(new PlayingState());
        }
    }
}