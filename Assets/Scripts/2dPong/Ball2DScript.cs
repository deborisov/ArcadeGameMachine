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
        rb.AddForce(Random.insideUnitCircle.normalized * speed);
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
