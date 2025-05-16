using UnityEngine;
using Attribute.Runtime;
using PlayerMovement.Runtime;
using CameraManager.Runtime;

namespace Goals.Runtime {
    public class ExitDoorClosedGoalHandler : IGoalHandler
    {
        private Transform RoomCenter;
        private ParticleSystem breath;

        public ExitDoorClosedGoalHandler(Transform RoomCenter, ParticleSystem breath)
        {
            this.RoomCenter = RoomCenter;
            this.breath = breath;
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

            breath.Play();
            // Mets ici
        }
    }
}