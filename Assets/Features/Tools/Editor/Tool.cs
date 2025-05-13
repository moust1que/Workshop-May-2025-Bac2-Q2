using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using CameraManager.Runtime;

namespace Tools.Editor {
    public class Tool : EditorWindow {
        private int tabs = 3;
        private string[] tabOptions = new string[] { "Rooms", "Tab 2", "Tab 3" };
        private int roomsTab = 3;
        private string[] roomsTabOptions = new string[] { "Room 1", "Room 2", "Room 3" };

        #region Room1
            private GameObject room1Root;
            private GameObject room2Root;
            private GameObject room3Root;
        #endregion

        #region MainCharacter
            private Texture2D profilePicture;
            private Vector3 charcterPosition = Vector3.zero;
            private float characterSpeed = 1.0f;
        #endregion

        [MenuItem("Tools/Workshop/Tool")]
        public static void ShowWindow() {
            Tool test = (Tool)GetWindow(typeof(Tool));
            test.minSize = new Vector2(300, 200);
            test.maxSize = new Vector2(500, 1000);
        }

        private void OnGUI() {
            tabs = GUILayout.Toolbar(tabs, tabOptions);

            switch(tabs) {
                case 0:
                    FirstTab();
                    break;
                case 1:
                    SecondTab();
                    break;
                case 2:
                    ThirdTab();
                    break;
            }
        }

        private void FirstTab() {
            roomsTab = GUILayout.Toolbar(roomsTab, roomsTabOptions);

            switch(roomsTab) {
                case 0:
                    Room1();
                    break;
                case 1:
                    Room2();
                    break;
                case 2:
                    Room3();
                    break;
            }
        }

        private void Room1() {
            room1Root = (GameObject)EditorGUILayout.ObjectField("Room 1 Root", GameObject.Find("Room1Destination"), typeof(GameObject), true);

            if(room1Root == null) return;

            DisplayNavigationData(room1Root);
        }

        private void Room2() {
            room2Root = (GameObject)EditorGUILayout.ObjectField("Room 2 Root", GameObject.Find("Room2Destination"), typeof(GameObject), true);

            if(room2Root == null) return;

            DisplayNavigationData(room2Root);
        }

        private void Room3() {
            room3Root = (GameObject)EditorGUILayout.ObjectField("Room 3 Root", GameObject.Find("Room3Destination"), typeof(GameObject), true);

            if(room3Root == null) return;

            DisplayNavigationData(room3Root);
        }

        private void DisplayNavigationData(GameObject room) {
            int childCount = room.transform.childCount;
            for(int i = 0; i < childCount; i++) {
                GameObject child = room.transform.GetChild(i).gameObject;
                    
                NavigationPoint navPoint = child.GetComponent<NavigationPoint>();
                if(navPoint) {
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField($"Navigation Data: {child.name}", EditorStyles.boldLabel);

                    SerializedObject serializedNavPoint = new SerializedObject(navPoint);
                    SerializedProperty navigationDataProperty = serializedNavPoint.FindProperty("navigationData");

                    serializedNavPoint.Update();

                    for(int j = 0; j < navigationDataProperty.arraySize; j++) {
                        SerializedProperty entryProperty = navigationDataProperty.GetArrayElementAtIndex(j);
                        SerializedProperty keyProp = entryProperty.FindPropertyRelative("key");
                        SerializedProperty valueProp = entryProperty.FindPropertyRelative("value");
                        
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(keyProp, GUIContent.none);
                        EditorGUILayout.PropertyField(valueProp, GUIContent.none);

                        if(GUILayout.Button("X", GUILayout.Width(20.0f))) {
                            navigationDataProperty.DeleteArrayElementAtIndex(j);
                        }

                        EditorGUILayout.EndHorizontal();
                    }

                    if(GUILayout.Button($"Add Entry to {child.name}")) {
                        navigationDataProperty.InsertArrayElementAtIndex(navigationDataProperty.arraySize);

                        SerializedProperty newElement = navigationDataProperty.GetArrayElementAtIndex(navigationDataProperty.arraySize - 1);
                    }

                    serializedNavPoint.ApplyModifiedProperties();
                }
            }
        }

        private void SecondTab() {
            profilePicture = Resources.Load<Texture2D>("ProfilePicture");
            if(profilePicture == null) {
                Debug.LogError("Failed to load profile picture. Path: Assets/Features/Tools/Editor/ProfilePicture");
            }
            Rect textureRect = EditorGUILayout.GetControlRect(GUILayout.MaxWidth(100.0f), GUILayout.MaxHeight(100.0f));
            GUI.DrawTexture(textureRect, profilePicture);
            charcterPosition = EditorGUILayout.Vector3Field("Position", charcterPosition);
            characterSpeed = EditorGUILayout.Slider("Speed", characterSpeed, 0.0f, 10.0f);
        }

        private void ThirdTab() {
            GUILayout.Label("Tab 3");
        }
    }
}