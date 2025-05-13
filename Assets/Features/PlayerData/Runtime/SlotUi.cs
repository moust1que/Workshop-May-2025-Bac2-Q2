using UnityEngine;
using ScriptableObjectArchitecture.Runtime;
using UnityEngine.UI;

namespace PlayerData.Runtime
{
    public class SlotUi : MonoBehaviour
    {
        public Image icon;
        [HideInInspector] public bool isEmpty = true;
        Image back;

        void Awake() => back = GetComponent<Image>();

        public void SetItem(Sprite spr) {
            icon.sprite  = spr;
            icon.enabled = true;
            isEmpty      = false;
        }

        public void Highlight(bool on) {
            back.color = on ? new Color(1f, .95f, .6f, 1f)
                            : new Color(1f, 1f, 1f, .35f);
        }

        public void SetItem(ItemData type) {
            icon.sprite  = type.icon;
            icon.enabled = true;
            isEmpty      = false;
        }

        public void ClearHighlight() => GetComponent<Image>().color = Color.white;
    }
}
