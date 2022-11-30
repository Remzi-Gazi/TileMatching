using UnityEngine;
using DG.Tweening;

public class TweenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //DOTween.PauseAll();
        Sequence sequence = DOTween.Sequence();
        Tween tween = transform.DOMove(new Vector3(10, 9, 10), 3);
        tween.Pause();


    }

    
}
