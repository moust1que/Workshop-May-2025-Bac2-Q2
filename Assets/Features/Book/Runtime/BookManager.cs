using UnityEngine;

namespace Book.Runtime {
    using BBehaviour.Runtime;

    public class BookManager : BBehaviour
    {
        [SerializeField] private GameObject openBookButton;
        [SerializeField] private GameObject closeBookButton;
        [SerializeField] private GameObject leftButton;
        [SerializeField] private GameObject rightButton;

        private int curPage = 0;

        public void OpenBook()
        {
            openBookButton.SetActive(false);
            closeBookButton.SetActive(true);
            curPage = 0;
            UpdateButtonDisplay();
        }

        public void CloseBook()
        {
            openBookButton.SetActive(true);
            closeBookButton.SetActive(false);
            leftButton.SetActive(false);
            rightButton.SetActive(false);
        }

        public void TurnPageLeft()
        {
            curPage = Mathf.Max(0, curPage - 1);
            UpdateButtonDisplay();
        }

        public void TurnPageRight()
        {
            curPage = Mathf.Min(3, curPage + 1);
            UpdateButtonDisplay();
        }

        private void UpdateButtonDisplay()
        {
            leftButton.SetActive(curPage > 0);
            rightButton.SetActive(curPage < 3);
        }
    }
}