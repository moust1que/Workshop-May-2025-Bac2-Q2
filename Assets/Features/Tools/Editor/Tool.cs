using UnityEngine;
using UnityEditor;
using CameraManager.Runtime;
using Goals.Runtime;
using System.Collections.Generic;

namespace Tools.Editor {
    public class Tool : EditorWindow {
        private int tabs = 3;
        private string[] tabOptions = new string[] { "Rooms", "PlayerData", "Tab 3" };
        private int roomsTab = 3;
        private string[] roomsTabOptions = new string[] { "Room 1", "Room 2", "Room 3" };

        #region Rooms
            private GameObject room1Root;
            private GameObject room2Root;
            private GameObject room3Root;
        #endregion

        #region PlayerData
            private bool showGoals = false;
            private string status = "Player Goals";
            private Dictionary<string, bool> goalFoldoutStates = new();
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
            showGoals = EditorGUILayout.BeginFoldoutHeaderGroup(showGoals, status);
            if(showGoals) {
                EditorGUI.indentLevel++;
                if(GoalsManager.instance == null) {
                    EditorGUILayout.LabelField("Enter Play Mode to load Goals.");
                }else {
                    foreach(Goal goal in GoalsManager.instance.goals.Values) {
                        string goalKey = goal.Id;

                        if(!goalFoldoutStates.ContainsKey(goalKey)) 
                            goalFoldoutStates[goalKey] = false;

                        goalFoldoutStates[goalKey] = EditorGUILayout.Foldout(goalFoldoutStates[goalKey], "Goal: " + goal.Id);

                        if(goalFoldoutStates[goalKey]) {
                            EditorGUI.indentLevel++;

                            GUILayout.BeginVertical("box");

                            EditorGUILayout.LabelField("ID", goal.Id);
                            EditorGUILayout.LabelField("Name", goal.Name);

                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("Completed", goal.Completed.ToString());
                            EditorGUILayout.LabelField("Discarded", goal.Discarded.ToString());
                            EditorGUILayout.LabelField("Always Hidden", goal.AlwaysHidden.ToString());
                            EditorGUILayout.EndHorizontal();

                            if (!string.IsNullOrEmpty(goal.ParentId))
                                EditorGUILayout.LabelField("Parent ID", goal.ParentId);

                            EditorGUILayout.Space(3);
                            EditorGUILayout.LabelField("Evaluation");
                            EditorGUI.indentLevel++;
                            EditorGUILayout.LabelField("Comparison", goal.Comparison);
                            EditorGUILayout.LabelField("Target", goal.Target);
                            EditorGUILayout.LabelField("Progress Type", goal.Progress?.type?.Name ?? "null");
                            EditorGUILayout.LabelField("Progress Value", goal.Progress?.Value?.ToString() ?? "null");
                            EditorGUILayout.LabelField("Evaluates To", goal.Evaluate().ToString());
                            EditorGUI.indentLevel--;

                            if (goal.Prereq != null && goal.Prereq.Length > 0) {
                                EditorGUILayout.Space(3);
                                EditorGUILayout.LabelField("Prerequisites");
                                EditorGUI.indentLevel++;
                                foreach (string prereq in goal.Prereq) {
                                    EditorGUILayout.LabelField("- " + prereq);
                                }
                                EditorGUI.indentLevel--;
                            }

                            GUILayout.EndVertical();

                            EditorGUI.indentLevel--;
                        }
                    }
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUILayout.Space();


            // profilePicture = Resources.Load<Texture2D>("ProfilePicture");
            // if(profilePicture == null) {
            //     Debug.LogError("Failed to load profile picture. Path: Assets/Features/Tools/Editor/ProfilePicture");
            // }
            // Rect textureRect = EditorGUILayout.GetControlRect(GUILayout.MaxWidth(100.0f), GUILayout.MaxHeight(100.0f));
            // GUI.DrawTexture(textureRect, profilePicture);
            // charcterPosition = EditorGUILayout.Vector3Field("Position", charcterPosition);
            // characterSpeed = EditorGUILayout.Slider("Speed", characterSpeed, 0.0f, 10.0f);
        }

        private void ThirdTab() {
            GUILayout.Label("Tab 3");
        }
    }
}