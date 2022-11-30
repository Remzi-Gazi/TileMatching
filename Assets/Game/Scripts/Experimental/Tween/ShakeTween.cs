using UnityEngine;
using DG.Tweening;
 
[CreateAssetMenu(fileName = "ShakeTween", menuName = "Scriptable Objects/Tweens/ShakeTween")]
public class ShakeTween : TweenBase
{
    public TweenShakeMode TweenShakeMode;

    public float ShakeDuration;
    public float ShakePower;

    public void DoTween(Sequence sequence, Transform tileTransform)
    {
        if (TweenShakeMode == TweenShakeMode.ShakeScale)
        {
            sequence.Append(tileTransform.DOShakeScale(ShakeDuration, ShakePower));
        }
        else if (TweenShakeMode == TweenShakeMode.ShakeRotation)
        {
            sequence.Append(tileTransform.DOShakeRotation(ShakeDuration, ShakePower));
        }
        else if (TweenShakeMode == TweenShakeMode.ShakePosition)
        {
            sequence.Append(tileTransform.DOShakePosition(ShakeDuration, ShakePower));
        }
    }

    #if UNITY_EDITOR
    private float ShakeScaleDefaultPower = 1f;
    private float ShakeRotationDefaultPower = 90f;
    private float ShakePositionDefaultPower = 1f;

    public override void SetDefaults()
    {
        if (TweenShakeMode == TweenShakeMode.ShakeScale)
        {
            ShakePower = ShakeScaleDefaultPower;
        }
        else if (TweenShakeMode == TweenShakeMode.ShakeRotation)
        {
            ShakePower = ShakeRotationDefaultPower;
        }
        else if (TweenShakeMode == TweenShakeMode.ShakePosition)
        {
            ShakePower = ShakePositionDefaultPower;
        }
    }
    #endif
}

public enum TweenShakeMode
{
    ShakeScale,
    ShakeRotation,
    ShakePosition
}