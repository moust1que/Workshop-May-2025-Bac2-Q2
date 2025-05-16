using Events.Runtime;
using UnityEngine;

namespace Goals.Runtime
{
    public class SolveEnigma2GoalHandler : IGoalHandler
    {
        private GameObject myEffect;
        public SolveEnigma2GoalHandler(GameObject quad)
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