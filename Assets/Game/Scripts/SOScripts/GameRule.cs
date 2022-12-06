using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameRule",menuName = "Scriptable Objects/GameRule")]
public class GameRule : ScriptableObject
{
    public int Rows;
    public int Columns;

    public int ColorCount;

    public TierData[] TierDatas;

    [Serializable]
    public struct TierData
    {
        public int lowerMatchLimit;
        public string tierName;
        public Sprite[] sprites;
    }
}
