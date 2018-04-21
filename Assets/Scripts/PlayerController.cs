using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameController GC;


    public float MoveSpeed;
    public float MoveSpeedJumping;
    public float JumpVelocity;

    bool JumpReq;
    bool Jumping;
    Rigidbody2D rb;
    
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") > 0 && !Jumping && GC.GetTurnState() == TurnStates.Player)
        {
            JumpReq = true;
        }
    }
    void FixedUpdate()
    {
        if (GC.GetTurnState() == TurnStates.Player)
        {
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
}