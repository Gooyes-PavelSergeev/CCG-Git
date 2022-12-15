using System.Collections.Generic;
using UnityEngine;

public class HandView
{
    private readonly HandController _controller;
    private readonly HandModel _model;

    private float _angleBetweenCards = 8f;
    private float _handRadius = 10f;

    public float AngleBetweenCards { get => _angleBetweenCards; }
    public float HandRadius { get => _handRadius; }
    public List<Card> Cards { get => _model.Cards; }

    public HandView (HandModel model)
    {
        _model = model;
        _controller = new HandController(this);
    }

    public void ShowCard(Transform card, int index, bool pointerEnter)
    {
        _controller.ShowCard(card, index, pointerEnter);
    }

    public void RefreshHandTransform(List<Card> cards)
    {
        _controller.RefreshHandTransform(cards);
        for (int i = 0; i < GameManager.Instance.HandTransform.childCount; i++)
        {
            Transform card = GameManager.Instance.HandTransform.GetChild(i);
            if (card != null && !card.gameObject.GetComponent<CardPointerHandler>().active)
            {
                GameObject.Destroy(card.gameObject);
                i--;
            }
        }
    }

    public void RefreshCardsView(List<Card> cards)
    {
        _controller.RefreshCardsView(cards);
    }
}
