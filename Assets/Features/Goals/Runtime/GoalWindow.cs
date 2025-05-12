using UnityEngine;
using Goals.Runtime;
using ScriptableObjectArchitecture.Runtime;

namespace Goals.Runtime
{
    [AddComponentMenu("Goals/Goal Window (demo)")]
    public class GoalWindow : MonoBehaviour{
        private GoalManager manager;
        private IFact killFact;     // référence facultative (affiche le bouton “Kill +1”)

        // /*======  Initialisation depuis GoalSetup  ======*/
        public void Initialize(GoalManager mgr, IFact kFact = null){
            manager  = mgr;
            // killFact = kFact;
        }

        /*======  GUI  ======*/
        private void OnGUI(){
            if (manager == null) return;      // rien n’a été branché → on sort

            /* ----- affichage hiérarchique ----- */
            GUILayout.BeginArea(new Rect(Screen.width - 220, 10, 210, Screen.height - 20));
            DrawRecursive("", 0);
            GUILayout.EndArea();
        }

        private void DrawRecursive(string parentId, int indent){
            foreach (Goal g in manager.ChildrenOf(parentId)){
                if (!g.Show) continue;

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
