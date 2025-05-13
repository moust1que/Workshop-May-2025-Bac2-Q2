using UnityEngine;

namespace Goals.Runtime {
    using BBehaviour.Runtime;

    [AddComponentMenu("Goals/Goal Window (demo)")]
    public class GoalWindow : BBehaviour {
        private void OnGUI() {
            if(GoalsManager.instance == null) return;

            GUILayout.BeginArea(new Rect(Screen.width - 220, 10, 210, Screen.height - 20));
            DrawRecursive("", 0);
            GUILayout.EndArea();
        }

        private void DrawRecursive(string parentId, int indent) {
            foreach(Goal g in GoalsManager.instance.ChildrenOf(parentId)) {
                Verbose("GoalWindow : affiche " + g.Show, VerboseType.Log);
                if(!g.Show) continue;

                GUILayout.BeginHorizontal();
                GUILayout.Space(indent * 15);
                GUI.color = g.Completed ? Color.green : Color.white;
                GUILayout.Label(g.Name + (g.Completed ? " ✔" : ""));
                GUI.color = Color.white;
                GUILayout.EndHorizontal();

                DrawRecursive(g.Id, indent + 1);
            }
        }
    }
}