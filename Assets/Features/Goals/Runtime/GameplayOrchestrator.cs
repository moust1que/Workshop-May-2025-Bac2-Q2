using UnityEngine;
using Events.Runtime;

namespace Goals.Runtime {
    using System.Collections.Generic;
    using BBehaviour.Runtime;
    using ScriptableObjectArchitecture.Runtime;

    public class GameplayOrchestrator : BBehaviour {
        public static GameplayOrchestrator instance;

        private Dictionary<string, IGoalHandler> goalHandlers;

        #region GameStart
            [SerializeField] private GameObject LetterToPickup;
        #endregion

        #region ACT1
        #endregion

        #region PickupLetter
        #endregion

        private void Start() {
            instance = this;

            goalHandlers = new Dictionary<string, IGoalHandler> {
                { "GameStart", new GameStartGoalHandler() },
                { "ACT1", new Act1GoalHandler() },
                { "PickupLetter", new PickupLetterGoalHandler() },
                { "LetterRead", new LetterReadGoalHandler() },
                { "Dialog1", new Dialog1GoalHandler() }
            };

            if(GoalsManager.instance != null) 
                GoalsManager.instance.OnGoalCompleted += HandleGoalCompleted;

            GameEvents.OnItemPickedUp += OnItemPickedUp;
            GameEvents.OnLetterRead += OnLetterRead;
            GameEvents.OnDialogEnded += OnDialogEnded;
        }

        private void OnDestroy() {
            if(GoalsManager.instance != null)
                GoalsManager.instance.OnGoalCompleted -= HandleGoalCompleted;

            GameEvents.OnItemPickedUp -= OnItemPickedUp;
            GameEvents.OnLetterRead -= OnLetterRead;
            GameEvents.OnDialogEnded -= OnDialogEnded;
        }

        private void HandleGoalCompleted(Goal goal) {
            if(goalHandlers.TryGetValue(goal.Id, out var handler)) {
                handler.OnGoalCompleted(goal);
            }else {
                Verbose($"[Orchestrator] No handler for goal {goal.Id}", VerboseType.Warning);
            }
        }

        public void TriggerInitialState() {
            Goal g = GoalsManager.instance.goals["GameStart"];
            g.Progress.Value = true;
            Goal g2 = GoalsManager.instance.goals["ACT1"];
            g2.Progress.Value = (int)g2.Progress.Value + 1;
            GoalsManager.instance.EvaluateAndPropagate();
        }

        public void OnItemPickedUp(ItemData item) {
            if(item.name == "Letter") {
                Goal g = GoalsManager.instance.goals["PickupLetter"];
                g.Progress.Value = true;
                
                Goal act = GoalsManager.instance.goals["ACT1"];
                act.Progress.Value = (int)act.Progress.Value + 1;
                GoalsManager.instance.EvaluateAndPropagate();
            }
            if(item.name == "Book") {
                Goal g = GoalsManager.instance.goals["GrabBook"];
                g.Progress.Value = true;
                GoalsManager.instance.EvaluateAndPropagate();
            }
        }

        public void OnLetterRead() {
            Goal g = GoalsManager.instance.goals["LetterRead"];
            g.Progress.Value = true;
            GoalsManager.instance.EvaluateAndPropagate();
        }

        public void OnDialogEnded(string id) {
            Goal g = GoalsManager.instance.goals[id];
            g.Progress.Value = true;
            GoalsManager.instance.EvaluateAndPropagate();
        }
    }
}