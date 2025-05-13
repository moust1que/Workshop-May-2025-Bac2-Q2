using System;

namespace GameManager.Runtime {
    public static class UIEvents {
        public static event Action OnInventoryToggle;

        public static void RaiseInventoryToggle() {
            OnInventoryToggle?.Invoke();
        }
    }
}