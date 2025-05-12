using System.Collections.Generic;

namespace Goals.Runtime
{
    /// <summary>Stocke les objectifs, évalue leur progression et propage l’état aux parents.</summary>
    public class GoalManager{
        private readonly Dictionary<string, Goal> goals = new();
        private readonly Dictionary<string, List<Goal>> children = new();

        /*-------------------  CRUD  -------------------*/
        public void AddGoal(Goal g){
            goals[g.Id] = g;

            if (!children.TryGetValue(g.ParentId, out var list))
                children[g.ParentId] = list = new List<Goal>();
            list.Add(g);
        }

        public IEnumerable<Goal> ChildrenOf(string parentId) =>
            children.TryGetValue(parentId, out var list) ? list : System.Array.Empty<Goal>();

        /*-------------------  Évaluation + propagation  -------------------*/
        public void EvaluateAndPropagate(){
            // 1) remonte récursivement
            Propagate("");      // "" = racines

            /* --------- Nouvelle partie : gère Show dynamiquement --------- */
            foreach (Goal g in goals.Values){
                g.Completed = g.Evaluate();
                if (g.Completed){
                    g.Show = false;
                    continue;
                }

                // S’affiche seulement si tous les prereq sont complets
                bool ready = true;
                foreach (string id in g.Prereq)
                    ready &= goals.TryGetValue(id, out var pre) && pre.Completed;

                g.Show = ready;
            }
        }

        private bool Propagate(string parentId){
            if (!children.TryGetValue(parentId, out var list)) return true;    // pas d'enfant

            bool allDone = true;
            foreach (Goal child in list){
                bool childDone = Propagate(child.Id) && child.Completed;
                allDone &= childDone;
            }

            if (goals.TryGetValue(parentId, out var parent) && parent.Show)
                parent.Completed = allDone;

            return allDone;
        }
    }
}
