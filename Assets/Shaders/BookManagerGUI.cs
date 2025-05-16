using UnityEngine;

public class BookManagerGUI : MonoBehaviour
{
    [SerializeField] private GameObject openBookButton;
    [SerializeField] private GameObject closeBookButton;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;
    [SerializeField] private BookTurnPage turnPage;


    private int curPage = 0;

    public void OpenBook()
    {
        openBookButton.SetActive(false);
        closeBookButton.SetActive(true);
        curPage = 0;
        UpdateButtonDisplay();
        turnPage.ResetPages();

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
        turnPage.TurnPageRight();

    }

    public void TurnPageRight()
    {
        curPage = Mathf.Min(6, curPage + 1);
        UpdateButtonDisplay();
        
        turnPage.TurnPageLeft();
    }

    private void UpdateButtonDisplay()
    {
        leftButton.SetActive(curPage > 0);
        rightButton.SetActive(curPage < 6);
    }
}