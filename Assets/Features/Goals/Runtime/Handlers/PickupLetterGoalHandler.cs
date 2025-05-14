namespace Goals.Runtime {
    public class PickupLetterGoalHandler : IGoalHandler {
        public void OnGoalCompleted(Goal goal) {
            UnityEngine.Debug.Log("Letter picked up");
        }
    }
}