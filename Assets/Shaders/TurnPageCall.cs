using UnityEngine;

public class TurnPageCall : MonoBehaviour
{
    [SerializeField] private BookTurnPage call;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            call.TurnPageLeft();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            call.TurnPageRight();
        }
    }

}
