using System;

namespace ScriptableObjectArchitecture.Runtime {
    public class Fact<T> : IFact {
        public string Name { get; set; }
        public Type type => typeof(T);
        public object Value { get; set; }
        public bool IsPersistent { get; set; }

        public Fact(string name, T value, bool isPersistent = false) {
            Name = name;
            Value = value;
            IsPersistent = isPersistent;
        }

        public T TypedValue {
            get => (T)Value;
            set => Value = value;
        }
    }
}