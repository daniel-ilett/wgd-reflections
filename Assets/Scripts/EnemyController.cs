using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Sprite sprites;

    private PlayerController player;

    private Animator anim;
    private new Rigidbody2D rigidbody;

    // Cache components.
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

        anim.SetBool("IsMoving", true);
    }

    // Find other objects.
    private void Start()
    {
        player = PlayerController.instance;
    }

    // Update the walking direction.
    private void FixedUpdate()
    {
        var direction = (player.transform.position - transform.position).normalized;
        rigidbody.velocity = direction * speed;

        // Set the facing direction.
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x < 0.0f)
            {
                anim.SetInteger("Direction", 0);
            }
            else
            {
                anim.SetInteger("Direction", 3);
            }
        }
        else
        {
            if (direction.y < 0.0f)
            {
                anim.SetInteger("Direction", 1);
            }
            else
            {
                anim.SetInteger("Direction", 2);
            }
        }
    }

    public void SetSprites(Sprite spriteSheet)
    {
        spriteRenderer.sprite = spriteSheet;
        sprites = spriteSheet;
    }

    public void GetHit()
    {
        gameObject.SetActive(false);
        ScoringSystem.instance.ScorePoints(transform.position);
    }
}
