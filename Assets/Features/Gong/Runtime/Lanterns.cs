using UnityEngine;

namespace Gong.Runtime
{
    public class Lanterns : MonoBehaviour
    {
        Vector3 strartPos;
        public float speed = 2f;
        public Transform targetLocation;

        public bool canMove = false;

        void Awake() => strartPos = transform.position;

        void Update()
        {
            if (canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, speed * Time.deltaTime);
            }
        }

    }
}
