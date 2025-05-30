using UnityEngine;
using Events.Runtime;

namespace Goals.Runtime {
    using System.Collections.Generic;
    using BBehaviour.Runtime;
    using ScriptableObjectArchitecture.Runtime;

    public class GameplayOrchestrator : BBehaviour
    {
        public static GameplayOrchestrator instance;

        private Dictionary<string, IGoalHandler> goalHandlers;

        #region GameStart
        [SerializeField] private GameObject LetterToPickup;
        #endregion

        #region Dialog1
        [SerializeField] private GameObject book;
        #endregion

        #region GrabBook
        [SerializeField] private GameObject doorAfterGrabBook;
        #endregion

        #region Dialog2
        [SerializeField] private GameObject doorToClose;
        #endregion

        #region ExitDoorClosed
        [SerializeField] private Transform room1Center;
        [SerializeField] private ParticleSystem breath;
        #endregion

        #region Dialog3
        [SerializeField] private GameObject ashes;
        #endregion

        #region Dialog4
        [SerializeField] private Transform room2Start;
        #endregion

        #region 
        [SerializeField] private GameObject blur;
        #endregion

        #region Dialog7
        [SerializeField] private Transform room2Center;
        #endregion

        private void Start()
        {
            instance = this;

            goalHandlers = new Dictionary<string, IGoalHandler> {
                { "GameStart", new GameStartGoalHandler() },
                { "ACT1", new Act1GoalHandler() },
                { "PickupLetter", new PickupLetterGoalHandler() },
                { "LetterRead", new LetterReadGoalHandler() },
                { "Dialog1", new Dialog1GoalHandler(book) },
                { "GrabBook", new GrabBookGoalHandler(doorAfterGrabBook) },
                { "LeaveTheRoom", new LeaveTheRoomGoalHandler() },
                { "Dialog2", new Dialog2GoalHandler(doorToClose) },
                { "ExitDoorClosed", new ExitDoorClosedGoalHandler(room1Center, breath) },
                { "SolveEnigma", new SolveEnigmaGoalHandler(breath) },
                { "IdentifyEffects1", new IdentifyEffects1GoalHandler() },
                { "SearchTheRoom1", new SearchTheRoom1GoalHandler() },
                { "Dialog3", new Dialog3GoalHandler(ashes) },
                { "PickupIngredient", new PickupIngredientGoalHandler() },
                { "Dialog4", new Dialog4GoalHandler(room2Start) },
                { "LeaveDungeon", new LeaveDungeonGoalHandler() },

                { "ACT2", new Act2GoalHandler() },
                { "Dialog5", new Dialog5GoalHandler() },
                { "ClimbTheLadder", new ClimbTheLadderGoalHandler() },
                { "Dialog6", new Dialog6GoalHandler() },
                { "FloorRambles", new FloorRamblesGoalHandler() },
                { "Dialog7", new Dialog7GoalHandler(blur, room2Center) },
                { "SolveEnigma2", new SolveEnigma2GoalHandler(blur) },
                { "IdentifyEffects2", new IdentifyEffects2GoalHandler() },
                { "SearchTheRoom2", new SearchTheRoom2GoalHandler() },
                { "FukuroSpawn", new FukuroSpawnGoalHandler() },
                { "TalkToYokai", new TalkToYokaiGoalHandler() },
                { "Dialog8", new Dialog8GoalHandler() },
                { "FeatherAdd", new FeatherAddGoalHandler() },
                { "Dialog9", new Dialog9GoalHandler() },
                { "FukuroDespawn", new FukuroDespawnGoalHandler() },
                { "Dialog10", new Dialog10GoalHandler() },

                { "ACT3", new Act3GoalHandler() },
                { "AppearInMausoleum", new AppearInMausoleumGoalHandler() },
                { "Dialog11", new Dialog11GoalHandler() },
                { "SolveEnigma3", new SolveEnigma3GoalHandler() },
                { "IdentifyEffects3", new IdentifyEffects3GoalHandler() },
                { "SearchTheRoom3", new SearchTheRoom3GoalHandler() },
                { "Dialog12", new Dialog12GoalHandler() },
                { "PutIngredients", new PutIngredientsGoalHandler() },
                { "BoxOpening", new BoxOpeningGoalHandler() },
                { "RecipeOnScreen", new RecipeOnScreenGoalHandler() },
                { "Dialog13", new Dialog13GoalHandler() }
            };

            if (GoalsManager.instance != null)
                GoalsManager.instance.OnGoalCompleted += HandleGoalCompleted;

            GameEvents.OnItemPickedUp += OnItemPickedUp;
            GameEvents.OnLetterRead += OnLetterRead;
            GameEvents.OnDialogEnded += OnDialogEnded;
            GameEvents.OnTeleport += OnTeleport;
            GameEvents.OnDoorClosed += OnDoorClosed;
            GameEvents.OnYokaiScream += OnYokaiScream;
        }

