using UnityEngine;
using Attribute.Runtime;
using PlayerMovement.Runtime;
using CameraManager.Runtime;

namespace Goals.Runtime {
    public class ExitDoorClosedGoalHandler : IGoalHandler
    {
        private Transform RoomCenter;

        public ExitDoorClosedGoalHandler(Transform RoomCenter)
        {
            this.RoomCenter = RoomCenter;
        }

        public void OnGoalCompleted(Goal goal)
        {
            Goal act = GoalsManager.instance.goals["ACT1"];
            act.Progress.Value = (int)act.Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();

            DelayManager.instance.Delay(1f, AfterDelay);
        }

        public void AfterDelay()
        {
            Camera.main.transform.position = RoomCenter.position;
            PlayerMovementManager.instance.CurrentNavigationPoint = RoomCenter.GetComponent<NavigationPoint>();
            PlayerMovementManager.instance.DisplayUI();

            // Mets ici
        }
    }
}