using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Goals.Runtime;

namespace UI.Runtime {
    using Events.Runtime;
    using BBehaviour.Runtime;

    public class ShamisenUIManager : BBehaviour
    {
        [Serializable]
        public class Partition
        {
            public string id;
            public AudioClip clip;
        }

        [SerializeField]
        private List<Partition> partitions;
        [SerializeField] private AudioClip yokaiScream;

        private Dictionary<string, AudioClip> _partitionDict;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
                _audioSource = gameObject.AddComponent<AudioSource>();

            _partitionDict = partitions
                .Where(p => !string.IsNullOrEmpty(p.id) && p.clip != null)
                .ToDictionary(p => p.id, p => p.clip);
        }

        private void Start()
        {
            GameEvents.OnLetterRead += OnLetterRead;
        }

        private void OnDestroy()
        {
            GameEvents.OnLetterRead -= OnLetterRead;
        }

        public void PlayPartition(string id)
        {
            if (_partitionDict.TryGetValue(id, out AudioClip clip))
            {
                _audioSource.Stop();
                _audioSource.clip = clip;
                _audioSource.Play();
            }
            else
            {
                Verbose($"[ShamisenUI] Partition introuvable pour l’ID « {id} »", VerboseType.Warning);
            }

            if (id == "3" && (bool)GoalsManager.instance.goals["SearchTheRoom1"].Progress.Value == false)
            {
                _audioSource.PlayOneShot(yokaiScream);
                GameEvents.OnYokaiScream?.Invoke();
            }
        }
        
        public void SelfDestroy() {
            // GameObject parentCanvas = GameObject.Find("UIInGame");
            // for(int i = 0; i < parentCanvas.transform.childCount; i++) {
            //     parentCanvas.transform.GetChild(i).gameObject.SetActive(true);
            // }
            UIInGameManager.instance.ShowAllChildSpecial();
            Destroy(gameObject);
        }

        public void OnLetterRead() {
            GameEvents.OnShamisenPlayed?.Invoke();
        }
    }
}
