#if UNITY_EDITOR
/*using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MapSerializationTest), editorForChildClasses: true)]
public class MapSerializerEditor : Editor
{
    bool _firstDraw = true;

    int maxKey = -1;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapSerializationTest e = target as MapSerializationTest;

        if(e.keys != null)
        {
            for(int i = 0; i < e.keys.Count; i++)
            {



            }


        }


        if (GUILayout.Button("Add"))
        {
            e.keyValuePairs.Add(maxKey + 1, "EmptyString");
        }


    }
}*/

#endif

