using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Runtime {
    using BBehaviour.Runtime;

    public class ShamisenUIManager : BBehaviour {
        [Serializable]
        public class Partition
        {
            public string id;
            public AudioClip clip;
        }
        
        [SerializeField]
        private List<Partition> partitions;

        private Dictionary<string, AudioClip> _partitionDict;
        private AudioSource _audioSource;

        private void Awake() {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
                _audioSource = gameObject.AddComponent<AudioSource>();

            _partitionDict = partitions
                .Where(p => !string.IsNullOrEmpty(p.id) && p.clip != null)
                .ToDictionary(p => p.id, p => p.clip);
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
        }
    }
}
