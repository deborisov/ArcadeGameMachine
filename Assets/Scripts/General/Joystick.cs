using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    private bool touchStart = false;
    private Vector2 startPoint, endPoint;
    public Transform innerCircle;
    public Transform outerCircle;
    public delegate void MakeMove(Vector2 d);
    public static event MakeMove OnMakeMove;
    public static bool IsAwake = true;

    public void Awake()
    {
        IsAwake = true;
    }

    private void OnEnable()
    {
        PauseMenu.OnPause += Hide;
        Ball2DScript.OnPlayerDied += Hide;
    }

    private void OnDisable()
    {
        PauseMenu.OnPause -= Hide;
        Ball2DScript.OnPlayerDied -= Hide;
    }
    void Hide()
    {
        innerCircle.GetComponent<SpriteRenderer>().enabled = false;
        outerCircle.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if (!IsAwake) return;
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            innerCircle.position = startPoint;
            outerCircle.position = startPoint;
            if (PlayerPrefs.GetInt("Joystick") == 1)
            {
                innerCircle.GetComponent<SpriteRenderer>().enabled = true;
                outerCircle.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            endPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            touchStart = false;
        }
    }

    private void FixedUpdate()
    {
        if (!IsAwake) return;
        if (touchStart)
        {
            Vector2 offset = endPoint - startPoint;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            if (PlayerPrefs.GetInt("Joystick") == 1)
            {
                innerCircle.position = new Vector2(startPoint.x + direction.x, startPoint.y + direction.y);
            }
            OnMakeMove(direction);
        }
        else
        {
            Hide();
        }
    }
}
