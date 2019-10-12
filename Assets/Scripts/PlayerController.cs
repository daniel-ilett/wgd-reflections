using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    private Vector2 moveVector;

    private new Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CalculateMove();
    }

    private void CalculateMove()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        moveVector = new Vector2(horizontal, vertical);

        if(moveVector.magnitude > 1.0f)
        {
            moveVector.Normalize();
        }
    }
    
    private void FixedUpdate()
    {
        rigidbody.velocity = moveVector * speed;
    }
}
