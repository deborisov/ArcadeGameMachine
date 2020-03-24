using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Explodable))]
public class ExplodeOnClick : MonoBehaviour
{

    private Explodable _explodable;

    void Start()
    {
        _explodable = GetComponent<Explodable>();
    }
    public void Explode()
    {
        Debug.Log("Mouse down");
        _explodable.GetComponent<Rigidbody2D>().simulated = true;
        _explodable.explode();
        ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
        ef.doExplosion(transform.position);
    }
}
