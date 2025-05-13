using System;
using System.Collections.Generic;

namespace Goals.Runtime {
    [Serializable] public class FactJson { public string id; public string type; public string initial; public string persistent; }

    [Serializable] public class RoomJson { public string name; public GoalJson[] goals; }

    [Serializable] public class GoalJson {
        public string id, name, parentId = "";
        public bool show = true, discarded = false, alwaysHidden = false;
        public string progress, comparison, target; 
        public List<string> prereq = new();
    }

    [Serializable] public class GoalFileWrapper {
        public List<FactJson>  facts  = new();
        public List<RoomJson>  rooms  = new();
        public List<GoalJson>  goals  = new();
    }
}