using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog.Runtime
{
    public struct PortraitEntry
    {
        public string speaker;
        public Sprite sprite;
    }

    public class PortraitController : MonoBehaviour {
        [Header("Static portraits")]
        [SerializeField] private Image mainPortrait;
        [SerializeField] private Sprite mainSprite;

        [Header("Dynamic speaker portrait")]
        [SerializeField] private Image speakerPortrait;
        [SerializeField] private PortraitEntry[] additionalPortraits;

        private Dictionary<string, Sprite> dict;

        public static PortraitController instance { get; private set; }

        private void Awake() {
            instance = this;
            dict = new Dictionary<string, Sprite>(additionalPortraits.Length);
            foreach (var p in additionalPortraits)
                dict[p.speaker] = p.sprite;
        }

        private void Start() {
            // héros toujours visible
            mainPortrait.sprite = mainSprite;
            mainPortrait.enabled = true;

            // interlocuteur masqué au départ
            speakerPortrait.enabled = false;
        }

        public void ShowSpeaker(string name) {
            // si le héros parle, on n’affiche pas de deuxième tête
            if (name == "HERO" || name == "Player") {   // adapte si besoin
                speakerPortrait.enabled = false;
                return;
            }

            if (dict.TryGetValue(name, out var sprite)) {
                speakerPortrait.sprite = sprite;
                speakerPortrait.enabled = true;
            } else {
                Debug.LogWarning($"PortraitController : sprite manquant pour le speaker '{name}'.");
                speakerPortrait.enabled = false;
            }
        }

        public void HideSpeaker() => speakerPortrait.enabled = false;
    }
}