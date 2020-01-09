using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public bool isRight;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    public GameObject ball;
    public float speed;

    void Start()
    {
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (isRight)
        {
            transform.position = new Vector2(screenBounds.x - objectWidth * 2, 0);
        }
        else
        {
            transform.position = new Vector2(-screenBounds.x + objectWidth * 2, 0);
        }
    }

    private void OnEnable()
    {
        if (!isRight)
        {
            Joystick.OnMakeMove += MakeMove;
        }
    }

    private void OnDisable()
    {
        Joystick.OnMakeMove -= MakeMove;
    }

    void MakeMove(Vector2 dir)
    {
        Vector2 direction = new Vector2(dir.y, 0);
        Debug.Log(direction.ToString());
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void Update()
    {
        if (isRight)
        {
            AIForRight();
        }
    }

    void AIForRight()
    {
        Vector3 vel = ball.GetComponent<Rigidbody2D>().velocity;
        if (vel.x > 0)
        {
            if (ball.transform.position.y > transform.position.y)
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
        }
    }
}
