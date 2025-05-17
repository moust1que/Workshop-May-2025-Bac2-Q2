using UnityEngine;

namespace Shuriken.Runtime
{
    public class Ladder : MonoBehaviour
    {
        public BoxCollider ladder;

        public ShurikenEnigma enigma;

        public void Start()
        {
            ladder.enabled = false;
        }

        public void Update()
        {
            if (enigma.allCorrect)
            {
                ladder.enabled = true;
            }
        }

        public void Hoverable()
        {
            if (ladder.enabled == true)
            {
                gameObject.tag = "Hoverable";
            }
            
        }
    }
}
