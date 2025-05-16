using UnityEngine;
using CameraManager.Runtime;

namespace PlayerMovement.Runtime
{
    using System.Collections.Generic;
    using BBehaviour.Runtime;

    public class PlayerMovementManager : BBehaviour
    {
        public GameObject ui;
        public GameObject backArrow;
        public GameObject leftArrow;
        public GameObject rightArrow;

        [SerializeField] private Camera mainCamera;

        public NavigationPoint CurrentNavigationPoint;
        private List<NavigationPoint.NavEntry> curNavEntries = new();

        public static PlayerMovementManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void DisplayUI()
        {
            ui.SetActive(true);
            UpdateDisplacementAvailable();
        }

        public void HideUI()
        {
            ui.SetActive(false);
        }

        public void UpdateDisplacementAvailable()
        {
            curNavEntries.Clear();

            foreach (NavigationPoint.NavEntry entry in CurrentNavigationPoint.navigationData)
            {
                curNavEntries.Add(entry);
            }

            backArrow.SetActive(CurrentNavigationPoint.HasDirection(Directions.Backward));
            leftArrow.SetActive(CurrentNavigationPoint.HasDirection(Directions.Left));
            rightArrow.SetActive(CurrentNavigationPoint.HasDirection(Directions.Right));
        }

        public void OnClickBack()
        {
            TryTeleport(Directions.Backward);
        }

        public void OnClickLeft()
        {
            TryTeleport(Directions.Left);
        }

        public void OnClickRight()
        {
            TryTeleport(Directions.Right);
        }

        private void TryTeleport(Directions direction)
        {
            var navEntry = curNavEntries.Find(x => x.key == direction);
            if(navEntry.value != null)
            {
                TeleportToPosition(navEntry);
            }else {
                Verbose($"No navigation entry found for direction {direction}", VerboseType.Warning);
            }
        }

        public void TeleportToPosition(NavigationPoint.NavEntry navEntry)
        {
            Verbose($"Teleporting to {navEntry.value.name}");
            mainCamera.transform.position = curNavEntries.Find(x => x.key == navEntry.key).value.position;
            mainCamera.transform.rotation = curNavEntries.Find(x => x.key == navEntry.key).value.rotation;

            CurrentNavigationPoint = navEntry.value.GetComponent<NavigationPoint>();
            UpdateDisplacementAvailable();
        }
    }
}