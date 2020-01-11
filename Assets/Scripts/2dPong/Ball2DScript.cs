using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2DScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public delegate void GameDelegate();
    public static event GameDelegate OnPlayerScored;
    public static event GameDelegate OnPlayerDied;

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

    void PlaceOnStart()
    {
        rb.velocity = new Vector3(0, 0, 0);
        transform.position = new Vector2(0, 0);
    }

    private void OnEnable()
    {
        twoDPongGameManager.OnGameStarted += ThrowBall;
        OnPlayerScored += PlaceOnStart;
        OnPlayerDied += () => rb.simulated = false;
    }

    private void OnDisable()
    {
        twoDPongGameManager.OnGameStarted -= ThrowBall;
        OnPlayerScored -= PlaceOnStart;
        OnPlayerDied -= () => rb.simulated = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LeftEdge"))
        {
            OnPlayerDied();
        }
        if (other.CompareTag("RightEdge"))
        {
            OnPlayerScored();
        }
    }
}
