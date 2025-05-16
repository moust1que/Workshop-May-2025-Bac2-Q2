using UnityEngine;

namespace Door.Runtime {
    using BBehaviour.Runtime;

    public class DoorCloseAuto : BBehaviour
    {
        [SerializeField] private Transform door;
        [SerializeField] private Transform targetPivot;

        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private AudioSource closeSound;

        private bool isClosing = false;
        private bool hasPlayedSound = false;

        public void TriggerClose()
        {
            if (door == null || targetPivot == null) return;

            isClosing = true;
            hasPlayedSound = false;
        }

        void Update()
        {
            if (!isClosing || door == null || targetPivot == null) return;

            door.position = Vector3.MoveTowards(door.position, targetPivot.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(door.position, targetPivot.position) < 0.01f)
            {
                door.position = targetPivot.position;
                isClosing = false;
                closeSound.PlayOneShot(closeSound.clip);
                hasPlayedSound = true;
            }
        }
    }
}