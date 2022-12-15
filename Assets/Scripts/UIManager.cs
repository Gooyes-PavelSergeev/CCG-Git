using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public void RefreshHand(Button button)
    {
        BlockButton(button);
        GameManager.Instance.RandomizeHand();
    }

    public void AddCard()
    {
        GameManager.Instance.AddCardToHand();
    }

    private void BlockButton(Button button)
    {
        StartCoroutine(BlockButtonForSeconds(button));
    }

    private IEnumerator BlockButtonForSeconds(Button button)
    {
        button.enabled = false;
        yield return new WaitForSeconds(0.5f);
        button.enabled = true;
    }
}
