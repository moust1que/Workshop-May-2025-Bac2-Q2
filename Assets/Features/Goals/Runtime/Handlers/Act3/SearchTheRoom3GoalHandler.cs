using Dialog.Runtime;

namespace Goals.Runtime
{
    public class SearchTheRoom3GoalHandler : IGoalHandler
    {
        public void OnGoalCompleted(Goal goal)
        {
            GoalsManager.instance.goals["ACT3"].Progress.Value = (int)GoalsManager.instance.goals["ACT3"].Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();
            
            DialogsManager.instance.DisplayDialog("Dialog12");
        }
    }
}