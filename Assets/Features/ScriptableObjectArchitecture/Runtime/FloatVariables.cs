using UnityEngine;

namespace ScriptableObjectArchitecture.Runtime {
    [CreateAssetMenu(fileName = "FloatVariables", menuName = "Scriptable Objects/FloatVariables")]
    public class FloatVariables : ScriptableObject {
        public float Value;
    }
}