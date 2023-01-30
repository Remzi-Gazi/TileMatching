using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VoidEventChannel), editorForChildClasses: true)]
public class EventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        VoidEventChannel e = target as VoidEventChannel;
        if (GUILayout.Button("Raise"))
            e.RaiseEvent();
    }
}

