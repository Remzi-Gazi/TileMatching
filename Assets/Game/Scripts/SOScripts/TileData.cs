using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Scriptable Objects/TileData")]
public class TileData : ScriptableObject
{
    public Dictionary<string, List<Sprite>> TileSpriteMap;

    public List<TileSprite> TileSprites;

    private void OnEnable()
    {
        TileSpriteMap = new Dictionary<string, List<Sprite>>();

        for(int i = 0; i < TileSprites.Count; i++)
        {
            TileSpriteMap.Add(TileSprites[i].TierName, TileSprites[i].TierSprites);
        }
    }
}

[Serializable]
public class TileSprite
{
    public string TierName;
    public List<Sprite> TierSprites;
}
