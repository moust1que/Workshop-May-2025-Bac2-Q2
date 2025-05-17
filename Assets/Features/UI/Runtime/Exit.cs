using UnityEngine;

namespace UI.Runtime
{
    public class Exit : MonoBehaviour
    {
        public void ExitGame()
        {
            Debug.Log("Exiting game");
            Application.Quit();
        }

    }
}
