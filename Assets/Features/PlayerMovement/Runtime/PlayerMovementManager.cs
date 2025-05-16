using UnityEngine;
using CameraManager.Runtime;
using Events.Runtime;

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
        private NavigationPoint lastNavigationPoint;

        public static PlayerMovementManager instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            GameEvents.OnPlayerMoved += OnPlayerMoved;
        }

        private void OnDestroy()
        {
            GameEvents.OnPlayerMoved -= OnPlayerMoved;
        }

        public void DisplayUI()
        {
            ui.SetActive(true);
            UpdateDisplacementAvailable();

            if (lastNavigationPoint != null && lastNavigationPoint != CurrentNavigationPoint && lastNavigationPoint.objectsToEnableOnArrival != null)
            {
                foreach (GameObject obj in lastNavigationPoint.objectsToEnableOnArrival)
                {
                    if (obj != null)
                    {
                        BoxCollider col = obj.GetComponent<BoxCollider>();
                        if (col != null)
                            col.enabled = false;
                        else
                            Verbose($"No BoxCollider found on {obj.name}", VerboseType.Warning);
                    }
                }
            }

            if (CurrentNavigationPoint != null && CurrentNavigationPoint.objectsToEnableOnArrival != null)
            {
                foreach (GameObject obj in CurrentNavigationPoint.objectsToEnableOnArrival)
                {
                    if (obj != null)
                    {
                        BoxCollider col = obj.GetComponent<BoxCollider>();
                        if (col != null)
                            col.enabled = true;
                        else
                            Verbose($"No BoxCollider found on {obj.name}", VerboseType.Warning);
                    }
                }
                lastNavigationPoint = CurrentNavigationPoint;
            }
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

        public void TryTeleport(Directions direction)
        {
            var navEntry = curNavEntries.Find(x => x.key == direction);
            if (navEntry.value != null)
            {
                TeleportToPosition(navEntry);
            }
            else
            {
                Verbose($"No navigation entry found for direction {direction}", VerboseType.Warning);
            }
        }

        public void TeleportToPosition(NavigationPoint.NavEntry navEntry)
        {
            NavigationPoint previousNavPoint = CurrentNavigationPoint;

            CurrentNavigationPoint = navEntry.value.GetComponent<NavigationPoint>();

            if (previousNavPoint != null && previousNavPoint.objectsToEnableOnArrival != null)
            {
                foreach (GameObject obj in previousNavPoint.objectsToEnableOnArrival)
                {
                    if (obj != null)
                    {
                        BoxCollider col = obj.GetComponent<BoxCollider>();
                        if (col != null)
                            col.enabled = false;
                        else
                            Verbose($"No BoxCollider found on {obj.name}", VerboseType.Warning);
                    }
                }
            }

            Verbose($"Teleporting to {navEntry.value.name}");
            mainCamera.transform.position = curNavEntries.Find(x => x.key == navEntry.key).value.position;
            mainCamera.transform.rotation = curNavEntries.Find(x => x.key == navEntry.key).value.rotation;

            if (CurrentNavigationPoint != null && CurrentNavigationPoint.objectsToEnableOnArrival != null)
            {
                foreach (GameObject obj in CurrentNavigationPoint.objectsToEnableOnArrival)
                {
                    if (obj != null)
                    {
                        BoxCollider col = obj.GetComponent<BoxCollider>();
                        if (col != null)
                            col.enabled = true;
                        else
                            Verbose($"No BoxCollider found on {obj.name}", VerboseType.Warning);
                    }
                }
            }

            UpdateDisplacementAvailable();
            lastNavigationPoint = CurrentNavigationPoint;
        }


        public void OnPlayerMoved(Transform transform)
        {
            NavigationPoint newNavPoint = transform.GetComponent<NavigationPoint>();
            if (newNavPoint == null)
            {
                Verbose("OnPlayerMoved: No NavigationPoint found on transform", VerboseType.Warning);
                return;
            }

            if (CurrentNavigationPoint != null && CurrentNavigationPoint.objectsToEnableOnArrival != null)
            {
                foreach (GameObject obj in CurrentNavigationPoint.objectsToEnableOnArrival)
                {
                    if (obj != null)
                    {
                        BoxCollider col = obj.GetComponent<BoxCollider>();
                        if (col != null)
                            col.enabled = false;
                        else
                            Verbose($"No BoxCollider found on {obj.name}", VerboseType.Warning);
                    }
                }
            }

            CurrentNavigationPoint = newNavPoint;

            if (CurrentNavigationPoint.objectsToEnableOnArrival != null)
            {
                foreach (GameObject obj in CurrentNavigationPoint.objectsToEnableOnArrival)
                {
                    if (obj != null)
                    {
                        BoxCollider col = obj.GetComponent<BoxCollider>();
                        if (col != null)
                            col.enabled = true;
                        else
                            Verbose($"No BoxCollider found on {obj.name}", VerboseType.Warning);
                    }
                }
            }

            lastNavigationPoint = CurrentNavigationPoint;
            UpdateDisplacementAvailable();
            ui.SetActive(true);
            Verbose("OnPlayerMoved: UI and colliders updated.");
        }
    }
}