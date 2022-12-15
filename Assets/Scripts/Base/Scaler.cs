using UnityEngine;

public static class Scaler
{
    private static float _fieldVertExtent;
    private static float _fieldHorExtent;

    public static void Init()
    {
        _fieldVertExtent = Camera.main.orthographicSize;
        _fieldHorExtent = _fieldVertExtent * Screen.width / Screen.height;
    }

    public static Transform SetScaleForPlane(Transform targetTransform)
    {
        Vector3 scale = new Vector3(_fieldHorExtent * 2 / 10 + 0.05f, 1, _fieldVertExtent * 2 / 10 + 0.05f);
        targetTransform.localScale = scale;
        return targetTransform;
    }

    public static Transform SetScaleForHand(Transform targetTransform)
    {
        float targetRelation = 16f / 9f;
        float relation = _fieldHorExtent / _fieldVertExtent;
        float horScale = relation / targetRelation;
        Vector3 scale = targetTransform.localScale;
        scale.x *= horScale;
        targetTransform.localScale = scale;
        return targetTransform;
    }
}
