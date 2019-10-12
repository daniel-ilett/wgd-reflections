using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private float speed = 1.0f;
    
    private LightningBolt lightningBolt;

    private Vector3 targetPos = Vector3.zero;

    private void Start()
    {
        lightningBolt = player.GetLightningBolt();
        SetTargetPos(player.transform.position);
    }

    private void Update()
    {
        SetTargetPos();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
    }

    // Set an anchor point between the player and head of the laser.
    private void SetTargetPos()
    {
        if(lightningBolt.isActiveAndEnabled)
        {
            var lightningPos = lightningBolt.GetHeadPosition();
            SetTargetPos((player.transform.position * 2.0f + lightningPos) / 3.0f);
        }
        else
        {
            SetTargetPos(player.transform.position);
        }
    }

    private void SetTargetPos(Vector2 pos)
    {
        targetPos = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
