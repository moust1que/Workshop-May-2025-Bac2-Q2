using UnityEngine;
using UnityEditor;

namespace Tools.Editor {
    public class EditorTest : EditorWindow {
        private int tabs = 3;
        private string[] tabOptions = new string[] { "Main Character", "Tab 2", "Tab 3" };

        #region MainCharacter
            private Texture2D profilePicture;
            private Vector3 charcterPosition = Vector3.zero;
            private float characterSpeed = 1.0f;
        #endregion

        [MenuItem("Window/Tabs/Test")]
        public static void ShowWindow() {
            EditorTest test = (EditorTest)GetWindow(typeof(EditorTest));
            test.minSize = new Vector2(300, 200);
            test.maxSize = new Vector2(500, 1000);
        }

        private void OnGUI() {
            tabs = GUILayout.Toolbar(tabs, tabOptions);

            switch (tabs) {
                case 0:
                    firstTab();
                    break;
                case 1:
                    secondTab();
                    break;
                case 2:
                    thirdTab();
                    break;
            }
        }

        private void firstTab() {
            profilePicture = Resources.Load<Texture2D>("ProfilePicture");
            if(profilePicture == null) {
                Debug.LogError("Failed to load profile picture. Path: Assets/Features/Tools/Editor/ProfilePicture");
            }
            Rect textureRect = EditorGUILayout.GetControlRect(GUILayout.MaxWidth(100.0f), GUILayout.MaxHeight(100.0f));
            GUI.DrawTexture(textureRect, profilePicture);
            charcterPosition = EditorGUILayout.Vector3Field("Position", charcterPosition);
            characterSpeed = EditorGUILayout.Slider("Speed", characterSpeed, 0.0f, 10.0f);
        }

        private void secondTab() {
            GUILayout.Label("Tab 2");
        }

        private void thirdTab() {
            GUILayout.Label("Tab 3");
        }
    }
}