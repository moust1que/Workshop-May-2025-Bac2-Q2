using UnityEngine;
using UnityEditor;

namespace Tools.Editor {
    public class EditorTest : EditorWindow {
        private int tabs = 3;
        private string[] tabOptions = new string[] { "Tab 1", "Tab 2", "Tab 3" };
        private Vector3 charcterPosition = Vector3.zero;

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
            GUILayout.Label("Tab 1");
            charcterPosition = EditorGUILayout.Vector3Field("Position", charcterPosition);
        }

        private void secondTab() {
            GUILayout.Label("Tab 2");
        }

        private void thirdTab() {
            GUILayout.Label("Tab 3");
        }
    }
}