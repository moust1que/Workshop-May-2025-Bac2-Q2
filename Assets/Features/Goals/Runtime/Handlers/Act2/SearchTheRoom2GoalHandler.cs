namespace Goals.Runtime
{
    public class SearchTheRoom2GoalHandler : IGoalHandler
    {
        public void OnGoalCompleted(Goal goal) {
            GoalsManager.instance.goals["ACT2"].Progress.Value = (int)GoalsManager.instance.goals["ACT2"].Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();
        }
    }
}
