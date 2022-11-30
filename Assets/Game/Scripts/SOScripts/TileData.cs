using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Scriptable Objects/TileData")]
public class TileData : ScriptableObject
{
    public Vector2 TileSize;
    public TileContainer[] Tiles;

    [Serializable]
    public struct TileContainer
    {
        public int colorId;
        public Sprite defaultTile;
        public Sprite tierA;
        public Sprite tierB;
        public Sprite tierC;
    }
}
