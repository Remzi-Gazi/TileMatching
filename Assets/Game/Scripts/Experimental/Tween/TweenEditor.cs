#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TweenBase), editorForChildClasses: true)]
//[CanEditMultipleObjects]
public class TweenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TweenBase e = target as TweenBase;
        if (GUILayout.Button("Use Default Values"))
            e.SetDefaults();
    }
}
#endif
