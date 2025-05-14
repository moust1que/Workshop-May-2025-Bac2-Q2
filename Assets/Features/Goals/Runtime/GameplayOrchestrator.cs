namespace Goals.Runtime {
    using BBehaviour.Runtime;

    public class GameplayOrchestrator : BBehaviour {
        public static GameplayOrchestrator instance;

        private void Start() {
            instance = this;

            if(GoalsManager.instance != null) 
                GoalsManager.instance.OnGoalCompleted += HandleGoalCompleted;
        }

        private void OnDestroy() {
            if(GoalsManager.instance != null)
                GoalsManager.instance.OnGoalCompleted -= HandleGoalCompleted;
        }

        private void HandleGoalCompleted(Goal goal) {
            Verbose($"[Orchestrator] Goal completed: {goal.Id}", VerboseType.Log);

            switch(goal.Id) {
                case "GameStart":
                    Verbose("[Orchestrator] Game start", VerboseType.Log);
                    break;
                case "ACT1":
                    break;
                case "PickupLetter":
                    break;
                case "Dialog1":
                    break;
                case "GrabB":
                    break;
                case "LeaveRoom":
                    break;
                case "Dialog2":
                    break;
                case "EDoorClosed":
                    break;
                case "SE1":
                    break;
                case "IE1":
                    break;
                case "STR1":
                    break;
                case "Dialog3":
                    break;
                case "PickupIngredient":
                    break;
                case "Dialog4":
                    break;
                case "LeaveDungeon":
                    break;
                case "ACT2":
                    break;
                case "ClimbTheLadder":
                    break;
                case "Dialog5":
                    break;
                case "SE2":
                    break;
                case "IE2":
                    break;
                case "STR2":
                    break;
                case "FukuroSpawn":
                    break;
                case "TalkToYokai":
                    break;
                case "Dialog6":
                    break;
                case "FukuroDespawn":
                    break;
                case "ACT3":
                    break;
                case "AIM":
                    break;
                case "Dialog7":
                    break;
                case "SE3":
                    break;
                case "IE3":
                    break;
                case "STR3":
                    break;
                case "Dialog8":
                    break;
                case "PutIngredients":
                    break;
                case "BoxOpening":
                    break;
                case "RecipeOnScreen":
                    break;
                case "Dialog9":
                    break;
            }
        }

        //! Log mais ne fais pas la suite
        public void TriggerInitialState() {
            Goal g = GoalsManager.instance.goals["GameStart"];
            g.Progress.Value = true;
        }
    }
}