        private void OnDestroy()
        {
            if (GoalsManager.instance != null)
                GoalsManager.instance.OnGoalCompleted -= HandleGoalCompleted;

            GameEvents.OnItemPickedUp -= OnItemPickedUp;
            GameEvents.OnLetterRead -= OnLetterRead;
            GameEvents.OnDialogEnded -= OnDialogEnded;
            GameEvents.OnTeleport -= OnTeleport;
            GameEvents.OnDoorClosed -= OnDoorClosed;
            GameEvents.OnYokaiScream -= OnYokaiScream;
        }

        private void HandleGoalCompleted(Goal goal)
        {
            if (goalHandlers.TryGetValue(goal.Id, out var handler))
            {
                handler.OnGoalCompleted(goal);
            }
            else
            {
                Verbose($"[Orchestrator] No handler for goal {goal.Id}", VerboseType.Warning);
            }
        }

        public void TriggerInitialState()
        {
            Goal g = GoalsManager.instance.goals["GameStart"];
            g.Progress.Value = true;
            GoalsManager.instance.EvaluateAndPropagate();
        }

        public void OnItemPickedUp(ItemData item)
        {
            if (item.name == "Letter")
            {
                Goal g = GoalsManager.instance.goals["PickupLetter"];
                g.Progress.Value = true;
                GoalsManager.instance.EvaluateAndPropagate();
            }
            if (item.name == "Book")
            {
                Goal g = GoalsManager.instance.goals["GrabBook"];
                g.Progress.Value = true;
                GoalsManager.instance.EvaluateAndPropagate();
            }
            if (item.name == "Ashes")
            {
                Goal g = GoalsManager.instance.goals["PickupIngredient"];
                g.Progress.Value = true;
                GoalsManager.instance.EvaluateAndPropagate();
            }
        }

        public void OnLetterRead()
        {
            Goal g = GoalsManager.instance.goals["LetterRead"];
            g.Progress.Value = true;
            GoalsManager.instance.EvaluateAndPropagate();
        }

        public void OnDialogEnded(string id)
        {
            Goal g = GoalsManager.instance.goals[id];
            g.Progress.Value = true;
            GoalsManager.instance.EvaluateAndPropagate();
        }

        public void OnTeleport(string id)
        {
            Goal g = GoalsManager.instance.goals[id];
            g.Progress.Value = true;
            GoalsManager.instance.EvaluateAndPropagate();
        }

        public void OnDoorClosed(string id)
        {
            Goal g = GoalsManager.instance.goals[id];
            g.Progress.Value = true;
            GoalsManager.instance.EvaluateAndPropagate();
        }

        public void OnYokaiScream()
        {
            Goal g = GoalsManager.instance.goals["SearchTheRoom1"];
            g.Progress.Value = 1;
            GoalsManager.instance.EvaluateAndPropagate();
        }
    }
}