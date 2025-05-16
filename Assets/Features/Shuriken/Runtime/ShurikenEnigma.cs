using UnityEngine;
using PlayerData.Runtime;

namespace Shuriken.Runtime {
    using BBehaviour.Runtime;
    public class ShurikenEnigma : BBehaviour
    {

        public RotateCylinder[] cylinders;
        public bool allCorrect = false;

        [SerializeField] private KatanaTracker katanaTracker;

        bool canResolve = false;

        public WindowOpening window;
        public GameObject kanji;

        public GameObject moon1;
        public GameObject moon2;


        void Start() => CheckPuzzle();

        public void CheckPuzzle()
        {
            if (!canResolve) return;
            bool solved = true;

            foreach (RotateCylinder c in cylinders)
            {
                if (!c.IsCorrect(tolerance: 1f))
                {
                    solved = false;
                    break;
                }
            }

            if (solved != allCorrect)
            {
                allCorrect = solved;

                if (allCorrect)
                {
                    Verbose("Code correct, énigme ouverte.");
                    SwitchMoon();
                }
                else
                {
                    Verbose("Code incorrect");
                }
            }
        }

        void Update()
        {
            Verbose($"canResolve={canResolve} window.IsOpen={window.IsOpen} katanaPlaced={katanaTracker.IsKatanaPlaced}");

            if (!canResolve && window.IsOpen && katanaTracker.IsKatanaPlaced)
            {
                kanji.SetActive(true);
                canResolve = true;
                CheckPuzzle();
            }
            else if (!canResolve && !window.IsOpen)   // <-- else if, pas if
            {
                canResolve = false;
                kanji.SetActive(false);
            }
        }
        
        void SwitchMoon(){
            moon1.SetActive(false);
            moon2.SetActive(true);
        }
    }
}
