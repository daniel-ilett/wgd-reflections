using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    private PlayerController player;

    private new Rigidbody2D rigidbody;

    private void Start()
    {
        player = PlayerController.instance;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var direction = (player.transform.position - transform.position).normalized;
        rigidbody.velocity = direction * speed;
    }

    public void GetHit()
    {
        gameObject.SetActive(false);
    }
}
