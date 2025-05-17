using UnityEngine;

namespace Gong.Runtime
{
    using Attribute.Runtime;
    public class Lanterns : MonoBehaviour
    {
        Vector3 strartPos;
        public float speed = 2f;
        public Transform targetLocation;

        public GameObject End;

        public bool canMove = false;

        void Awake()
        {
            transform.position = strartPos;
            End.SetActive(false);
        }

        void Update()
        {
            if (canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, speed * Time.deltaTime);
                DelayManager.instance.Delay(5.0f, () =>  End.SetActive(true));
            }
        }

    }
}
