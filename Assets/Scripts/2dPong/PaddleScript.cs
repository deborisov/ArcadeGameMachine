using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public bool isRight;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (isRight)
        {
            transform.position = new Vector2 (screenBounds.x - objectWidth * 2, 0);
        }
        else
        {
            transform.position = new Vector2(-screenBounds.x + objectWidth * 2, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
