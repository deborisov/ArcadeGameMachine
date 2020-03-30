using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAcceleration : MonoBehaviour
{
    private bool move;
    private Vector3 target;
    float acceleration = 0.3f;
    float speed = 0;
    void Update()
    {
        if (move)
        {
            Debug.Log(transform.position);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            speed += acceleration;
            if (transform.position == target)
            {
                move = false;
            }
        }
    }

    public void StartMove(Vector3 target)
    {
        move = true;
        this.target = target;
    }
}
