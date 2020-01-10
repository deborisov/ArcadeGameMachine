using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PannelScript : MonoBehaviour
{
    public float speed;
    public GameManager gm;

    void Update()
    {
        /*if (gm.gameOver)
        {
            return;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).tapCount <= 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (touchPosition.x > 0 )
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
        }*/
    }

    private void OnEnable()
    {
        Joystick.OnMakeMove += MakeMove;
    }

    private void OnDisable()
    {
        Joystick.OnMakeMove -= MakeMove;
    }

    void MakeMove(Vector2 dir)
    {
        Vector2 direction = new Vector2(dir.x, 0);
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ExtraLife")){
            gm.ChangeLives(1);
            Destroy(other.gameObject);
        }
    }
}
