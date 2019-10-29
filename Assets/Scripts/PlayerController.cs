using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float hurtAmount = 10.0f;

    [SerializeField]
    private float immunityPeriod = 0.5f;

    [SerializeField]
    private float maxHealth = 100.0f;

    private float health;

    [SerializeField]
    private AnimationCurve hurtTintCurve;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private LightningBolt lightningBoltPrefab;

    private LightningBolt lightningBolt;

    private Vector2 moveVector = Vector2.zero;

    private bool canBeHit = true;

    public static int score;

    private Animator anim;
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
        anim = GetComponent<Animator>();
        lightningBolt = Instantiate(lightningBoltPrefab);

        health = maxHealth;
    }

    // Attempt to move and then try to fire a projectile.
    private void Update()
    {
        CalculateMove();

        if(moveVector.magnitude > 0.1f)
        {
            CalculateFacingDirection();
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

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

    private void CalculateFacingDirection()
    {
        if(Mathf.Abs(moveVector.x) > Mathf.Abs(moveVector.y))
        {
            if(moveVector.x < 0.0f)
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
            if(moveVector.y < 0.0f)
            {
                anim.SetInteger("Direction", 1);
            }
            else
            {
                anim.SetInteger("Direction", 2);
            }
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Enemy" && canBeHit)
        {
            GetHit();
        }
    }

    private void GetHit()
    {
        canBeHit = false;

        health -= hurtAmount;
        HealthBar.instance.SetHealth(health / maxHealth);

        if (health <= 0.0f)
        {
            Die();
        }

        StartCoroutine(WaitForCanBeHit());
    }

    private void Die()
    {
        score = ScoringSystem.instance.GetScore();
        SceneManager.LoadScene("GameOver");
    }

    // Wait for the umminity period and tint the player colour red.
    private IEnumerator WaitForCanBeHit()
    {
        for (float t = 0.0f; t < immunityPeriod; t += Time.deltaTime)
        {
            spriteRenderer.color = Color.Lerp(Color.red, Color.white, t / immunityPeriod);
            yield return null;
        }

        spriteRenderer.color = Color.white;
        canBeHit = true;
    }
}
