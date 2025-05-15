using UnityEngine;
using TMPro;
using Events.Runtime;

namespace Dialog.Runtime {
    using System.Collections.Generic;
    using BBehaviour.Runtime;

    public class DialogsManager : BBehaviour {
        [SerializeField] private TextAsset dialogsFile;
        [SerializeField] private GameObject dialogBox;
        [SerializeField] private TextMeshProUGUI speakerName;
        [SerializeField] private TextMeshProUGUI text;


        private Dictionary<string, DialogJson> dialogs = new();
        private DialogLine[] currentLines;
        private int currentDialogIndex = 0;
        private bool displaying = false;
        private string currentDialogId;

        public static DialogsManager instance { get; private set; }

        private void Awake() {
            instance = this;
        }

        private void Start() {
            if(dialogsFile == null) {
                Verbose("DialogsManager : file missing!", VerboseType.Error);
                return;
            }

            DialogFileWrapper wrapper = JsonUtility.FromJson<DialogFileWrapper>(dialogsFile.text);

            foreach(DialogJson dialogJson in wrapper.dialogs) {
                Verbose($"DialogsManager : loaded dialog {dialogJson.id} with {dialogJson.dialog.Length} lines.", VerboseType.Log);
                dialogs[dialogJson.id] = dialogJson;
            }
        }

        private void Update() {
            dialogBox.SetActive(displaying);
        }

        public void DisplayDialog(string id) {
            if(!dialogs.TryGetValue(id, out var dialog)) {
                Verbose($"DialogsManager : dialog ID '{id}' not found.", VerboseType.Warning);
                return;
            }

            var lines = dialog.dialog;
            if(lines == null || lines.Length == 0) {
                Verbose($"DialogsManager : dialog '{id}' has no lines.", VerboseType.Warning);
                return;
            }

            currentDialogId = id;
            currentLines = dialog.dialog;
            currentDialogIndex = 0;
            displaying = true;

            DisplayCurrentLine();
        }

        public void ShowNextLine() {
            currentDialogIndex++;

            if(currentDialogIndex >= currentLines.Length) {
                EndDialog();
                return;
            }

            DisplayCurrentLine();
        }

        private void EndDialog() {
            displaying = false;
            text.SetText("");
            speakerName.SetText("");
            GameEvents.OnDialogEnded?.Invoke(currentDialogId);
        }

        private void DisplayCurrentLine() {
            DialogLine line = currentLines[currentDialogIndex];
            speakerName.SetText(line.speaker);
            text.SetText(line.text);
        }
    }
}