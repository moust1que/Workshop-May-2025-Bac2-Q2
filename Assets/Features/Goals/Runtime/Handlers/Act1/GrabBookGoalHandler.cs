using UnityEngine;

namespace Goals.Runtime {
    public class GrabBookGoalHandler : IGoalHandler {
        private GameObject DoorToUnlock;

        public GrabBookGoalHandler(GameObject DoorToUnlock) {
            this.DoorToUnlock = DoorToUnlock;
        }

        public void OnGoalCompleted(Goal goal) {
            Goal act = GoalsManager.instance.goals["ACT1"];
            act.Progress.Value = (int)act.Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();
            
            DoorToUnlock.GetComponent<BoxCollider>().enabled = true;
        }
    }
}