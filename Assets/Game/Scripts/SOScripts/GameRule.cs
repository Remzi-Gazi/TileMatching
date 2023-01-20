using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameRule",menuName = "Scriptable Objects/GameRule")]
public class GameRule : ScriptableObject
{
    public int Rows;
    public int Columns;

    public int ColorCount;

    public TierData[] TierData;
}

[Serializable]
public class TierData
{
    public int LowerMatchLimit;
    public string TierName;
}
