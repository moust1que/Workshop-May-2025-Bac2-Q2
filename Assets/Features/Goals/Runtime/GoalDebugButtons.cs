/*  Assets/Scripts/Goals/GoalDebugButtons.cs  */
using UnityEngine;
using Goals.Runtime;          // pour GoalLoader

/// <summary>
/// Deux boutons OnGUI en bas-gauche pour tester les goals.
/// </summary>
public class GoalDebugButtons : MonoBehaviour{
    [SerializeField] private GoalLoader goalLoader;   // glisse GoalSystem ou laisse vide (auto-find)

    private const int BTN_W = 120;
    private const int BTN_H = 28;
    private const int MARGIN = 10;

    private void Awake(){
        // Si on n’a pas mis la référence, on cherche sur le même GameObject
        if (goalLoader == null) goalLoader = GetComponent<GoalLoader>();
        if (goalLoader == null)
            Debug.LogError("GoalDebugButtons : GoalLoader introuvable !");
    }

    private void OnGUI(){
        if (goalLoader == null) return;

        float x = MARGIN;
        // point de départ en bas : hauteur totale = 2 boutons + marge
        float yStart = Screen.height - (BTN_H * 2 + MARGIN * 3);

        // --- Kill +1 --------------------------------------------------------
        if (GUI.Button(new Rect(x, yStart, BTN_W, BTN_H), "Kill +1"))
            goalLoader.Increment("KillFact", 1);

        // --- Find Key -------------------------------------------------------
        if (GUI.Button(new Rect(x, yStart + BTN_H + MARGIN, BTN_W, BTN_H), "Find Key"))
            goalLoader.SetBool("KeyFound", true);
    }
}
