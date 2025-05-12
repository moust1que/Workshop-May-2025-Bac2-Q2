using UnityEngine;

namespace Save.Runtime {
    public class Save {
        // private static string fileName = "Save.txt";
        // public static DictionaryVariable factDictionary;

        // public static void SaveToFile() {
        //     if(File.Exists(fileName)) File.Delete(fileName);

        //     List<StringFact> serializableFacts = factDictionary.ToSerializableList();

        //     FileStream stream = File.Create(fileName);
        //     StreamWriter writer = new StreamWriter(stream);
            
        //     foreach(StringFact pair in serializableFacts) {
        //         writer.WriteLine($"{pair.key}:{pair.factType},{pair.value},{pair.isPersistent}");
        //     }

        //     writer.Close();
        //     stream.Close();

        //     factDictionary.Clear();
        //     Debug.Log(factDictionary.facts.Count);
        // }

        // public static void LoadFromFile() {
        //     if(!File.Exists(fileName)) return;

        //     StreamReader reader = new StreamReader(fileName);
        //     string data = reader.ReadToEnd();
        //     List<string> lines = new List<string>(data.Split('\n'));
        //     List<StringFact> entries = new List<StringFact>();

        //     foreach(string line in lines) {
        //         if (string.IsNullOrWhiteSpace(line)) continue;
                
        //         string[] parts = line.Split(':');
        //         string[] values = parts[1].Split(',');
        //         Debug.Log(values[0] + ", " + values[1] + ", " + values[2]);
        //         entries.Add(new StringFact { key = parts[0], factType = values[0], value = values[1], isPersistent = bool.Parse(values[2]) });
        //     }

        //     factDictionary.LoadFromSerializableList(entries);

        //     reader.Close();

        //     foreach(KeyValuePair<string, IFact> pair in factDictionary.facts) {
        //         Debug.Log(pair.Key + ": " + pair.Value.type + ", " + pair.Value.Value + ", " + pair.Value.IsPersistent);
        //     }
        // }

        // public static bool Exists() {
        //     if(!File.Exists(fileName)) return false;
        //     else return true;
        // }
    }
}