using Events.Runtime;

namespace Goals.Runtime
{
    public class Dialog7GoalHandler : IGoalHandler
    {
        public void OnGoalCompleted(Goal goal) {
            GoalsManager.instance.goals["ACT2"].Progress.Value = (int)GoalsManager.instance.goals["ACT2"].Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();

            GameEvents.OnEnableFeature?.Invoke();
        }
    }
}