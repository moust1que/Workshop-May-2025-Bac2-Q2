using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.Runtime {
    public class UIManager : MonoBehaviour {
        
        [Header("UI References")]
        [Tooltip("Prefab contenant un Button + Image + TMP_Text")]
        public Button buttonPrefab;
        public VerticalLayoutGroup layoutGroup;
        // [SerializeField] UIBuilderManager builder;

        private void OnEnable() {
            // 1. Nettoie d'éventuels enfants existants (utile si ce menu s'ouvre/ferme)
            foreach (Transform child in layoutGroup.transform) {
                Destroy(child.gameObject);
            }

            // 2. Boutons permanents
            CreateButton("New Game", Color.white, () => OnStartClicked());
            CreateButton("Settings", Color.white, () => Debug.Log("Open Settings"));
            CreateButton("teste", Color.red, () => ReloadMenu());

            // 3. Boutons conditionnels
            if (Save.Runtime.Save.Exists()) {
                CreateButton("Load Game", Color.white, () => Debug.Log("Load Game"));
                CreateButton("Continue", Color.white, () => Debug.Log("Continue"));
            }

            // 4. Force le recalcul du layout (utile juste après la création)
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
            if (btn.GetComponentInChildren<TMP_Text>() is TMP_Text tmp) {
                tmp.text = label;
            } else if (btn.GetComponentInChildren<Text>() is Text uiText) {
                uiText.text = label;
            } else {
                Debug.LogWarning($"Le prefab '{buttonPrefab.name}' ne contient ni TMP_Text ni UI.Text.");
            }

            // Callback clic
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(onClick);

            return btn;
        }

        void ReloadMenu(){
            if (Save.Runtime.Save.Exists()) {
                CreateButton("Load Game", Color.white, () => Debug.Log("Load Game"));
                CreateButton("Continue", Color.white, () => Debug.Log("Continue"));
            }
        }

        void OnStartClicked(){
            // builder.Show();
            DisableCanvas();
        }

        void DisableCanvas(){
            layoutGroup.gameObject.SetActive(false);
        }
    }
}