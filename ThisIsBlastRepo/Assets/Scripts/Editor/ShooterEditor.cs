using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Shooter))]
public class ShooterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Shooter shooter = (Shooter)target;

        GUILayout.Space(10);

        if (GUILayout.Button("🔥 ACTIVATE SHOOTER"))
        {
            shooter.Activate();
        }
    }
}
