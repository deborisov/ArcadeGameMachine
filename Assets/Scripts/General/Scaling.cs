using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    public GameObject scaler;
    public float timeToScale = 0.5f;
    public (bool, bool) scalingSide = (false, false);
    float targetScale = 0.2f;
    public float delta = 0.1f;
    public float timeForUpgrade = 5f;
    bool stopCycle = false;

    void Update()
    {
        if (scalingSide.Item1 || scalingSide.Item2)
        {
            Debug.Log(timeForUpgrade);
            timeForUpgrade -= Time.deltaTime;
            if (timeForUpgrade <= 0 && !stopCycle)
            {
                scalingSide = (scalingSide.Item2, scalingSide.Item1);
                delta = -delta;
                targetScale += delta;
                stopCycle = true;
            }
            if (scalingSide.Item1 && targetScale <= scaler.transform.localScale.x ||
                scalingSide.Item2 && targetScale >= scaler.transform.localScale.x)
            {
                if (stopCycle)
                {
                    scalingSide = (false, false);
                    stopCycle = false;
                }
                return;
            }
            var temp = scaler.transform.localScale;
            temp.x = scaler.transform.localScale.x + delta / timeToScale * Time.deltaTime;
            temp.y = scaler.transform.localScale.y + delta / timeToScale * Time.deltaTime;
            temp.z = scaler.transform.localScale.z + delta / timeToScale * Time.deltaTime;
            scaler.transform.localScale = temp;
        }
    }
}
