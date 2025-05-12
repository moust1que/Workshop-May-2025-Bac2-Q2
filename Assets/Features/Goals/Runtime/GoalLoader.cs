using System.Collections.Generic;
using UnityEngine;
using System;  
using ScriptableObjectArchitecture.Runtime;

namespace Goals.Runtime {
    using BBehaviour.Runtime;

    [AddComponentMenu("Goals/Goal Loader")]
    public class GoalLoader : BBehaviour {
        [SerializeField] private TextAsset jsonFile;
        [SerializeField] private GoalWindow windowPrefab;
        [SerializeField] private Dictionary dictionary;

        private GoalManager manager;
        private readonly Dictionary<string, IFact> factTable = new();

        private void Awake() {
            if(jsonFile == null || dictionary == null) {
                Verbose("GoalLoader : références manquantes !", VerboseType.Error);
                return;
            }

            var wrapper = JsonUtility.FromJson<GoalFileWrapper>(jsonFile.text);

            // 2) crée les facts
            foreach(var factJson in wrapper.facts) {
                IFact fact = factJson.type switch {
                    "int" => new Fact<int>(factJson.id, int.Parse(factJson.initial)),
                    "float" => new Fact<float>(factJson.id, float.Parse(factJson.initial)),
                    "bool" => new Fact<bool>(factJson.id, bool.Parse(factJson.initial)),
                    _ => null
                };
                if(fact == null) {
                    Verbose($"GoalLoader : type inconnu : {factJson.type}", VerboseType.Warning);
                    continue;
                }
                factTable[factJson.id] = fact;
            }

            manager = new GoalManager();
            GoalWindow window = Instantiate(windowPrefab);

            foreach(var roomJson in wrapper.rooms) {
                Verbose("GoalLoader : chargement de la salle " + roomJson.name, VerboseType.Log);
                foreach(var goalJson in roomJson.goals) {
                    if(!factTable.TryGetValue(goalJson.progress, out var prog)) {
                        Verbose($"GoalLoader : fact {goalJson.progress} non trouvé pour goal {goalJson.id}", VerboseType.Warning);
                        continue;
                    }

                    Goal goal = new Goal {
                        Id = goalJson.id,
                        Name = goalJson.name,
                        ParentId = goalJson.parentId,
                        Show = goalJson.show,
                        AlwaysHidden = goalJson.alwaysHidden,
                        Discarded = goalJson.discarded,
                        Progress = prog,
                        Comparison = goalJson.comparison,
                        Target = goalJson.target,
                        Prereq = goalJson.prereq?.ToArray() ?? Array.Empty<string>()
                    };
                    manager.AddGoal(goal);
                }
            }

            manager.EvaluateAndPropagate();
            window.Initialize(manager);
        }
        
        public void Increment(string factId, int delta = 1) {
            if(factTable.TryGetValue(factId, out var f) && f is Fact<int> fi) {
                fi.TypedValue += delta;
                manager.EvaluateAndPropagate();
            }
        }

        public void SetBool(string factId, bool value) {
            if(factTable.TryGetValue(factId, out var f) && f is Fact<bool> fb) {
                fb.TypedValue = value;
                manager.EvaluateAndPropagate();
            }
        }
    }
}