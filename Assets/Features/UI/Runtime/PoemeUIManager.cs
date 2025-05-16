using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Runtime
{
    
    using Events.Runtime;
    public class PoemeUIManager : MonoBehaviour
    {
        public void SelfDestroy()
        {
            UIInGameManager.instance.ShowAllChildSpecial();
            Destroy(gameObject);
        }

        public void OnLetterRead()
        {
            GameEvents.OnShamisenPlayed?.Invoke();
        }

        private void Start()
        {
            GameEvents.OnLetterRead += OnLetterRead;
        }

        private void OnDestroy()
        {
            GameEvents.OnLetterRead -= OnLetterRead;
        }

    }
}
