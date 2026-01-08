using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Shooter))]
public class ShooterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Shooter shooter = (Shooter)target;

        if(shooter.currentState != null)
        {
            GUILayout.TextArea("STATE: " + shooter.currentState.ToString());
        }
        

        DrawDefaultInspector();

        

        GUILayout.Space(10);

        if (GUILayout.Button("🔥 ACTIVATE SHOOTER"))
        {
            shooter.ChangeState(new ActiveState());
        }
    }
}
