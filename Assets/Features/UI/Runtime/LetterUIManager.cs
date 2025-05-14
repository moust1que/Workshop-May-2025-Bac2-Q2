using UnityEngine;

namespace UI.Runtime {
    using BBehaviour.Runtime;

    public class LetterUIManager : BBehaviour {
        public void SelfDestroy() {
            GameObject parentCanvas = GameObject.Find("UIInGame");
            for(int i = 0; i < parentCanvas.transform.childCount; i++) {
                parentCanvas.transform.GetChild(i).gameObject.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}