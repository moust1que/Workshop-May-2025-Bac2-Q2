using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog.Runtime {
    [Serializable] public class DialogJson {
        public string id;
        public string[] speakers;
        public string[] dialogs;
    }

    [Serializable] public class DialogFileWrapper {
        public List<DialogJson> dialogs;
    }
}
