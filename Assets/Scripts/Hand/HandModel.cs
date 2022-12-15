using System.Collections.Generic;
using UnityEngine;

public class HandModel
{
    public List<Card> Cards { get; private set; }

    public HandView View { get; private set; }

    public HandModel(int cardAmout)
    {
        Cards = new List<Card>();

        for (int i = 0; i < cardAmout; i++)
        {
            if (Cards.Count < GameManager.Instance.HandCapacity)
            {
                Card card = new Card();
                Cards.Add(card);
            }
        }

        View = new HandView(this);

        RefreshHand();
        View.RefreshHandTransform(Cards);
    }

    public void SetRandomValues()
    {
        List<Card> cards = new List<Card>();

        for (int i = 0; i < Cards.Count; i++)
        {
            cards.Add(Cards[i]);
        }

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetRandom();
        }

        View.RefreshCardsView(cards);
    }

    public void AddCard()
    {
        if (Cards.Count >= GameManager.Instance.HandCapacity) return;

        Card card = new Card();
        Cards.Add(card);

        RefreshHand();
        View.RefreshHandTransform(Cards);
    }

    public void RemoveCard(int index)
    {
        Card card = Cards[index];
        if (card == null) return;

        View.RefreshCardsView(Cards);
        Cards.RemoveAt(index);
        //card.Remove();

        RefreshHand();
    }

    private void RefreshHand()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].Index = i;
        }
    }

    public void RefreshHandView()
    {
        View.RefreshHandTransform(Cards);
    }

    public void RemoveCard()
    {
        if (Cards.Count == 0) return;
        int index = Cards.Count - 1;
        RemoveCard(index);
    }
}
