using UnityEngine;
using UnityEngine.Events;
// using Attribute.Runtime;

namespace Gong.Runtime
{
    using System.Collections.Generic;
    using BBehaviour.Runtime;
    public class GongSequenceManager : BBehaviour
    {
        public List<int> keySequence = new() { 0, 2, 4, 1, 3 };

        public UnityEvent onSequenceComplete;
        public UnityEvent onWrongSequence;

        public Lanterns lanterns;

        private List<int> _inputBuffer = new();

        public void RegisterHit(int gongID)
        {
            Verbose("RegisterHit");
            _inputBuffer.Add(gongID);

            if (_inputBuffer.Count > keySequence.Count)
                _inputBuffer.RemoveAt(0);

            if (_inputBuffer.Count == keySequence.Count)
            {
                bool isCorrect = true;
                Verbose("isCorrect = true;");
                for (int i = 0; i < keySequence.Count; i++)
                {
                    if (_inputBuffer[i] != keySequence[i])
                    {
                        isCorrect = false;
                        break;
                    }
                }

                if (isCorrect)
                {

                    Verbose("Sequence complete");
                    lanterns.canMove = true;
                    // DelayManager.instance.Delay(0.5f, () => onSequenceComplete?.Invoke());
                }
                else
                {
                    onWrongSequence?.Invoke();
                    Verbose("Wrong sequence");
                }

                _inputBuffer.Clear();
            }
        }
    }
}
