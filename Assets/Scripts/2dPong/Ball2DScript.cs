using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2DScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public delegate void GameDelegate();
    public static event GameDelegate OnPlayerScored;
    public static event GameDelegate OnPlayerDied;
    public GameObject ball;
    public twoDPongGameManager gm;
    public float timeToScale = 0.5f;
    public (bool, bool) scalingSide = (false, false);
    float targetScale = 0.2f;
    public float delta = 0.1f;
    public float timeForUpgrade = 5f;
    bool stopCycle = false;
    bool boosted;
    float timeForBoost;

    void Start()
    {
        gm = GameObject.Find("Main Camera").GetComponent<twoDPongGameManager>();
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
        rb.simulated = true;
        Debug.Log(rb.velocity);
    }

    void StopSimulating()
    {
        rb.simulated = false;
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
        OnPlayerDied += StopSimulating;
        OnPlayerDied += PlaceOnStart;
        OnPlayerDied += ResetUpgrades;
        OnPlayerScored += ResetUpgrades;
    }

    private void OnDisable()
    {
        twoDPongGameManager.OnGameStarted -= ThrowBall;
        OnPlayerScored -= PlaceOnStart;
        OnPlayerDied -= StopSimulating;
        OnPlayerDied -= PlaceOnStart;
        OnPlayerDied -= ResetUpgrades;
        OnPlayerScored -= ResetUpgrades;
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
        if (other.CompareTag("Double"))
        {
            if (!scalingSide.Item1 && !scalingSide.Item2)
            {
                timeForUpgrade = 15;
                scalingSide = (true, false);
                delta = Mathf.Abs(delta);
                targetScale = transform.localScale.x + delta;
                var shape = transform.Find("Trail").transform.Find("Trails").GetComponent<ParticleSystem>().shape;
                shape.radius = 0.37f;
            }
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Speed"))
        {
            if (!boosted)
            {
                boosted = true;
                var localVelocity = rb.velocity;
                rb.velocity += rb.velocity / 4;
                timeForBoost = 15f;
                transform.Find("Trail").gameObject.SetActive(true);
                var audioManager = FindObjectOfType<AudioManager>();
                audioManager.Play("Wind");
            }
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        if (scalingSide.Item1 || scalingSide.Item2)
        {
            timeForUpgrade -= Time.deltaTime;
            if (timeForUpgrade <= 0 && !stopCycle)
            {
                scalingSide = (scalingSide.Item2, scalingSide.Item1);
                delta = -delta;
                targetScale += delta;
                stopCycle = true;
                var shape = transform.Find("Trail").transform.Find("Trails").GetComponent<ParticleSystem>().shape;
                shape.radius = 0.18f;
            }
            if (scalingSide.Item1 && targetScale <= transform.localScale.x ||
                scalingSide.Item2 && targetScale >= transform.localScale.x)
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
            temp.y = transform.localScale.y + delta / timeToScale * Time.deltaTime;
            temp.z = transform.localScale.z + delta / timeToScale * Time.deltaTime;
            transform.localScale = temp;
        }
        if (boosted)
        {
            timeForBoost -= Time.deltaTime;
            if (timeForBoost < 0)
            {
                var audioManager = FindObjectOfType<AudioManager>();
                audioManager.Stop("Wind");
                rb.velocity -= rb.velocity / 5;
                boosted = false;
                transform.Find("Trail").gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gm.gameStarted)
        {
            var audioManager = FindObjectOfType<AudioManager>();
            audioManager.Play("Hit");
        }
    }

    private void ResetUpgrades()
    {
        NullSpeed();
        NullDouble();
    }

    private void NullSpeed()
    {
        if (boosted)
        {
            var audioManager = FindObjectOfType<AudioManager>();
            audioManager.Stop("Wind");
            rb.velocity -= rb.velocity / 5;
            boosted = false;
            transform.Find("Trail").gameObject.SetActive(false);
        }
    }

    private void NullDouble()
    {
        if (scalingSide.Item1 || scalingSide.Item2)
        {
            var shape = transform.Find("Trail").transform.Find("Trails").GetComponent<ParticleSystem>().shape;
            shape.radius = 0.17f;
            scalingSide = (false, false);
            stopCycle = false;
            transform.localScale = new Vector3(0.1f, 0.1f, 0);
        }
    }
}
