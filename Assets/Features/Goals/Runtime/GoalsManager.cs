using System.Collections.Generic;

namespace Goals.Runtime {
    using BBehaviour.Runtime;

    public class GoalsManager : BBehaviour {
        public readonly Dictionary<string, Goal> goals = new();
        private readonly Dictionary<string, List<Goal>> children = new();

        public static GoalsManager instance { get; private set; }

        public event System.Action<Goal> OnGoalCompleted;

        private void Awake() {
            instance = this;
            Verbose("GoalsManager : initialisation", VerboseType.Log);
        }

        public void AddGoal(Goal g) {
            goals[g.Id] = g;

            if(!children.TryGetValue(g.ParentId, out var list)) {
                children[g.ParentId] = list = new List<Goal>();
            }

            list.Add(g);
        }

        public IEnumerable<Goal> ChildrenOf(string parentId) => children.TryGetValue(parentId, out var list) ? list : System.Array.Empty<Goal>();

        public void EvaluateAndPropagate() {
            Propagate("");

            foreach(Goal g in goals.Values) {
                bool wasCompleted = g.Completed;
                g.Completed = g.Evaluate();

                if(!wasCompleted && g.Completed) {
                    Verbose($"Goal {g.Id} completed !", VerboseType.Log);
                    OnGoalCompleted?.Invoke(g);
                }

                if(g.Completed) {
                    g.Show = false;
                    continue;
                }

                if(g.AlwaysHidden) {
                    g.Show = false;
                    continue;
                }

                bool ready = true;
                foreach(string id in g.Prereq) {
                    if(goals.TryGetValue(id, out var pre)) {
                        if(!pre.Completed) {
                            Verbose($"Prerequisite not meeted for {g.Id} prerequisite is {g.Prereq[0]}!", VerboseType.Warning);
                            ready = false;
                        }
                    }
                }

                g.Show = ready;
            }
        }

        private bool Propagate(string parentId) {
            if(!children.TryGetValue(parentId, out var list)) return true;

            bool allDone = true;
            foreach(Goal child in list) {
                bool childDone = Propagate(child.Id) && child.Completed;
                allDone &= childDone;
            }

            if(goals.TryGetValue(parentId, out var parent) && parent.Show)
                parent.Completed = allDone;

            return allDone;
        }
    }
}