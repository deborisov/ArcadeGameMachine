using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PannelScript : MonoBehaviour
{
    public float speed;
    public GameManager gm;
    public float timeToScale = 0.5f;
    (bool, bool) scalingSide = (false, false);
    float targetXScale = 0;
    public float delta = 0.2f;
    public float timeForUpgrade = 15f;
    bool stopCycle = false;

    public void Awake()
    {
        switch (PlayerPrefs.GetInt("Difficulty", 1))
        {
            case 0:
                transform.localScale = new Vector3(0.78f, 0.15f, 1f);
                break;
            case 1:
                transform.localScale = new Vector3(0.7f, 0.15f, 1f);
                break;
            case 2:
                transform.localScale = new Vector3(0.5f, 0.15f, 1f);
                break;
        }
    }

    public void Update()
    {
        if (scalingSide.Item1 || scalingSide.Item2)
        {
            timeForUpgrade -= Time.deltaTime;
            if (timeForUpgrade <= 0 && !stopCycle)
            {
                scalingSide = (scalingSide.Item2, scalingSide.Item1);
                delta = -delta;
                targetXScale += delta;
                stopCycle = true;
            }
            if (scalingSide.Item1 && targetXScale <= transform.localScale.x ||
                scalingSide.Item2 && targetXScale >= transform.localScale.x)
            {
                if (stopCycle)
                {
                    scalingSide = (false, false);
                    stopCycle = false;
                }
                return;
            }
            var temp = transform.localScale;
            temp.x = transform.localScale.x + delta / timeToScale * Time.deltaTime;
            transform.localScale = temp;
        }
    }
    private void OnEnable()
    {
        Joystick.OnMakeMove += MakeMove;
    }

    private void OnDisable()
    {
        Joystick.OnMakeMove -= MakeMove;
    }

    void MakeMove(Vector2 dir)
    {
        Vector2 direction = new Vector2(dir.x, 0);
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ExtraLife")){
            gm.ChangeLives(1);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Decrease"))
        {
            if (!scalingSide.Item1 && !scalingSide.Item2)
            {
                timeForUpgrade = 15f;
                scalingSide = (false, true);
                delta = -Mathf.Abs(delta);
                targetXScale = transform.localScale.x + delta;
            }
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Increase"))
        {
            if (!scalingSide.Item1 && !scalingSide.Item2)
            {
                timeForUpgrade = 15f;
                scalingSide = (true, false);
                delta = Mathf.Abs(delta);
                targetXScale = transform.localScale.x + delta;
            }
            Destroy(other.gameObject);
        }
    }
}
