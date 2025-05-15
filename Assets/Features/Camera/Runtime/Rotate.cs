using UnityEngine;

namespace CameraManager.Runtime
{
    public class Rotate : MonoBehaviour
    {
        public float stepAngle = 90f;     // taille d'un cran
        public bool invertPitch = false;  // coche si ↑ doit regarder vers le bas

        void Update ()
        {
            // GAUCHE / DROITE  → Yaw (axe Y)
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                transform.Rotate(0f, -stepAngle, 0f, Space.World);

            if (Input.GetKeyDown(KeyCode.RightArrow))
                transform.Rotate(0f,  stepAngle, 0f, Space.World);

            // HAUT / BAS → Pitch (axe X)
            if (Input.GetKeyDown(KeyCode.UpArrow))
                transform.Rotate(invertPitch ?  stepAngle : -stepAngle, 0f, 0f, Space.Self);

            if (Input.GetKeyDown(KeyCode.DownArrow))
                transform.Rotate(invertPitch ? -stepAngle :  stepAngle, 0f, 0f, Space.Self);
        }
    }
}
