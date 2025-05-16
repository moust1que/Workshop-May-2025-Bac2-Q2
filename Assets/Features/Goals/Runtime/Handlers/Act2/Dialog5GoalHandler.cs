namespace Goals.Runtime
{
    public class Dialog5GoalHandler : IGoalHandler
    {
        public void OnGoalCompleted(Goal goal) {
            Goal act = GoalsManager.instance.goals["ACT2"];
            act.Progress.Value = (int)act.Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();
        }
    }
}