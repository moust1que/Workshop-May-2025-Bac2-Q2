using UnityEngine;

public class BookAnimationEvent : MonoBehaviour
{
    [SerializeField] private Animator bookAnimator;
    
    public static Animator rootAnimator;
    
    [SerializeField] private GameObject page;
    private void Start()
    {
        rootAnimator = GetComponent<Animator>();
        //DisabelPage();
        if (rootAnimator == null)
        {
            Debug.Log("Root Animator not assign");
        }
    }
    public void OpenBook()
    {
        bookAnimator.SetTrigger("Open");
        bookAnimator.ResetTrigger("Close");

        
    }
    public void CloseBook()
    {
        bookAnimator.SetTrigger("Close");
        bookAnimator.ResetTrigger("Open");
       // DisabelPage();
    }

    public void EnablePage()
    {
        //page.SetActive(true);
    }
    public void DisabelPage()
    {
       // page.SetActive(false);
    }
    public static void PlayBookAnimation()
    {
        rootAnimator.SetTrigger("Come");
        rootAnimator.ResetTrigger("Leave");
    }

    public static void EndBookAnimation()
    {
        rootAnimator.SetTrigger("Leave");
        rootAnimator.ResetTrigger("Come");
       
    }
}
