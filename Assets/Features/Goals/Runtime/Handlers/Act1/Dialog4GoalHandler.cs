using UnityEngine;

namespace Goals.Runtime
{
    public class Dialog4GoalHandler : IGoalHandler
    {
        private Transform room2Start;

        public Dialog4GoalHandler(Transform room2Start)
        {
            this.room2Start = room2Start;
        }
        
        public void OnGoalCompleted(Goal goal)
        {
            Goal act = GoalsManager.instance.goals["ACT1"];
            act.Progress.Value = (int)act.Progress.Value + 1;

            Camera.main.transform.position = room2Start.position;
            Camera.main.transform.rotation = room2Start.rotation;
            
            Goal g = GoalsManager.instance.goals["LeaveDungeon"];
            g.Progress.Value = true;

            GoalsManager.instance.EvaluateAndPropagate();
        }
    }
}