using UnityEngine;
using System;

namespace Attribute.Editor {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ShowIfAttribute : PropertyAttribute {
        public string conditionFieldName { get; private set; }
        public object expectedValue { get; private set; }

        public ShowIfAttribute(string conditionFieldName, object expectedValue) {
            this.conditionFieldName = conditionFieldName;
            this.expectedValue = expectedValue;
        }
    }
}