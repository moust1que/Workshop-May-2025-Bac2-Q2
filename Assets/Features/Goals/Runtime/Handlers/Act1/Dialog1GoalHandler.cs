using UnityEngine;

namespace Goals.Runtime {
    public class Dialog1GoalHandler : IGoalHandler {
        private GameObject book;

        public Dialog1GoalHandler(GameObject book) {
            this.book = book;
        }

        public void OnGoalCompleted(Goal goal) {
            Goal act = GoalsManager.instance.goals["ACT1"];
            act.Progress.Value = (int)act.Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();

            book.GetComponent<BoxCollider>().enabled = true;
        }
    }
}