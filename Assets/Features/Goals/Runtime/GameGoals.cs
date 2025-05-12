using System;
using ScriptableObjectArchitecture.Runtime;

namespace Goals.Runtime
{
    /// <summary>Décrit un objectif générique : la valeur courante est dans Progress.</summary>
    [Serializable]
    public class Goal{
        public string Id;
        public string Name;

        public bool Show = true;
        public bool Completed = false;
        public bool Discarded = false;

        public string ParentId  = "";          // chaîne vide = racine
        public IFact Progress;                // Fact<int>, Fact<bool>, Fact<float>, etc.

        public string Comparison = "==";       // "==", ">=", "<", …
        public string Target = "0";        // valeur-cible en texte

        public string[] Prereq = Array.Empty<string>();

        /*------------------------------------------------------------------*/
        /*  ÉVALUATION                                                       */
        /*------------------------------------------------------------------*/
        public bool Evaluate(){
            if (Discarded || Progress == null) return false;

            object cur = Progress.Value;
            object tgt = ConvertText(Progress.type, Target);
            if (cur == null || tgt == null)    return false;

            if (Progress.type == typeof(bool))
                return Compare((bool)cur, (bool)tgt);

            // Tous les autres types numériques -> double
            double a, b;
            try { a = Convert.ToDouble(cur); b = Convert.ToDouble(tgt); }
            catch { return false; }

            return Compare(a, b);
        }

        private bool Compare<T>(T a, T b) where T : IComparable => Comparison switch{
            "==" => a.CompareTo(b) == 0,
            "!=" => a.CompareTo(b) != 0,
            "<"  => a.CompareTo(b) <  0,
            ">"  => a.CompareTo(b) >  0,
            "<=" => a.CompareTo(b) <= 0,
            ">=" => a.CompareTo(b) >= 0,
            _    => false
        };

        private static object ConvertText(Type t, string txt){
            try{
                if (t == typeof(bool))   return bool.Parse(txt);
                if (t == typeof(int))    return int.Parse(txt);
                if (t == typeof(float))  return float.Parse(txt);
                if (t == typeof(double)) return double.Parse(txt);
            }
            catch { }
            UnityEngine.Debug.LogWarning($"Goal: impossible de convertir «{txt}» en {t.Name}");
            return null;
        }
    }
}
