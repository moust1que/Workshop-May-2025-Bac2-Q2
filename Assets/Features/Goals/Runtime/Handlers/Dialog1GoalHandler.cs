using UnityEngine;

namespace Goals.Runtime {
    public class Dialog1GoalHandler : IGoalHandler {
        public void OnGoalCompleted(Goal goal) {
            GameObject.Find("Book").GetComponent<MeshCollider>().enabled = true;
        }
    }
}