using UnityEngine;

namespace UI.Runtime {
    using BBehaviour.Runtime;
    using Events.Runtime;

    public class LetterUIManager : BBehaviour {
        public void SelfDestroy() {
            // GameObject parentCanvas = GameObject.Find("UIInGame");
            // for(int i = 0; i < parentCanvas.transform.childCount; i++) {
            //     parentCanvas.transform.GetChild(i).gameObject.SetActive(true);
            // }
            UIInGameManager.instance.ShowAllChildSpecial();
            Destroy(gameObject);
        }

        public void OnLetterRead() {
            GameEvents.OnLetterRead?.Invoke();
        }
    }
}