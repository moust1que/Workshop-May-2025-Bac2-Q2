using UnityEngine;
using Events.Runtime;
using ScriptableObjectArchitecture.Runtime;


namespace PlayerData.Runtime {
    using BBehaviour.Runtime;
    public class KatanaTracker : BBehaviour {
         [SerializeField] private ItemData katanaData;

        public bool IsKatanaPlaced { get; private set; }

        void OnEnable() {
            GameEvents.OnItemUsed += OnItemUsed;
        }

        void OnDisable() {
            GameEvents.OnItemUsed -= OnItemUsed;
        }

        private void OnItemUsed(ItemData item) {
            if (item == katanaData)
                IsKatanaPlaced = true;
                Verbose("Katana placed");
        }
    }
}
