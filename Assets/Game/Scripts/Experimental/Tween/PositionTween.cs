using UnityEngine;
using DG.Tweening;
 
[CreateAssetMenu(fileName = "PositionTween", menuName = "Scriptable Objects/Tweens/PositionTween")]
public class PositionTween: TweenBase
{
    public float PlacementDuration;

    public void DoTween(Sequence sequence, Transform tileTransform, Vector3[,] tilePlacementPosition, int tileRow, int tileColumn)
    {
        sequence.Append(tileTransform.DOMove(tilePlacementPosition[tileRow, tileColumn], PlacementDuration));
    }

    #if UNITY_EDITOR

    public override void SetDefaults()
    {
        
    }
    #endif
}
