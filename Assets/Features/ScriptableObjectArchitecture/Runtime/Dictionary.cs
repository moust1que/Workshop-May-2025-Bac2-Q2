using UnityEngine;
using System.Collections.Generic;

namespace ScriptableObjectArchitecture.Runtime {
    [CreateAssetMenu(fileName = "Dictionary", menuName = "Scriptable Objects/Dictionary")]
    public class Dictionary : ScriptableObject {
         public Dictionary<string, IFact> facts = new Dictionary<string, IFact>();

        public void SetFact(string fact, IFact value) {
            if(value != null) facts.Add(fact, value);
        }

        public IFact GetFact(string fact) {
            if(!TryGet(fact)) return null;

            return facts[fact];
        }

        public bool TryGet(string fact) {
            return facts.ContainsKey(fact);
        }

        public void RemoveFact(string fact) {
            facts.Remove(fact);
        }

        public void Clear() {
            facts.Clear();
        }

        public List<StringFact> ToSerializableList() {
            List<StringFact> entries = new List<StringFact>();

            foreach (var pair in facts) {
                entries.Add(new StringFact {
                    key = pair.Key,
                    factType = pair.Value.type.Name,
                    value = pair.Value.Value.ToString(),
                    isPersistent = pair.Value.IsPersistent
                });
            }

            return entries;
        }

        public void LoadFromSerializableList(List<StringFact> entries) {
            facts.Clear();

            foreach (var entry in entries) {
                IFact fact = null;

                switch (entry.factType) {
                    case "String":
                        fact = new Fact<string>(entry.key, entry.value, entry.isPersistent);
                        break;
                    case "Boolean":
                        bool boolVal = bool.Parse(entry.value);
                        fact = new Fact<bool>(entry.key, boolVal, entry.isPersistent);
                        break;
                    case "Int32":
                        int intVal = int.Parse(entry.value);
                        fact = new Fact<int>(entry.key, intVal, entry.isPersistent);
                        break;
                }

                if (fact != null) facts.Add(entry.key, fact);
            }
        }
    }
}