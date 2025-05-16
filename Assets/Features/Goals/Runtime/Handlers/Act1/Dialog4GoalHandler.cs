using UnityEngine;

namespace Goals.Runtime
{
    public class Dialog4GoalHandler : IGoalHandler
    {

        public void OnGoalCompleted(Goal goal) {
            Goal act = GoalsManager.instance.goals["ACT1"];
            act.Progress.Value = (int)act.Progress.Value + 1;

            Goal g = GoalsManager.instance.goals["LeaveDungeon"];
            g.Progress.Value = true;
            
            GoalsManager.instance.EvaluateAndPropagate();

            

        }
    }
}