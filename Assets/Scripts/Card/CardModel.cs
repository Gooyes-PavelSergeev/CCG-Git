using System.Collections.Generic;
using UnityEngine;

public class CardModel
{
    private Parameter _manacost;
    private Parameter _health;
    private Parameter _damage;

    public int Manacost { get => _manacost.value; }
    public int Health { get => _health.value; }
    public int Damage { get => _damage.value; }

    private Parameter[] _parameters;

    public bool Active { get; private set; }

    public CardView View { get; private set; }

    public CardModel (CardParameters parameters)
    {
        _manacost = new Parameter(parameters.manacost, "Manacost");
        _health = new Parameter(parameters.health, "Health");
        _damage = new Parameter(parameters.damage, "Damage");

        _parameters = new Parameter[] {_manacost, _health, _damage};

        Active = true;

        View = new(this, parameters);
    }

    private void SetParameter(Parameter parameter, int targetValue)
    {
        if (CheckRange(parameter))
        {
            parameter.value = targetValue;
        }
        else Debug.Log("Value of " + parameter.name + " is out of range");

        CheckHealth();
    }

    public void SetParameter(string parameterName, int targetValue)
    {
        Parameter parameter = GetParameter(parameterName);
        if (parameter == null) return;

        SetParameter(parameter, targetValue);
    }

    private bool CheckRange(Parameter parameter)
    {
        Vector2Int limit = GetLimit(parameter);
        bool isInRange = parameter.value >= limit.x && parameter.value <= limit.y;
        return isInRange;
    }

    public int GetParameterValue(string name)
    {
        return GetParameter(name).value;
    }

    private Parameter GetParameter(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;

        foreach (var parameter in _parameters)
            if (parameter.name == name)
                return parameter;

        Debug.Log("Wrong param name");
        return null;
    }

    private void Destroy()
    {
        Active = false;
        View.Active = false;
        if (View.InHand) GameManager.Instance.RemoveCardFromHand(View.Index.Value);
        else View.DestroyCard();
    }

    private void CheckHealth()
    {
        if (_health.value <= 0)
        {
            Destroy();
        }
    }

    private Vector2Int GetLimit(Parameter parameter)
    {
        Vector2Int limit = GameManager.Instance.CardDefaultData.GetLimit(parameter.name);
        return limit;
    }

    private class Parameter
    {
        public int value;
        public string name;

        public Parameter(int value, string name)
        {
            this.value = value;
            this.name = name;
        }
    }
}
