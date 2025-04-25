using System;

namespace ScriptableObjectArchitecture.Runtime {
    public interface IFact {
        string Name { get; set; }
        Type ValueType { get; }
        object Value { get; set; }
        bool IsPersistent { get; set; }
    }
}