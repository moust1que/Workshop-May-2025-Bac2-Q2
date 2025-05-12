using System;
using System.Collections.Generic;

namespace ScriptableObjectArchitecture.Runtime {
    [Serializable]
    public class StringFact {
        public string key;
        public string factType;
        public string value;
        public bool isPersistent;
    }

    [Serializable]
    public class FactEntryListWrapper {
        public List<StringFact> entries;
    }
}
