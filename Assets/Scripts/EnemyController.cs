using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float EnemySpeed;
    public int MoveLength;
    public Vector2 Faceing = Vector2.left;

    public GameController GC;

    Rigidbody2D rb;
    SpriteRenderer sr;

    private Vector2 startPos;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
        sr = GetComponent<SpriteRenderer>();
	}

    void FixedUpdate()
    {
        if (GC.GetTurnState() == TurnStates.Enemies)
        {
            if (Faceing == Vector2.right)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

            rb.velocity += Faceing * EnemySpeed;

            if (Vector2.Distance(rb.position, startPos) < MoveLength)
            {
                    Faceing = Vector2.right;
            }
            else if (Vector2.Distance(rb.position, startPos) > MoveLength)
            {
                    Faceing = Vector2.left;
            }
        }
    }
}
