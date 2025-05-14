namespace Goals.Runtime {
    public class Act1GoalHandler : IGoalHandler {
        public void OnGoalCompleted(Goal goal) {
            switch(goal.Id) {
                case "PickupLetter":
                    UnityEngine.Debug.Log("Letter picked up");
                    break;
                case "Dialog1":
                    UnityEngine.Debug.Log("Dialog 1");
                    break;
            }
        }
    }
}