using System;

namespace ScriptableObjectArchitecture.Runtime {
    public interface IFact {
        string Name { get; set; }
        Type type { get; }
        object Value { get; set; }
        bool IsPersistent { get; set; }
    }
}