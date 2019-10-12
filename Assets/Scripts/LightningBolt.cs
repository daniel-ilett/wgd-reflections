using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightningBolt : MonoBehaviour
{
    private new LineRenderer renderer;

    private bool hasFired = false;

    private List<Vector2> positions;
    private Vector2 direction;

    private void Start()
    {
        renderer = GetComponent<LineRenderer>();
        Reset();
    }

    private void Update()
    {
        if(hasFired)
        {
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
                Physics2D.RaycastAll(positions[0], direction, Time.deltaTime);
            
            foreach(var hitObject in hitObjects)
            {
                
            }
        }
    }

    public void Fire(Vector2 position, Vector2 direction)
    {
        hasFired = true;
        this.direction = direction;
    }

    private void Reset()
    {
        gameObject.SetActive(false);
        renderer.positionCount = 1;
        positions = new List<Vector2>();
    }
}
