using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandController
{
    private HandView _view;

    private List<Transform> _cards;

    public HandController(HandView view)
    {
        _view = view;
    }

    public Vector3 SetPosition(Transform card, int index)
    {
        float angleFrom = 90 + _view.AngleBetweenCards * (_cards.Count - 1) / 2 - _view.AngleBetweenCards * index;

        float x = Mathf.Cos(angleFrom * Mathf.Deg2Rad) * _view.HandRadius;
        float y = Mathf.Sin(angleFrom * Mathf.Deg2Rad) * _view.HandRadius;

        Vector3 pos = new Vector3(x, y, 0.016f * index);

        card.localPosition = pos;
        return pos;
    }

    private void SetAngle(Transform card)
    {
        Vector3 vector = card.position - GameManager.Instance.HandTransform.position;
        Vector3 lookRotation = Quaternion.LookRotation(vector).eulerAngles;
        float angle = card.localPosition.x < GameManager.Instance.HandTransform.position.x ? lookRotation.x - 270f : 270f - lookRotation.x;
        Vector3 eulerAngles = new Vector3(0, 0, angle);
        card.DOLocalRotate(eulerAngles, 0.1f);
    }

    public void ShowCard(Transform card, int index, bool pointerEnter)
    {
        Vector3 targetPosition;
        Transform childOverlay = card.GetChild(0);
        Vector3 childPos = childOverlay.position;

        if (pointerEnter)
        {
            targetPosition = card.localPosition + new Vector3(0f, GameManager.Instance.CardDefaultData.CardPointerMovement, -0.2f);
            card.localPosition = targetPosition;
            card.DORotate(Vector3.zero, 0.1f);
        }
        else
        {
            targetPosition = SetPosition(card, index);
            SetAngle(card);
        }

        childOverlay.position = childPos;
        DOTween.To(() => childOverlay.localPosition, x => childOverlay.localPosition = x, Vector3.zero, 0.25f);
    }

    public void RefreshHandTransform(List<Card> cards)
    {
        _cards = GetTransformList(_view.Cards);

        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].parent = GameManager.Instance.HandTransform;

            SetPosition(_cards[i], i);

            SetAngle(_cards[i]);
        }
    }

    CardParameterTweener _tweener;
    public void RefreshCardsView(List<Card> cards)
    {
        if (_tweener != null) _tweener.Finish();
        _tweener = new CardParameterTweener();
        foreach (Card card in cards)
        {
            List<Tween> tweens = card.RefreshView();
            if (card.Active) _tweener.TweenParamaters(tweens);
            else _tweener.TweenParamaters(tweens, () => RefreshHandTransform(cards));
        }
    }

    private List<Transform> GetTransformList(List<Card> cards)
    {
        List<Transform> cardTransforms = new();

        foreach (Card card in cards)
        {
            if (card.Transform != null) cardTransforms.Add(card.Transform);
        }

        return cardTransforms;
    }
}
