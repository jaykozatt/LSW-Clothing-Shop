using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HintAnimator : MonoBehaviour
{
    
    Sequence sequence;

    // Start is called before the first frame update
    void Start()
    {
        sequence = DOTween.Sequence();

        sequence.Append(
            transform.DOLocalMoveY(transform.localPosition.y+10f, .125f)
            .SetEase(Ease.Linear)
            .SetDelay(Random.value/10)
        );
        sequence.Append(
            transform.DOLocalMoveY(transform.localPosition.y, .125f)
        );
        sequence.Append(
            transform.DOLocalMoveY(transform.localPosition.y-10f, .125f)
            .SetEase(Ease.Linear)
        );
        sequence.Append(
            transform.DOLocalMoveY(transform.localPosition.y, .125f)
        );        
        sequence.SetLoops(-1);

        sequence.Play();    
    }

}
