using DG.Tweening;
using UnityEngine;

public class TileView : MonoBehaviour
{
    public delegate void ClickAction(int connectionId);
    public static event ClickAction OnClick;

    [SerializeField] private GameRule _gameRule;
    [SerializeField] private TweenerData _tweenerData;
    
    private TileController _tileController;

    //components
    private SpriteRenderer _tileSpriteRenderer;
    private Transform _transform;

    private bool _firstPlacement;

    private void Awake()
    {
        _tileSpriteRenderer = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        _firstPlacement = true;
    }

    public void UpdateView(TileData tileData, ITileMatching tileMatching)
    {
        UpdateSpriteRenderer(tileData, tileMatching);
        if (_tileController.Tile.Blasted)
        {
            SetTilePosition();
        }
        if (_tileController.Tile.Shifted)
        {
            ShiftTile();
        }
        ResetTileProperties();
    }

    private void UpdateSpriteRenderer(TileData tileData, ITileMatching tileMatching)
    {
        string tierName = _tileController.GetTierName(tileMatching, _gameRule.TierData);
        int colorId = _tileController.GetColorId();
        _tileSpriteRenderer.sprite = tileData.TileSpriteMap[tierName][colorId];
        _tileSpriteRenderer.sortingOrder = _tileController.GetSortingOrder();
    }
    
    private void SetTilePosition()
    {
        if (_firstPlacement)
        {
            _transform.position = _tweenerData.TilePlacementPositions[_tileController.Tile.Row, _tileController.Tile.Column];
            _firstPlacement = false;
        }
        else
        {
            _transform.position = _tweenerData.TilePlacementPositions[_tileController.Tile.Row, _tileController.Tile.Column] + _tweenerData.TileReplacementHeight;
        }
        
    }
    
    private void ShiftTile()
    {
        Sequence sequence = DOTween.Sequence();
        Vector3 placementPosition  = _tweenerData.TilePlacementPositions[_tileController.Tile.Row, _tileController.Tile.Column];
        sequence.Append(_transform.DOMove(placementPosition, _tweenerData.TilePlacementDuration));
        sequence.Append(_transform.DOShakeScale(0.3f, 0.2f));
    }

    public void SetTile(Tile tile)
    {
        _tileController = new TileController(tile);
    }

    private void ResetTileProperties()
    {
        _tileController.ResetTileProperties();
    }
    
    public void OnMouseDown()
    {
        OnClick(_tileController.Tile.ConnectionId);
    }
}
