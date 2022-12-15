using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine.UI;

public class CardController
{
    private CardView _view;

    public CardController(CardView view, CardParameters parameters)
    {
        _view = view;
        InitSetParameters(parameters);
    }

    public void InitSetParameters(CardParameters parameters)
    {
        SetArt(parameters.art);

        if (parameters.title != null)
        {
            if (GameManager.Instance.CardDefaultData.MaxTitleCharactersNum < parameters.title.Length)
                parameters.title = parameters.title.Substring(0, GameManager.Instance.CardDefaultData.MaxTitleCharactersNum);
            _view.Title.text = parameters.title;
        }
        if (parameters.description != null)
        {
            if (GameManager.Instance.CardDefaultData.MaxDescriptionCharactersNum < parameters.description.Length)
                parameters.description = parameters.description.Substring(0, GameManager.Instance.CardDefaultData.MaxDescriptionCharactersNum);
            _view.Description.text = parameters.description;
        }

        _view.Health.text = parameters.health.ToString();
        _view.Damage.text = parameters.damage.ToString();
        _view.Manacost.text = parameters.manacost.ToString();
    }

    public Tween SetParameter(TextMeshPro targetText, int startValue, int endValue)
    {
        if (startValue == endValue) return null;
        float duration = Mathf.Abs(endValue - startValue) * GameManager.Instance.CardDefaultData.ParameterCounterDuration;
        Tween tween = DOTween.To(() => startValue, x => startValue = x, endValue, duration).OnUpdate(() => targetText.text = startValue.ToString());
        return tween;
    }

    public void SetArt(Texture2D art)
    {
        if (art != null)
        {
            Material material = new(_view.Art.material);
            material.SetTexture("_MainTex", art);
            _view.Art.material = material;
        }
    }

    public void OnCardShow(BoxCollider collider, bool pointerEnter)
    {
        float change = GameManager.Instance.CardDefaultData.CardPointerMovement;
        Vector3 colliderSizeChange = Vector3.zero;
        colliderSizeChange.y = pointerEnter ? change : - change;
        collider.size += colliderSizeChange;

        Vector3 colliderCenterChange = Vector3.zero;
        colliderCenterChange.y = pointerEnter ? - change / 2 : change / 2;
        collider.center += colliderCenterChange;

        float scale = GameManager.Instance.CardDefaultData.PointerEnterScale;
        collider.gameObject.transform.localScale = pointerEnter ? collider.gameObject.transform.localScale * scale : new Vector3(1, 1, 1);
    }
}
