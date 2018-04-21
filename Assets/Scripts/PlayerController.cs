using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Controllers")]
    public GameController GC;


    [Header("Movement")]
    public float  MoveSpeed;
    public float MoveSpeedJumping;
    public float JumpVelocity;

    [Header("Shooting")]
    public GameObject Shell;
    public float ShellVelocity;
    public float ShotTimeOut;
    private float shotTimeOut;
    public float ShellKnockbackForce;
    public float ShellKnockbackForceJumping;

    bool JumpReq;
    bool Jumping;
    bool ShotAvail;
    bool ShotReq;

    Vector2 Faceing = Vector2.right;

    Rigidbody2D rb;
    SpriteRenderer sr;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        shotTimeOut = 0;
        ShotAvail = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") > 0 && !Jumping && GC.GetTurnState() == TurnStates.Player)
        {
            JumpReq = true;
        }

        if (Input.GetAxis("Fire1") > 0 && ShotAvail && GC.GetTurnState() == TurnStates.Player)
        {
            ShotReq = true;
        }
    }
    void FixedUpdate()
    {
        if(shotTimeOut > 0)
        {
            shotTimeOut -= 1;
        }
        else if(shotTimeOut <= 0)
        {
            ShotAvail = true;
        }

        if (GC.GetTurnState() == TurnStates.Player)
        {
            if(ShotReq)
            {
                Shoot();
                if (Jumping)
                {
                    rb.AddForce(-Faceing * ShellKnockbackForceJumping, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(-Faceing * ShellKnockbackForce, ForceMode2D.Impulse);
                }
                shotTimeOut = ShotTimeOut * 60;
                ShotAvail = false;
                ShotReq = false;
            }

            if (JumpReq)
            {
                rb.AddForce(Vector2.up * JumpVelocity, ForceMode2D.Impulse);
                Jumping = true;
                JumpReq = false;
            }

            if (Jumping)
            {
                rb.velocity += Vector2.right * (Input.GetAxis("Horizontal") * MoveSpeedJumping);
            }
            else
            {
                rb.velocity += Vector2.right * (Input.GetAxis("Horizontal") * MoveSpeed);
            }

            if (rb.velocity.y == 0)
            {
                Jumping = false;
            }
        }
    }

    public void OnDestroy()
    {
        GC.GameOver();
    }

    void Shoot()
    {
        Vector2 spawnPos;
        if (Faceing == Vector2.right)
        {
            spawnPos = new Vector2(transform.position.x + 0.481f, transform.position.y + 0.139f);
        }
        else
        {
            spawnPos = new Vector2(transform.position.x - 0.481f, transform.position.y + 0.139f);
        }
        GameObject s = Instantiate(Shell, spawnPos, transform.rotation);
        s.GetComponent<Shell>().SetProperties(ShellVelocity, Faceing, this.gameObject);
    }

    public void TurnAround()
    {
        if (Faceing == Vector2.left)
        {
            Faceing = Vector2.right;
            sr.flipX = true;
        }
        else
        {
            Faceing = Vector2.left;
            sr.flipX = false;
        }
    }
}