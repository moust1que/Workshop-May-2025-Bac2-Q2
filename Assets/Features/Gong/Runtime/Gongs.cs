using UnityEngine;

namespace Gong.Runtime
{
    using BBehaviour.Runtime;
    public class Gongs : BBehaviour
    {
        public int gongID;

        public AudioClip gongSfx;

        private AudioSource _source;
        [SerializeField]private GongSequenceManager _manager;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        void OnMouseDown()
        {
            Verbose($"Gong {gongID} hit");
            if (gongSfx != null) _source.PlayOneShot(gongSfx);
            if (_manager != null)
                _manager.RegisterHit(gongID);
            else
                Verbose("GongSequenceManager reference NOT set!", VerboseType.Warning);
        }
    }
}
