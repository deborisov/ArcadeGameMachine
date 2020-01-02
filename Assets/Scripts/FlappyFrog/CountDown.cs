using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countdown;

    private void OnEnable()
    {
        countdown = GetComponent<TextMeshProUGUI>();
        countdown.text = "3";
    }

    IEnumerator Countdown()
    {
        int count = 3;
        for (int i = count; i > 0; --i)
        {
            countdown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
    }
}
