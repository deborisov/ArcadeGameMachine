using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public float tapForce = 200;
    public float tiltSmooth = 1.5f;
    public Vector3 startPos;

    FlappyBirdGameManager game;

    Rigidbody2D rigidbody;
    Quaternion downRotation;
    Quaternion forwardRotation;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -50);
        forwardRotation = Quaternion.Euler(0, 0, 40);
        transform.position = startPos;
        game = FlappyBirdGameManager.Instance;
        rigidbody.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.GameOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (Random.Range(0f, 1f) <= 0.1)
            {
                Time.timeScale += 0.03f;
            }
            rigidbody.velocity = Vector3.zero;
            transform.rotation = forwardRotation;
            rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeadZone")
        {
            rigidbody.simulated = false;
            OnPlayerDied();
        }
        if (collision.gameObject.tag == "ScoreZone")
        {
            OnPlayerScored();
            if (game.Won)
            {
                rigidbody.simulated = false;
                OnPlayerDied();
            }
        }
    }

    private void OnEnable()
    {
        FlappyBirdGameManager.OnGameStarted += OnGameStarted;
        FlappyBirdGameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    private void OnDisable()
    {
        FlappyBirdGameManager.OnGameStarted -= OnGameStarted;
        FlappyBirdGameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true;
    }

    void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
    }
}
