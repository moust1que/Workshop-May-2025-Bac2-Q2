using Dialog.Runtime;
using UnityEngine;

namespace Goals.Runtime
{
    public class PickupIngredientGoalHandler : IGoalHandler
    {
        private GameObject yokaiAshes;

        public PickupIngredientGoalHandler(GameObject yokaiAshes){
            this.yokaiAshes = yokaiAshes;
        }
        public void OnGoalCompleted(Goal goal)
        {
            Goal act = GoalsManager.instance.goals["ACT1"];
            act.Progress.Value = (int)act.Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();

            DialogsManager.instance.DisplayDialog("Dialog4");

            yokaiAshes = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/yokaiAshesPrefab"), Vector3.zero, Quaternion.identity);
        }
    }
}