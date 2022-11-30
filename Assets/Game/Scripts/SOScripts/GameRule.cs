using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameRule",menuName = "Scriptable Objects/GameRule")]
public class GameRule : ScriptableObject
{
    public TileData TileData;

    public int Rows;
    public int Columns;

    public int ColorCount;

    public MinimumMatchCount MinimumMatchCounts;
    public TierData[] matchRanges;

    [Serializable]
    public struct MinimumMatchCount
    {
        public int tierA;
        public int tierB;
        public int tierC;
    }


    [Serializable]
    [Tooltip("Start is inclusive, end is exclusive")]
    public struct TierData
    {
        public int start;
        public int end;

        public string tierName;
    }


}
