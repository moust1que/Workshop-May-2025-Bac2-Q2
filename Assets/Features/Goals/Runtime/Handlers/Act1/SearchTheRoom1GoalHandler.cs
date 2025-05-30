using Dialog.Runtime;

namespace Goals.Runtime
{
    public class SearchTheRoom1GoalHandler : IGoalHandler
    {
        public void OnGoalCompleted(Goal goal)
        {
            Goal act = GoalsManager.instance.goals["ACT1"];
            act.Progress.Value = (int)act.Progress.Value + 1;

            Goal g = GoalsManager.instance.goals["IdentifyEffects1"];
            g.Progress.Value = 3;
            
            GoalsManager.instance.EvaluateAndPropagate();

            DialogsManager.instance.DisplayDialog("Dialog3");
        }
    }
}