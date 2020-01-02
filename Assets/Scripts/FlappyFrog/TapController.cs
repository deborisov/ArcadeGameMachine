using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour
{
    public float tapForce = 200;
    public float tiltSmooth = 1.5f;
    public Vector3 startPos;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (/*Input.touchCount > 0*/ Input.GetMouseButtonDown(0))
        {
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
        }
        if (collision.gameObject.tag == "ScoreZone")
        {
            
        }
    }
}
