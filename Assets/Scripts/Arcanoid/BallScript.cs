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
    public GameManager gm;
    public Transform extraLife;
    public Transform increaseUpgrade;
    public Transform decreaseUpgrade;
    public AudioSource blup;

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
                var audioManager = FindObjectOfType<AudioManager>();
                audioManager.Play("Hit");
            }
            else
            {
                UpgradeLogic(other);
                gm.ChangeScore(brickScript.pointForBrick);
                brickScript.Explode();
                gm.ChangeNumberOfBricks();
                var audioManager = FindObjectOfType<AudioManager>();
                audioManager.Play("Blup");
            }
        }
        else if (!gm.StartPage.activeSelf)
        {
            var audioManager = FindObjectOfType<AudioManager>();
            audioManager.Play("Hit");
        }
    }

    private void UpgradeLogic(Collision2D other)
    {
        int chanceForUpgrade = Random.Range(1, 11);
        if (chanceForUpgrade < 3)
        {
            chanceForUpgrade = Random.Range(1, 4);
            switch (chanceForUpgrade)
            {
                case 1: Instantiate(decreaseUpgrade, other.transform.position, other.transform.rotation); break;
                case 2: Instantiate(increaseUpgrade, other.transform.position, other.transform.rotation); break;
                case 3: Instantiate(extraLife, other.transform.position, other.transform.rotation); break;
            }
        }
    }
}
