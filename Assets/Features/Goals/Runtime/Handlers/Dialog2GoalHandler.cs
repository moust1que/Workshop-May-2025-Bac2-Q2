using UnityEngine;
using Door.Runtime;

namespace Goals.Runtime {
    public class Dialog2GoalHandler : IGoalHandler {
        GameObject door;

        public Dialog2GoalHandler(GameObject door)
        {
            this.door = door;
        }

        public void OnGoalCompleted(Goal goal)
        {
            Goal act = GoalsManager.instance.goals["ACT1"];
            act.Progress.Value = (int)act.Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();
            
            door.GetComponent<DoorCloseAuto>().TriggerClose();
        }
    }
}