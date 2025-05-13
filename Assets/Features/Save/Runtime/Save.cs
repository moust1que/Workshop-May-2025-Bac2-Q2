using UnityEngine;
using ScriptableObjectArchitecture.Runtime;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

namespace Save.Runtime {
    public class Save {
        public static DictionaryVariable factDictionary;
        public static List<Button> buttons = new();

        private static string directoryPath = Path.Combine(Application.dataPath, "saves");

        public static void SaveToFile(string slotName) {
            if(!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            string fileName = Path.Combine(directoryPath, slotName);

            if(File.Exists(fileName)) File.Delete(fileName);

            List<StringFact> serializableFacts = factDictionary.ToSerializableList();

            FileStream stream = File.Create(fileName);
            StreamWriter writer = new StreamWriter(stream);
            
            foreach(StringFact pair in serializableFacts) {
                if(pair.isPersistent == false) continue;

                writer.WriteLine($"{pair.key}:{pair.factType},{pair.value},{pair.isPersistent}");
            }

            writer.Close();
            stream.Close();

            factDictionary.Clear();
        }

        public static void LoadFromFile(string slotName) {
            string fileName = Path.Combine(directoryPath, slotName);

            if(!File.Exists(fileName)) return;

            StreamReader reader = new StreamReader(fileName);
            string data = reader.ReadToEnd();
            List<string> lines = new List<string>(data.Split('\n'));
            List<StringFact> entries = new List<StringFact>();

            foreach(string line in lines) {
                if (string.IsNullOrWhiteSpace(line)) continue;
                
                string[] parts = line.Split(':');
                string[] values = parts[1].Split(',');
                Debug.Log(values[0] + ", " + values[1] + ", " + values[2]);
                entries.Add(new StringFact { key = parts[0], factType = values[0], value = values[1], isPersistent = bool.Parse(values[2]) });
            }

            factDictionary.LoadFromSerializableList(entries);

            reader.Close();

            foreach(KeyValuePair<string, IFact> pair in factDictionary.facts) {
                Debug.Log(pair.Key + ": " + pair.Value.type + ", " + pair.Value.Value + ", " + pair.Value.IsPersistent);
            }
        }

        public static bool ExistInSlot(string slotName) {
            if(!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            string fileName = Path.Combine(directoryPath, slotName);

            if(!File.Exists(fileName)) return false;
            else return true;
        }

        public static bool Exists() {
            if(!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            foreach(Button button in buttons) {
                string fileName = Path.Combine(directoryPath, button.name);
                if(File.Exists(fileName)) return true;
            }

            return false;
        }
    }
}