using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Attribute.Editor {
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;

            object target = property.serializedObject.targetObject;
            FieldInfo conditionField = target.GetType().GetField(showIf.conditionFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if(conditionField == null) {
                Debug.LogWarning($"ShowIf: Field '{showIf.conditionFieldName}' not found on  {target.GetType()}");
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            object conditionValue = conditionField.GetValue(target);

            if(Equals(conditionValue, showIf.expectedValue)) {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;

            object target = property.serializedObject.targetObject;
            FieldInfo conditionField = target.GetType().GetField(showIf.conditionFieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if(conditionField == null) {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }

            object conditionValue = conditionField.GetValue(target);

            if(Equals(conditionValue, showIf.expectedValue)) {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }

            return 0;
        }
    }
}