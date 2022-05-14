using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReelsRoll : MonoBehaviour
{
    [SerializeField] private RectTransform[] reels;
    [SerializeField] private float delayStep;
    [SerializeField] private Ease startEase;
    [SerializeField] private Ease stopEase;
    public void ScrollStart()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            var reel = reels[i];

            reel.DOAnchorPosY(-860, 1)
                .SetDelay(i * delayStep)
                .SetEase(startEase)
                .OnComplete(() => ScrollLinear(reel));
        }
    } 

    private void ScrollLinear(RectTransform reel)
    {
        reel.DOAnchorPosY(-3520, 1).SetEase(Ease.Linear).OnComplete(() => ScrollStop(reel));
    }
    private void ScrollStop(RectTransform reel)
    {
        reel.DOAnchorPosY(-4400, 1).SetEase(stopEase);
    }
}
