using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace BookShelf {
    using BBehaviour.Runtime;
    public class BookShelfEnigma : BBehaviour {
        [Header("Tous les livres (y compris ceux hors séquence)")]
        public Book[] books;

        [Header("Séquence attendue (par index)")]
        public int[] correctOrder;

        public bool canResolve = true;
        public bool allCorrect = false;

        private List<int> placedOrder = new List<int>();
        


        public void OnBookPlaced(Book b) {
            if (!canResolve || allCorrect) 
                return;

            placedOrder.Add(b.orderIndex);

            if (placedOrder.Count == correctOrder.Length) {
                CheckPuzzle();
            }
        }

        private void CheckPuzzle() {
            bool solved = true;

            for (int i = 0; i < correctOrder.Length; i++) {
                if (placedOrder[i] != correctOrder[i]) {
                    solved = false;
                    break;
                }
            }

            if (solved) {
                allCorrect = true;
                Verbose("Code correct, énigme ouverte");
                //here 
            }
            else {
                Verbose("Code incorrect");
                ResetAll();
            }
        }

        private void ResetAll() {
            foreach (Book book in books) {
                book.ResetPosition();
            }
            placedOrder.Clear();
        }
    }
}
