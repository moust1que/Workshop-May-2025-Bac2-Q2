using Dialog.Runtime;

namespace Goals.Runtime
{
    public class FukuroDespawnGoalHandler : IGoalHandler
    {
        public void OnGoalCompleted(Goal goal) {
            GoalsManager.instance.goals["ACT2"].Progress.Value = (int)GoalsManager.instance.goals["ACT2"].Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();
            
            DialogsManager.instance.DisplayDialog("Dialog10");
        }
    }
}
