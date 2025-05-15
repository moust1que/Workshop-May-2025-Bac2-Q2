using Dialog.Runtime;

namespace Goals.Runtime {
    public class LetterReadGoalHandler : IGoalHandler {
        public void OnGoalCompleted(Goal goal) {
            UnityEngine.Debug.Log("Letter read");
            DialogsManager.instance.DisplayDialog("dialog1");
        }
    }
}