namespace Goals.Runtime
{
    public class SolveEnigma3GoalHandler : IGoalHandler
    {
        public void OnGoalCompleted(Goal goal) {
            GoalsManager.instance.goals["ACT3"].Progress.Value = (int)GoalsManager.instance.goals["ACT3"].Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();
        }
    }
}