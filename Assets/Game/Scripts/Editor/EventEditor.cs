#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/*
[CustomEditor(typeof(EventChannel), editorForChildClasses: true)]
public class EventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        EventChannel e = target as EventChannel;
        if (GUILayout.Button("Raise"))
            e.Raise();
    }
}
*/
#endif

