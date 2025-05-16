using Events.Runtime;
using JetBrains.Annotations;
// using Attribute.Runtime;
using PlayerMovement.Runtime;
using CameraManager.Runtime;
using UnityEngine;

namespace Goals.Runtime
{
    public class Dialog7GoalHandler : IGoalHandler
    {
        private Transform Room2Center;

        private GameObject myEffect;
        public Dialog7GoalHandler(GameObject quad, Transform Room2Center)
        {
            this.Room2Center = Room2Center;
            this.myEffect = quad;
        }
        public void OnGoalCompleted(Goal goal)
        {
            GoalsManager.instance.goals["ACT2"].Progress.Value = (int)GoalsManager.instance.goals["ACT2"].Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();

            myEffect.SetActive(true);
        }
        
        public void AfterDelay()
        {
            Camera.main.transform.position = Room2Center.position;
            PlayerMovementManager.instance.CurrentNavigationPoint = Room2Center.GetComponent<NavigationPoint>();
            PlayerMovementManager.instance.DisplayUI();
        }
    }
}