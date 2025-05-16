using Dialog.Runtime;

namespace Goals.Runtime
{
    public class Act1GoalHandler : IGoalHandler
    {
        public void OnGoalCompleted(Goal goal)
        {
            DialogsManager.instance.DisplayDialog("Dialog5");
        }
    }
}