using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform pannel;
    public float speed;
    public Transform explosion;
    public GameManager gm;
    public Transform powerUp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gm.gameOver)
        {
            return;
        }
        if (!inPlay)
        {
            transform.position = pannel.position;
        }
        if (Input.touchCount > 0 && !inPlay)
        {
            Touch pos = Input.GetTouch(0);
            if (pos.tapCount > 1)
            {
                rb.AddForce(Vector2.up * speed);
                inPlay = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bottom"))
        {
            inPlay = false;
            rb.velocity = Vector2.zero;
            gm.ChangeLives(-1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Brick"))
        {
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();
            if (brickScript.hitsToBreak > 1)
            {
                brickScript.BreakBrick();
            }
            else
            {
                int rnd = Random.Range(1, 11);
                if (rnd < 3)
                {
                    Instantiate(powerUp, other.transform.position, other.transform.rotation);
                }
                gm.ChangeScore(brickScript.pointForBrick);
                Transform exp = Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(exp.gameObject, 3);
                Destroy(other.gameObject);
                gm.ChangeNumberOfBricks();
            }
        }
    }
}
