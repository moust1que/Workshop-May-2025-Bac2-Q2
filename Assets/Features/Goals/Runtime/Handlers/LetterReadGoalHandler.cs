using Dialog.Runtime;

namespace Goals.Runtime {
    public class LetterReadGoalHandler : IGoalHandler {
        public void OnGoalCompleted(Goal goal) {
            DialogsManager.instance.DisplayDialog("dialog1");
        }
    }
}