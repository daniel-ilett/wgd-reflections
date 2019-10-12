using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightningBolt : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float maxLength = 1.0f;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float maxLifetime = 3.0f;
    [SerializeField]
    private float pickupThreshold = 2.0f;

    private float lifetime = 0.0f;

    private new LineRenderer renderer;

    //private List<Vector2> positions;
    private Vector2 direction;

    private void Awake()
    {
        renderer = GetComponent<LineRenderer>();
        Reset();
    }

    private void Update()
    {
        FireRay(Time.deltaTime * speed, new List<Transform>());
        ShortenRay();
        lifetime -= Time.deltaTime;

        if (lifetime < 0.0f)
        {
            Die();
        }
    }

    // Fire a ray forward and reflect when a wall is hit.
    // The ray hurts enemies and is picked up by the player after a certain time.
    private void FireRay(float distance, List<Transform> alreadyHit)
    {
        Debug.Log("Firing Ray.");
        // Forward ray: 

        // Raycast distance of deltaTime * speed in front.

        // If hit enemy: hurt them.
        // If hit player and cooldown is reached: get collected.
        // If hit wall: reflect ray and do secondary raycast, 
        //     ignoring that wall.

        // Backward ray:

        // Propogate backwards through position list and make
        //     ray segments smaller. If ray segments disappear,
        //     then go to next ray segment and delete.

        RaycastHit2D[] hitObjects =
            Physics2D.RaycastAll(renderer.GetPosition(0), direction, distance, layerMask);

        bool hasHit = false;
        Vector2 hitPoint = Vector3.zero;
        Vector2 hitNormal = Vector3.zero;

        foreach (var hitObject in hitObjects)
        {
            if (!alreadyHit.Contains(hitObject.transform))
            {
                switch (hitObject.transform.tag)
                {
                    case "Player":
                        {
                            var player = hitObject.transform.GetComponent<PlayerController>();

                            if (lifetime <= pickupThreshold)
                            {
                                Reset();
                                return;
                            }
                        }
                        break;
                    case "Enemy":
                        {
                            alreadyHit.Add(hitObject.transform);
                            Debug.Log("Hurt the enemy.");
                        }
                        break;
                    case "Wall":
                        {
                            alreadyHit.Add(hitObject.transform);
                            hasHit = true;
                            hitPoint = hitObject.point;
                            hitNormal = hitObject.normal;
                        }
                        break;
                }
            }
        }

        if (hasHit)
        {
            // Set the head to the hit point.
            distance -= Vector2.Distance(renderer.GetPosition(0), hitPoint);
            renderer.SetPosition(0, hitPoint);

            direction = Vector2.Reflect(direction, hitNormal).normalized;

            // Add a position at the tail and propogate values down the list.
            renderer.positionCount += 1;
            for(int i = renderer.positionCount - 1; i > 0; --i)
            {
                renderer.SetPosition(i, renderer.GetPosition(i - 1));
            }

            FireRay(distance, alreadyHit);
        }
        else
        {
            renderer.SetPosition(0, (Vector2)renderer.GetPosition(0) + direction * distance);
        }
    }

    // Propogate through ray backwards and shorten where appropriate.
    private void ShortenRay()
    {
        float length = 0.0f;

        for(int i = 0; i < renderer.positionCount - 1; ++i)
        {
            length += Vector2.Distance(renderer.GetPosition(i), renderer.GetPosition(i + 1));
        }

        while (length > maxLength)
        {
            var positionCount = renderer.positionCount;
            var tailPos = renderer.GetPosition(positionCount - 1);
            var nextPos = renderer.GetPosition(positionCount - 2);
            
            var segmentLength = Vector2.Distance(tailPos, nextPos);

            if (segmentLength >= length - maxLength)
            {
                var newTailPos = nextPos + (tailPos - nextPos).normalized * (segmentLength - (length - maxLength));
                renderer.SetPosition(positionCount - 1, newTailPos);
                length = maxLength;
            }
            else
            {
                length -= segmentLength;
                renderer.positionCount -= 1;
            }
        }
    }

    // Activate the GameObject and set the start position + direction.
    public void Fire(Vector2 position, Vector2 direction)
    {
        gameObject.SetActive(true);
        this.direction = direction.normalized;
        renderer.SetPosition(0, position);
        renderer.SetPosition(1, position);
        lifetime = maxLifetime;
    }

    private void Die()
    {
        Reset();
    }

    private void Reset()
    {
        gameObject.SetActive(false);
        renderer.positionCount = 2;
    }

    public Vector3 GetHeadPosition()
    {
        return renderer.GetPosition(0);
    }
}
