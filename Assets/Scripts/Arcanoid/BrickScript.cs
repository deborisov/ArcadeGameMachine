using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int pointForBrick;
    public int hitsToBreak;
    public Sprite hitSprite;
    public Transform explosion;

    public void BreakBrick()
    {
        hitsToBreak--;
        GetComponent<SpriteRenderer>().sprite = hitSprite;
    }

    public void Explode()
    {
        Transform exp = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(exp.gameObject, 3);
        Destroy(gameObject);
    }
}
