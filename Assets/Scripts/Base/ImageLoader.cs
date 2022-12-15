using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class ImageLoader
{
    public static string url = "https://picsum.photos/200/300";

    [System.Obsolete]
    public static IEnumerator DownloadImage(CardView cardView)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            cardView.SetArt(texture);
        }
        //yield return null;
    }
}
