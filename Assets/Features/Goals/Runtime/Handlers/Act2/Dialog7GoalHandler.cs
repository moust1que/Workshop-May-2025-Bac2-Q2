using Events.Runtime;
using UnityEngine;

namespace Goals.Runtime
{
    public class Dialog7GoalHandler : IGoalHandler
    {
        private GameObject myEffect;
        public Dialog7GoalHandler(GameObject quad)
        {

            this.myEffect = quad;
        }
        public void OnGoalCompleted(Goal goal) {
            GoalsManager.instance.goals["ACT2"].Progress.Value = (int)GoalsManager.instance.goals["ACT2"].Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();

            myEffect.SetActive(true);
        }
    }
}