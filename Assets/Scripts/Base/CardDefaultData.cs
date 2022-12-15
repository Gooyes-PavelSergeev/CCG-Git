using UnityEngine;

[CreateAssetMenu]
public class CardDefaultData : ScriptableObject
{
    [SerializeField] private Vector2Int _manacostLimit;
    [SerializeField] private Vector2Int _healthLimit;
    [SerializeField] private Vector2Int _damageLimit;

    [SerializeField] private string _title = "Default name";
    [SerializeField] private string _description = "Default description";

    [SerializeField] private float _pointerEnterScale = 1.4f;
    [SerializeField] private float _cardPointerMovement = 1f;
    [SerializeField] private float _parameterCounterDuration = 0.1f;

    [SerializeField] private int maxTitleCharactersNum = 16;
    [SerializeField] private int maxDescriptionCharactersNum = 30;

    public string Title { get => _title; }
    public string Description { get => _description; }

    public float PointerEnterScale { get => _pointerEnterScale; }
    public float CardPointerMovement { get => _cardPointerMovement; }
    public float ParameterCounterDuration { get => _parameterCounterDuration; }

    public int MaxTitleCharactersNum { get => maxTitleCharactersNum; }
    public int MaxDescriptionCharactersNum { get => maxDescriptionCharactersNum; }

    public Vector2Int GetLimit(string parameterName)
    {
        if (string.IsNullOrEmpty(parameterName)) return Vector2Int.zero;

        switch (parameterName)
        {
            case "Manacost":
                return _manacostLimit;
            case "Health":
                return _healthLimit;
            case "Damage":
                return _damageLimit;
        }

        return Vector2Int.zero;
    }
}
