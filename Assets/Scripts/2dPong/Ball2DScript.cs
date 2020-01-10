using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2DScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    void Start()
    {
        transform.position = new Vector2(0, 0);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void ThrowBall()
    {
        Vector2 direction = new Vector2(0,0);
        float x = direction.x, y = direction.y;
        do
        {
            direction = Random.insideUnitCircle.normalized;
            x = direction.x;
            y = direction.y;
        } while (direction == new Vector2(0, 0) || x < y && x > -y || x > y && x < -y);
        rb.AddForce(direction * speed);
    }
    private void OnEnable()
    {
        twoDPongGameManager.OnGameStarted += ThrowBall;
    }

    private void OnDisable()
    {
        twoDPongGameManager.OnGameStarted -= ThrowBall;
    }
}
