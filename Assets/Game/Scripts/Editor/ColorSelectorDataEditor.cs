#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ColorSelectorData), editorForChildClasses: true)]
public class ColorSelectorDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        

        ColorSelectorData colorSelectorData = target as ColorSelectorData;
        
        
        
        if (GUILayout.Button("UpdateDatabase"))
            colorSelectorData.UpdateDatabase();
    }
}

#endif

