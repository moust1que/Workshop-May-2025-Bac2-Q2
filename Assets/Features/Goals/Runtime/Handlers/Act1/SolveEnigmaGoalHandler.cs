using UnityEngine;

namespace Goals.Runtime
{
    public class SolveEnigmaGoalHandler : IGoalHandler
    {
        private ParticleSystem breath;

        public SolveEnigmaGoalHandler( ParticleSystem breath)
        {
            
            this.breath = breath;
        }
        public void OnGoalCompleted(Goal goal)
        {
           
            Goal act = GoalsManager.instance.goals["ACT1"];
            act.Progress.Value = (int)act.Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();
            
            breath.Stop();
        }
    }
}