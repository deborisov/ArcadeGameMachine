using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public bool isRight;
    private Vector2 screenBounds;
    private float objectWidth;
    public GameObject ball;
    public float ratedSpeed;
    private float currentSpeed;
    private float currentTime = 0;
    public float secondsToSlowPaddle = 0.3f, removedSpeed = 15f / 240;

    void Start()
    {
        currentSpeed = ratedSpeed;
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
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
        twoDPongGameManager.OnGameStarted += NormalizeTimeAndSpeed;
    }

    private void OnDisable()
    {
        Joystick.OnMakeMove -= MakeMove;
        twoDPongGameManager.OnGameStarted -= NormalizeTimeAndSpeed;
    }

    void MakeMove(Vector2 dir)
    {
        Vector2 direction = new Vector2(dir.y, 0);
        transform.Translate(direction * currentSpeed * Time.deltaTime);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (isRight)
        {
            AIForRight();
            SlowDownPaddle();
        }
    }

    void SlowDownPaddle()
    {
        if (currentTime > secondsToSlowPaddle)
        {
            currentSpeed -= removedSpeed;
            currentTime -= secondsToSlowPaddle;
            Debug.Log(currentSpeed);
            Debug.Log(Time.timeScale);
        }
    }

    void AIForRight()
    {
        Vector3 vel = ball.GetComponent<Rigidbody2D>().velocity;
        if (vel.x > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, ball.transform.position.y, transform.position.z), currentSpeed * Time.deltaTime);
        }
    }

    void NormalizeTimeAndSpeed()
    {
        currentSpeed = ratedSpeed;
        currentTime = 0;
    }
}
