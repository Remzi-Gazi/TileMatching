using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameRule",menuName = "Scriptable Objects/GameRule")]
public class GameRule : ScriptableObject
{
    public int Rows;
    public int Columns;

    public int ColorCount;

   // public MinimumMatchCount MinimumMatchCounts;
    public TierData[] TierDatas;


   /* [Serializable]
    public struct MinimumMatchCount
    {
        public int tierA;
        public int tierB;
        public int tierC;
    }*/

    [Serializable]
    public struct TierData
    {
        public int lowerMatchLimit;
        public string tierName;
        public Sprite[] sprites;
    }

    


   


}
