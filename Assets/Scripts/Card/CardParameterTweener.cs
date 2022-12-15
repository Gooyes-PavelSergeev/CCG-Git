using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CardParameterTweener
{
    private Sequence _sequence;

    public CardParameterTweener()
    {
        _sequence = DOTween.Sequence();
    }

    public void Finish()
    {
        _sequence.Rewind();
    }

    public void TweenParamaters(List<Tween> tweens, TweenCallback onCompleteCallback)
    {
        if (onCompleteCallback != null) GetLongestTween(tweens).onComplete += onCompleteCallback;
        TweenParamaters(tweens);
    }

    public void TweenParamaters(List<Tween> tweens)
    {
        Sequence sequence = DOTween.Sequence();
        if (tweens == null) return;
        foreach (Tween tween in tweens)
        {
            sequence.Join(tween);
        }

        _sequence.Append(sequence);
    }

    public static Tween GetLongestTween(List<Tween> tweens)
    {
        float maxDuration = 0f;
        Tween maxDurationTween = null;
        foreach (Tween tween in tweens)
        {
            if (tween.Duration() > maxDuration)
            {
                maxDurationTween = tween;
            }
        }

        return maxDurationTween;
    }
}
