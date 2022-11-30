 using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "MapSerializationTest", menuName = "MapSerializationTest")]
public class MapSerializationTest : ScriptableObject
{
    public Dictionary<int, string> keyValuePairs;

    public List<int> keys;
    public List<string> values;

    /*public void OnEnable()
    {
        keyValuePairs = new Dictionary<int, string>();
        for (int i = 0; i < keys.Count; i++)
        {
            keyValuePairs.Add(keys[i], values[i]);
        }
    }*/





}