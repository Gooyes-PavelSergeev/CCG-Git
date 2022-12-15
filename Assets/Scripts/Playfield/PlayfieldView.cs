using UnityEngine;

public class PlayfieldView
{
    public PlayfieldView(Transform playfieldTransform)
    {
        Scaler.SetScaleForPlane(playfieldTransform);
    }
}
