using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    private PlayerController player;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float minSize = 4.0f;
    
    private LightningBolt lightningBolt;

    private Vector3 targetPos = Vector3.zero;
    private float targetZoom = 4.0f;

    private new Camera camera;

    // Cache components.
    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    // Cache otjher gameObjects and set initial target.
    private void Start()
    {
        player = PlayerController.instance;

        lightningBolt = player.GetLightningBolt();
        SetTargetPos(player.transform.position);
    }

    private void Update()
    {
        SetTargetPos();
        SetCameraZoom();
    }

    // Move towards the target position.
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * speed);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, Time.fixedDeltaTime * speed);
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

    // Zoom in and out depending on the bolt and player positions.
    private void SetCameraZoom()
    {
        if(lightningBolt.isActiveAndEnabled)
        {
            float xDist = Mathf.Abs(player.transform.position.x - lightningBolt.GetHeadPosition().x);
            float yDist = Mathf.Abs(player.transform.position.y - lightningBolt.GetHeadPosition().y);

            float xZoom = xDist / camera.aspect * 0.75f;
            float yZoom = yDist * 0.75f;

            targetZoom = Mathf.Max(Mathf.Max(xZoom, yZoom), minSize);
        }
        else
        {
            targetZoom = minSize;
        }
    }

    // Set the target position, preserving the existing z-component.
    private void SetTargetPos(Vector2 pos)
    {
        targetPos = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
