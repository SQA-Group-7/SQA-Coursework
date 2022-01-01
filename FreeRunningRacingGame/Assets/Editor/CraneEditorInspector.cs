using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CraneController))]
public class CraneEditorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CraneController controller = (CraneController)target;
        if (GUILayout.Button("Increase Crane Height"))
        {
            controller.IncreaseHeight();
        }
        else if (GUILayout.Button("Decrease Crane Height"))
        {
            controller.DecreaseHeight();
        }
        /*
        else if (GUILayout.Button("Set Status to Still"))
        {
            controller.SetToStill();
        }
        else if (GUILayout.Button("Set Status to Rotate Crane"))
        {
            controller.SetToRotateCrane();
        }
        else if (GUILayout.Button("Set Status to Rotate Load"))
        {
            controller.SetToRotateLoad();
        }
        else if (GUILayout.Button("Set Status to MoveLoad"))
        {
            controller.SetToMoveLoad();
        }
        EditorGUILayout.LabelField("Crane Stauts: " + controller.cranestatus);
        */
    }
}
