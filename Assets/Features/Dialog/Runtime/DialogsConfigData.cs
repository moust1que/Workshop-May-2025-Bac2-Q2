using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog.Runtime {
    [Serializable] public class DialogJson {
        public string id;
        public DialogLine[] dialog;
    }

    [Serializable] public class DialogLine {
        public string speaker;
        public string text;
    }

    [Serializable] public class DialogFileWrapper {
        public List<DialogJson> dialogs;
    }
}
