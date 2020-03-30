using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countdown;
    public delegate void TapHappened();
    public static event TapHappened OnTapHappened;
    private void OnEnable()
    {
        countdown = GetComponent<TextMeshProUGUI>();
        countdown.text = "Tap to start";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnTapHappened();
        }
    }
}
