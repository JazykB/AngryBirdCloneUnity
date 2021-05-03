using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBird2 : Bird
{
    Collider2D[] inExplosionRadius = null;
    [SerializeField] private float ExplosionForceMulti = 5;
    [SerializeField] private float ExplosionRadius = 5;

    public bool _hasExploded = false;

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Bomb");
        string tag = col.gameObject.tag;
        if (tag == "Enemy" || tag == "Obstacle" && !_hasExploded)
        {
            Debug.Log("Bomb tagged");
            Explode();
            _hasExploded = true;
        }
    }

    void Explode()
    {
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

        foreach (Collider2D o in inExplosionRadius)
        {
            Rigidbody2D o_rigidbody = o.GetComponent<Rigidbody2D>();
            if (o_rigidbody != null)
            {
                Vector2 distanceVector = o.transform.position - transform.position;
                if (distanceVector.magnitude > 0)
                {
                    float explosionForce = ExplosionForceMulti / distanceVector.magnitude;
                    o_rigidbody.AddForce(distanceVector.normalized * explosionForce);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
