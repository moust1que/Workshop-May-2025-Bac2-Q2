using UnityEngine;
using System.Collections.Generic;

namespace ScriptableObjectArchitecture.Runtime {
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
    public class Inventory : ScriptableObject {

        public static Inventory Instance { get; private set; }

        public class ItemEntry {
            public ItemData type;
            public int count = 1;
        }

        public List<ItemEntry> inventory = new();


        public void Add(ItemData type) {
            var entry = inventory.Find(e => e.type == type && e.count < type.maxStack);
            if (entry != null) entry.count++;
            else               inventory.Add(new ItemEntry { type = type, count = 1 });
        }

        public void RemoveOne(ItemData type) {
            var entry = inventory.Find(e => e.type == type);
            if (entry == null) return;
            entry.count--;
            if (entry.count <= 0) inventory.Remove(entry);
        }

        public void Clear() => inventory.Clear();

        void OnEnable()  => Instance = this;
        void OnDisable() => Instance = null;
        
    }
}