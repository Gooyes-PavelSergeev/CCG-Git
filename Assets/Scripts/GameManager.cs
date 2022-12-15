using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private CardDefaultData _cardDefaultData;

    [SerializeField] private CardAppearance _cardAppearance;

    [SerializeField] private int _handCapacity;

    [SerializeField] private Transform _handTransform;

    public CardDefaultData CardDefaultData { get => _cardDefaultData; }
    public CardAppearance CardAppearance { get => _cardAppearance; }
    public int HandCapacity { get => _handCapacity; }
    public Transform HandTransform { get => _handTransform; }

    public HandModel Hand { get; private set; }

    private void Start()
    {
        Scaler.Init();

        Scaler.SetScaleForHand(_handTransform);

        Playfield.Instance.Init();

        //CardHandModel cardHand = new CardHandModel(Random.Range(1, _handCapacity + 1));
        HandModel hand = new HandModel(10);

        Hand = hand;
    }

    public void ShowCardFromHand(Transform card, int? index, bool pointerEnter)
    {
        if (index != null) Hand.View.ShowCard(card, index.Value, pointerEnter);
    }

    public void RandomizeHand()
    {
        Hand.SetRandomValues();
    }

    public void AddCardToHand()
    {
        Hand.AddCard();
    }

    public void RemoveCardFromHand(int index)
    {
        Hand.RemoveCard(index);
    }
}
