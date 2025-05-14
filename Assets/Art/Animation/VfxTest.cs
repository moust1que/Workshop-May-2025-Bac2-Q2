using UnityEngine;

public class VfxTest : MonoBehaviour
{
    

    
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
        {
            BookAnimationEvent.PlayBookAnimation();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BookAnimationEvent.EndBookAnimation();
        }
    }
}
