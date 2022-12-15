using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private readonly CardModel _model;

    public int Manacost { get => _model.Manacost; }
    public int Health { get => _model.Health; }
    public int Damage { get => _model.Damage; }

    public int Index { set { _model.View.Index = value; } }

    public bool Active { get => _model.Active; }
    public Transform Transform { get => _model.View.Transform; }

    public Card(CardParameters parameters)
    {
        _model = new CardModel(parameters);
    }

    public Card()
    {
        CardParameters parameters = new CardParameters();
        parameters.manacost = UnityEngine.Random.Range(0, GameManager.Instance.CardDefaultData.GetLimit("Manacost").y);
        parameters.health = UnityEngine.Random.Range(1, GameManager.Instance.CardDefaultData.GetLimit("Health").y);
        parameters.damage = UnityEngine.Random.Range(0, GameManager.Instance.CardDefaultData.GetLimit("Damage").y);
        parameters.title = GameManager.Instance.CardDefaultData.Title;
        parameters.description = GameManager.Instance.CardDefaultData.Description;

        _model = new CardModel(parameters);

        _model.View.SetArt();
    }

    public void SetParameterValue(string name, int value)
    {
        _model.SetParameter(name, value);
    }

    public List<Tween> RefreshView()
    {
        return _model.View.RefreshParameters();
    }

    public void SetRandom()
    {
        int index = UnityEngine.Random.Range(0, 3);
        Parameter parameter = GetParameter(index);

        Vector2Int limit = GameManager.Instance.CardDefaultData.GetLimit(parameter.ToString());
        int value = UnityEngine.Random.Range(limit.x, limit.y + 1);

        SetParameterValue(parameter.ToString(), value);
    }

    private Parameter GetParameter(int index)
    {
        return (Parameter)index;
    }

    private enum Parameter
    {
        Manacost,
        Health,
        Damage
    }
}

public struct CardParameters
{
    public int manacost;
    public int health;
    public int damage;
    public Texture2D art;
    public string title;
    public string description;
}
