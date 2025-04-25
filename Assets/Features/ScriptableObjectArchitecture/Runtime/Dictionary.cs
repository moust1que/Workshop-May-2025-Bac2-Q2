using UnityEngine;
using System.Collections.Generic;

namespace ScriptableObjectArchitecture.Runtime {
    [CreateAssetMenu(fileName = "Dictionary", menuName = "Scriptable Objects/Dictionary")]
    public class Dictionary : ScriptableObject {
        public Dictionary<string, IFact> facts = new();
    }
}