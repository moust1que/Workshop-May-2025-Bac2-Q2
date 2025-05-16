using UnityEngine;

namespace Shuriken.Runtime
{
    public class Ladder : MonoBehaviour
    {
        public BoxCollider ladder;

        public ShurikenEnigma enigma;

        public void Start()
        {
            ladder.enabled = true;
        }

        public void Update()
        {
            // if (enigma.allCorrect)
            // {
            //     ladder.enabled = true;
            // }
        }
    }
}
