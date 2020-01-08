using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2DScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    void Start()
    {
        transform.position = new Vector2(0, 0);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ThrowBall()
    {
        rb.AddForce(Random.insideUnitCircle.normalized * speed);
        Debug.Log("Lol");
    }
    private void OnEnable()
    {
        twoDPongGameManager.OnGameStarted += ThrowBall;
    }

    private void OnDisable()
    {
        StartTap.OnTapHappened -= ThrowBall;
    }
}
