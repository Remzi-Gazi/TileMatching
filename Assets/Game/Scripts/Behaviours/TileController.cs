using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private Tweener _tweener;

    //Related tile
    private Tile _tile;
    //Related Game Object
    private GameObject _tileGameObject;

    //components
    private SpriteRenderer _tileSpriteRenderer;
    private Transform _transform;

    private void Awake()
    {
        _tileGameObject = gameObject;
        _transform = GetComponent<Transform>();
        _tileSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateSprite(ITileMatching tileMatching, GameRule gameRule, TileData tileData)
    {
        int tileTierIndex = tileMatching.GetMatchTierIndex(_tile, gameRule);

        string tierName = gameRule.TierData[tileTierIndex].TierName;
        _tileSpriteRenderer.sprite = tileData.TileSpriteMap[tierName][_tile.ColorId];
        _tileSpriteRenderer.sortingOrder = -_tile.Row + 20;
    }


    public void UpdateTile()
    {
        if(_tile.TileState == TileState.Stable)
        {
            return;
        }
        else if(_tile.TileState == TileState.Instantiated)
        {
            //animate to final position
            _tweener.PlaceTileInitially(_transform, _tile.Row,_tile.Column);
            //make tile stable
            _tile.TileState = TileState.Stable;
        }
        else if (_tile.TileState == TileState.Blasted)
        {
            //set gameobjects inactive
            _tileGameObject.SetActive(false);
            //make it stable
            _tile.TileState = TileState.Stable;
        }
        else if (_tile.TileState == TileState.Shifted)
        {
            _tweener.ShiftRemainingTile(_transform, _tile.Row, _tile.Column);
            //make tile stable
            _tile.TileState = TileState.Stable;
        }
        else if (_tile.TileState == TileState.Respawned)
        {
            _tileGameObject.SetActive(true);
            _tweener.ShiftRespawnedTile(_transform, _tile.Row, _tile.Column);
            _tile.TileState = TileState.Stable;
        }
    }

    public Tile Tile { get => _tile; set => _tile = value; }
    public Tweener Tweener { get => _tweener; set => _tweener = value; }


}


