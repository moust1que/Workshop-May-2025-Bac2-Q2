using PlayerMovement.Runtime;

namespace Goals.Runtime
{
    public class LeaveDungeonGoalHandler : IGoalHandler
    {
        public void OnGoalCompleted(Goal goal)
        {
            Goal act = GoalsManager.instance.goals["ACT1"];
            act.Progress.Value = (int)act.Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();


            PlayerMovementManager.instance.HideUI();
        }
    }
}