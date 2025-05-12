using System.Collections.Generic;
using UnityEngine;
using System;         
// using System.Linq;     
using ScriptableObjectArchitecture.Runtime;

namespace Goals.Runtime
{
    [AddComponentMenu("Goals/Goal Loader")]
    public class GoalLoader : MonoBehaviour
    {
        [SerializeField] private TextAsset jsonFile;      // glisse goals.json
        [SerializeField] private GoalWindow windowPrefab;  // prefab ou GO avec GoalWindow
        [SerializeField] private Dictionary dictionary;    // asset partagé (vide)

        private GoalManager manager;
        private readonly Dictionary<string, IFact> factTable = new();

        private void Awake(){
            if (jsonFile == null || dictionary == null){
                Debug.LogError("GoalLoader : références manquantes !");
                return;
            }

            // 1) lit le JSON
            var wrapper = JsonUtility.FromJson<GoalFileWrapper>(jsonFile.text);

            // 2) crée les facts
            foreach (var fj in wrapper.facts){
                IFact fact = fj.type switch{
                    "int"  => new Fact<int>   (fj.id, int.Parse   (fj.initial)),
                    "float"=> new Fact<float> (fj.id, float.Parse (fj.initial)),
                    "bool" => new Fact<bool>  (fj.id, bool.Parse  (fj.initial)),
                    _      => null
                };
                if (fact == null){
                    Debug.LogWarning($"Type inconnu : {fj.type}");
                    continue;
                }
                factTable[fj.id] = fact;
            }

            // 3) instancie le GoalManager & l'UI
            manager = new GoalManager();
            GoalWindow window = Instantiate(windowPrefab);
            // window.Initialize(manager);             // pas besoin de killFact ici

            // 4) crée les goals
            foreach (var gj in wrapper.goals){
                if (!factTable.TryGetValue(gj.progress, out var prog)){
                    Debug.LogWarning($"Fact {gj.progress} non trouvé pour goal {gj.id}");
                    continue;
                }

                Goal g = new Goal {
                Id = gj.id,
                Name = gj.name,
                ParentId = gj.parentId,
                Show = gj.show,
                Discarded = gj.discarded,
                Progress = prog,
                Comparison = gj.comparison,
                Target = gj.target,
                Prereq = gj.prereq?.ToArray() ?? Array.Empty<string>()   // ← NOUVEAU
                };
                manager.AddGoal(g);
            }

            manager.EvaluateAndPropagate();
            window.Initialize(manager);             // (re)branche le manager
        }

        /* ---------------------------------------------------------------
         *  Exemples pour mettre à jour les facts depuis n'importe où
         * ------------------------------------------------------------- */
        public void Increment(string factId, int delta = 1){
            if (factTable.TryGetValue(factId, out var f) && f is Fact<int> fi){
                fi.TypedValue += delta;
                manager.EvaluateAndPropagate();
            }
        }

        public void SetBool(string factId, bool value){
            if (factTable.TryGetValue(factId, out var f) && f is Fact<bool> fb){
                fb.TypedValue = value;
                manager.EvaluateAndPropagate();
            }
        }
    }
}
