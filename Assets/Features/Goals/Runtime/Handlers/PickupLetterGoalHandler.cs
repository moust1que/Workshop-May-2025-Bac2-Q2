namespace Goals.Runtime {
    public class PickupLetterGoalHandler : IGoalHandler {
        public void OnGoalCompleted(Goal goal) {
            Goal act = GoalsManager.instance.goals["ACT1"];
            act.Progress.Value = (int)act.Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();
        }
    }
}