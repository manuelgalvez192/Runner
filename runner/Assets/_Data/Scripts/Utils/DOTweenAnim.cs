using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTweenAnim : MonoBehaviour
{
    public Vector3 punch;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        CreateSequence();
        //transform.DOPunchRotation(punch, duration, 10, 0.8f).SetDelay(1f).SetRelative(true).SetEase(Ease.OutQuad).OnComplete(MoveUp);
    }

    private void MoveUp()
    {
        transform.DOMove(new Vector3(0f, 10f, 0f), 1f).SetRelative(true);
    }

    private void CreateSequence()
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(transform.DOMove(new Vector3(0f, 10f, 0f), 2f).SetRelative(true).SetDelay(1f));
        sq.Insert(0, transform.DOScale(new Vector3(2f, 2f, 2f), 0.3f).SetRelative(true).SetDelay(1f).SetLoops(-1, LoopType.Yoyo));

        sq.Play();
    }

}
