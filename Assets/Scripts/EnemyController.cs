using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float EnemySpeed;
    public Vector2 Faceing = Vector2.left;

    [Header("Shooting")]
    public GameObject Shell;
    public float ShellVelocity;
    public float ShotTimeOut;
    private float shotTimeOut;
    public float ShellKnockbackForce;
    public float xoffset;
    public float yoffset;

    public GameController GC;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Vector2 pos;
    bool canSeePlayer = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pos = rb.position;
        shotTimeOut = ShotTimeOut;
	}
    void FixedUpdate()
    {
        if(shotTimeOut > 0)
        {
            shotTimeOut -= 1;
        }

        if (GC.GetTurnState() == TurnStates.Enemies)
        {
            if (canSeePlayer)
            {
                if (shotTimeOut <= 0)
                {
                    Shoot();
                }
            }
            else
            {
                pos += Faceing * EnemySpeed;
                rb.MovePosition(pos);
            }
        }
    }

    void Shoot()
    {
        Vector2 spawnPos;
        if (Faceing == Vector2.right)
        {
            spawnPos = new Vector2(transform.position.x + xoffset, transform.position.y + yoffset);
        }
        else
        {
            spawnPos = new Vector2(transform.position.x - xoffset, transform.position.y + yoffset);
        }

        GameObject s = Instantiate(Shell, spawnPos, transform.rotation);
        s.GetComponent<Shell>().SetProperties(ShellVelocity, Faceing, this.gameObject);
        rb.AddForce(-Faceing * ShellKnockbackForce, ForceMode2D.Impulse);
        shotTimeOut = ShotTimeOut * 60;
    }

    public void  CanSeePlayer()
    {
        canSeePlayer = true;
    }
    
    public void LostPlayer()
    {
        canSeePlayer = false;
    }

    public void TurnAround()
    {
        if(!canSeePlayer)
        {
            if (Faceing == Vector2.left)
            {
                Faceing = Vector2.right;
                GetComponentInChildren<LightController>().Flip();
                sr.flipX = true;
            }
            else
            {
                Faceing = Vector2.left;
                GetComponentInChildren<LightController>().Flip();
                sr.flipX = false;
            }
        }
    }
}
