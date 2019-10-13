using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class TooltipCanvas : MonoBehaviour
{
    [SerializeField]
    private Tooltip tooltipPrefab;

    public static TooltipCanvas instance;

    // Set singleton reference.
    private void Awake()
    {
        instance = this;
    }

    // Create a tooltip object on the canvas.
    public void CreateTooltip(Vector2 position, string message)
    {
        var newPos = position + new Vector2(0.0f, 0.5f);

        var newTip = Instantiate(tooltipPrefab, position, Quaternion.identity, transform);
        newTip.SetTextAndPos(message, newPos);
    }
}
