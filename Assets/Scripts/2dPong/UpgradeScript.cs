using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    private float liveTime = 20f;

    void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime < 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
