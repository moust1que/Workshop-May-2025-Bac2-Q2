using UnityEngine;

namespace Goals.Runtime
{
    public class Dialog3GoalHandler : IGoalHandler
    {
        private GameObject ashes;

        public Dialog3GoalHandler(GameObject ashes)
        {
            this.ashes = ashes;
        }

        public void OnGoalCompleted(Goal goal)
        {
            Goal act = GoalsManager.instance.goals["ACT1"];
            act.Progress.Value = (int)act.Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();

            ashes.SetActive(true);
        }
    }
}