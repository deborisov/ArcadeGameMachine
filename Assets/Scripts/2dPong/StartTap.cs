using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StartTap : MonoBehaviour
{
    public TextMeshProUGUI startText;
    public delegate void TapHappened();
    public static event TapHappened OnTapHappened;
    private void OnEnable()
    {
        startText = GetComponent<TextMeshProUGUI>();
        startText.text = "Tap to start";
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            OnTapHappened();
        }
    }

}
