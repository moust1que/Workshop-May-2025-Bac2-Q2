using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


namespace Gong.Runtime
{
    using BBehaviour.Runtime;
    public class GongSequenceManager : BBehaviour
    {
        [Header("Séquence attendue")]
        public List<int> keySequence = new() { 0, 2, 4, 1, 3 };

        [Header("Événements externes (facultatif)")]
        public UnityEvent onSequenceComplete;
        public UnityEvent onWrongSequence;

        [Header("Références gameplay")]
        public Lanterns lanterns;

        [Header("Audio")]
        public AudioClip successSfx;
        public AudioClip failSfx;
        private AudioSource _audio;

        private readonly List<int> _inputBuffer = new();

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            if (_audio == null) _audio = gameObject.AddComponent<AudioSource>();
        }

        public void RegisterHit(int gongID)
        {
            _inputBuffer.Add(gongID);

            if (_inputBuffer.Count > keySequence.Count)
                _inputBuffer.RemoveAt(0);

            if (_inputBuffer.Count != keySequence.Count) return;

            bool isCorrect = true;
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
                lanterns.canMove = true;
                if (successSfx) _audio.PlayOneShot(successSfx);
                onSequenceComplete?.Invoke();
            }
            else
            {
                if (failSfx) _audio.PlayOneShot(failSfx);
                onWrongSequence?.Invoke();
            }

            _inputBuffer.Clear();
        }
    }
}
