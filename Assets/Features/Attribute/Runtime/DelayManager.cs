using System;
using System.Collections.Generic;
using UnityEngine;


namespace Attribute.Runtime {
    using BBehaviour.Runtime;
    
    public class DelayManager : BBehaviour
    {
        private class DelayedAction
        {
            public float triggerTime;
            public Action callback;
        }

        private readonly List<DelayedAction> delayedActions = new List<DelayedAction>();

        public static DelayManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void Delay(float seconds, Action callback)
        {
            delayedActions.Add(new DelayedAction
            {
                triggerTime = Time.time + seconds,
                callback = callback
            });
        }

        private void Update()
        {
            float currentTime = Time.time;

            for (int i = delayedActions.Count - 1; i >= 0; i--)
            {
                if (currentTime >= delayedActions[i].triggerTime)
                {
                    delayedActions[i].callback?.Invoke();
                    delayedActions.RemoveAt(i);
                }
            }
        }
    }
}
