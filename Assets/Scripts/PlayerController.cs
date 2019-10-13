using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private LightningBolt lightningBoltPrefab;

    private LightningBolt lightningBolt;

    private Vector2 moveVector = Vector2.zero;

    private new Rigidbody2D rigidbody;

    public static PlayerController instance = null;

    // Cache components and create objects.
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rigidbody = GetComponent<Rigidbody2D>();
        lightningBolt = Instantiate(lightningBoltPrefab);
    }

    // Attempt to move and then try to fire a projectile.
    private void Update()
    {
        CalculateMove();

        if(!lightningBolt.gameObject.activeSelf)
        {
            ShootBolt();
        }
    }

    // Calculate the move vector and cache ready for the next FixedUpdate.
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

    // Attempt to fire a projectile.
    private void ShootBolt()
    {
        if(Input.GetButtonUp("FireKeyboard"))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var direction = mousePos - transform.position;

            lightningBolt.Fire(transform.position, direction / direction.magnitude);
        }
        else if(Input.GetButtonUp("FireController"))
        {
            Vector2 direction = new Vector2(Input.GetAxis("LookX"), Input.GetAxis("LookY"));

            if (direction == Vector2.zero)
            {
                direction = moveVector;
            }

            lightningBolt.Fire(transform.position, direction / direction.magnitude);
        }
    }
    
    // Move the player according to the value cached by Update.
    private void FixedUpdate()
    {
        rigidbody.velocity = moveVector * speed;
    }

    public LightningBolt GetLightningBolt()
    {
        return lightningBolt;
    }
}
