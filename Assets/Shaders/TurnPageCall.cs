using UnityEngine;

public class TurnPageCall : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            BookTurnPage.TurnPageLeft();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            BookTurnPage.TurnPageRight();
        }
    }

}
