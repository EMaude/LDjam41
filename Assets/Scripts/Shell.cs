using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    public float ShellVelocity;
    public Vector2 Faceing = Vector2.right;
    float timer;
    Rigidbody2D rb;
    SpriteRenderer sr;

    GameObject spawner;

    public void SetProperties(float shellVel, Vector2 faceing, GameObject s)
    {
        ShellVelocity = shellVel;
        Faceing = faceing;
        spawner = s;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (Faceing == Vector2.left)
        {
            sr.flipX = true;
        }

        rb.AddForce(Faceing * ShellVelocity, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if(rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject + " : " + coll.gameObject.tag);

        if (coll.gameObject != spawner)
        {
            if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy")
            {
                Destroy(coll.gameObject);
            }

            if (coll.gameObject.tag != "NoCollision")
            {
                Destroy(this.gameObject);
            }
        }
    }
}
