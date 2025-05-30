using System;
using UnityEngine;
using ScriptableObjectArchitecture.Runtime;


namespace Events.Runtime {
    public static class GameEvents
    {
        public static Action<ItemData> OnItemPickedUp;
        public static Action<ItemData> OnItemUsed;
        public static Action OnLetterRead;
        public static Action<string> OnDialogEnded;
        public static Action<string> OnTeleport;
        public static Action<string> OnDoorClosed;
        public static Action<Transform> OnPlayerMoved;
        public static Action OnShamisenPlayed;
        public static Action OnYokaiScream;
        public static Action OnEnableFeature;
        public static Action OnDisableFeature;

        public static Action OnTurnPageLeft;
        public static Action OnTurnPageRight;

    }
}