using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;
using System;

public class CardView
{
    private MeshRenderer _art;
    private TextMeshPro _title;
    private TextMeshPro _description;
    private Parameter _damage;
    private Parameter _health;
    private Parameter _manacost;
    private Parameter[] _parameters;

    private int? _index;

    public MeshRenderer Art { get => _art; }
    public TextMeshPro Title { get => _title; }
    public TextMeshPro Description { get => _description; }
    public TextMeshPro Damage { get => _damage.text; }
    public TextMeshPro Health { get => _health.text; }
    public TextMeshPro Manacost { get => _manacost.text; }

    public bool InHand { get => _index != null; }

    public int? Index { get => _index; set { _pointerHandler.index = value; _index = value; } }

    private readonly CardController _controller;
    private CardAppearance _card;
    private CardPointerHandler _pointerHandler;
    private BoxCollider _cardCollider;

    private readonly CardModel _model;

    public bool Active { set => _pointerHandler.active = value; }
    public Transform Transform { get; private set; }

    public CardView(CardModel model, CardParameters parameters)
    {
        _model = model;
        _card = GameManager.Instance.CardAppearance;

        var card = GameObject.Instantiate(_card);
        _art = card.art;
        _title = card.title;
        _description = card.description;
        _damage = new Parameter(parameters.damage, "Damage", card.damage);
        _health = new Parameter(parameters.health, "Health", card.health);
        _manacost = new Parameter(parameters.manacost, "Manacost", card.manacost);
        _parameters = new Parameter[] { _manacost, _health, _damage };

        _card = card;
        Transform = card.transform;
        _cardCollider = card.gameObject.GetComponent<BoxCollider>();
        _pointerHandler = _card.gameObject.AddComponent<CardPointerHandler>();
        _pointerHandler.cardView = this;
        _pointerHandler.active = true;

        _controller = new CardController(this, parameters);
    }

    public List<Tween> RefreshParameters()
    {
        List<Tween> tweens = new List<Tween>();
        foreach (var parameter in _parameters)
        {
            Tween tween = _controller.SetParameter(parameter.text, Int32.Parse(parameter.text.text), _model.GetParameterValue(parameter.name));
            if (tween != null) tweens.Add(tween);
            parameter.value = _model.GetParameterValue(parameter.name);
        }

        if (tweens.Count <= 0) return null;

        Tween longestTween = CardParameterTweener.GetLongestTween(tweens);
        longestTween.onComplete += CheckActive;

        return tweens;
    }

    private void CheckActive()
    {
        if (!_model.Active)
        {
            DestroyCard();
        }
    }

    public void SetArt()
    {
        _card.StartCoroutine(ImageLoader.DownloadImage(this));
    }

    public void SetArt(Texture2D art)
    {
        _controller.SetArt(art);
    }

    public void DestroyCard()
    {
        if (_card != null) GameObject.Destroy(_card.gameObject);
    }

    public void OnCardShow(bool pointerEnter)
    {
        _controller.OnCardShow(_cardCollider, pointerEnter);
    }

    private class Parameter
    {
        public int value;
        public string name;
        public TextMeshPro text;

        public Parameter(int value, string name, TextMeshPro text)
        {
            this.value = value;
            this.name = name;
            this.text = text;
        }
    }
}

public class CardPointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    public int? index;
    public CardView cardView;

    public bool active;
    private bool _isDragging;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!active) return;
        Debug.Log("Dropped");
        _isDragging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!active) return;
        if (_isDragging)
        {

        }
        Debug.Log("Picked");
        _isDragging = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!active) return;
        GameManager.Instance.ShowCardFromHand(this.gameObject.transform, index, true);
        cardView.OnCardShow(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!active) return;
        GameManager.Instance.ShowCardFromHand(this.gameObject.transform, index, false);
        cardView.OnCardShow(false);
    }
}
