using UnityEngine;
using PlayerData.Runtime;

namespace Shuriken.Runtime {
    using BBehaviour.Runtime;
    public class ShurikenEnigma : BBehaviour {

        public RotateCylinder[] cylinders;
        public bool allCorrect = false;

        [SerializeField] private KatanaTracker katanaTracker;

        bool canResolve = false;

        public WindowOpening window;
        public GameObject kanji;

        void Start() => CheckPuzzle();

        public void CheckPuzzle() {
            if (!canResolve) return;
            bool solved = true;

            foreach (RotateCylinder c in cylinders) {
                if (!c.IsCorrect(tolerance: 1f)) {
                    solved = false;
                    break;
                }
            }

            if (solved != allCorrect) {
                allCorrect = solved;

                if (allCorrect) {
                    Verbose("Code correct, énigme ouverte.");
                }
                else {
                    Verbose("Code incorrect");
                }  
            }
        }

        void Update() {
            if(!canResolve && window.IsOpen && katanaTracker.IsKatanaPlaced) {
                kanji.SetActive(true);
                canResolve = true;
                CheckPuzzle();
            }
            if(canResolve&& !window.IsOpen){
                canResolve = false;
                kanji.SetActive(false);
            }
        }
    }
}
