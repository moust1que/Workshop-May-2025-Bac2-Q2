using UnityEngine;
using TMPro;

namespace Dialog.Runtime {
    using System.Collections.Generic;
    using BBehaviour.Runtime;

    public class DialogsManager : BBehaviour {
        [SerializeField] private TextAsset dialogsFile;
        [SerializeField] private GameObject dialogBox;
        [SerializeField] private TextMeshProUGUI speakerName;
        [SerializeField] private TextMeshProUGUI text;


        private Dictionary<string, DialogJson> dialogs = new();

        public static DialogsManager instance { get; private set; }

        private string[] currentDialog;
        private string[] currentSpeakers;
        private int currentDialogIndex = 0;
        private bool displaying = false;

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
                dialogs[dialogJson.id] = dialogJson;
            }
        }

        private void Update() {
            if(displaying) {
                dialogBox.SetActive(true);
            }else if(dialogBox.activeSelf) {
                dialogBox.SetActive(false);
            }
        }

        private string[] GetDialogs(string id) {
            if(!dialogs.TryGetValue(id, out var dialog)) {
                Verbose($"DialogsManager : dialog {id} not found!", VerboseType.Warning);
                return null;
            }

            return dialog.dialogs;
        }

        private string[] GetSpeakers(string id) {
            if(!dialogs.TryGetValue(id, out var dialog)) {
                Verbose($"DialogsManager : dialog {id} not found!", VerboseType.Warning);
                return null;
            }

            return dialog.speakers;
        }

        public void DisplayDialog(string id) {
            string[] dialog = GetDialogs(id);
            string[] speakers = GetSpeakers(id);

            if(dialog == null || speakers == null || dialog.Length == 0 || speakers.Length != dialog.Length) {
                Verbose($"DialogsManager : invalid dialog or speakers for id {id}!", VerboseType.Error);
                return;
            }

            currentDialog = dialog;
            currentSpeakers = speakers;
            currentDialogIndex = 0;
            displaying = true;

            DisplayCurDialog(currentDialogIndex);
        }

        public void ShowNextLine() {
            currentDialogIndex++;

            if(currentDialogIndex >= currentDialog.Length) {
                EndDialog();
                return;
            }

            DisplayCurDialog(currentDialogIndex);
        }

        private void EndDialog() {
            displaying = false;
            text.SetText("");
            speakerName.SetText("");
        }

        private void DisplayCurDialog(int id) {
            speakerName.SetText(currentSpeakers[id]);
            text.SetText(currentDialog[id]);
        }
    }
}