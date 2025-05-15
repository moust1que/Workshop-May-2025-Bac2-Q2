using System;
using ScriptableObjectArchitecture.Runtime;

namespace Events.Runtime {
    public static class GameEvents {
        public static Action<ItemData> OnItemPickedUp;

        public static Action OnLetterRead;
    }
}