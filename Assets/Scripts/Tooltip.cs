using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private float riseAmount;

    private Text tooltipText;

    // Cache components.
    private void Awake()
    {
        tooltipText = GetComponent<Text>();
    }

    // Change the tooltip message and set position.
    public void SetTextAndPos(string message, Vector2 position)
    {
        tooltipText.text = message;
        StartCoroutine(Rise());
    }

    // Rise and fade within the curve time.
    private IEnumerator Rise()
    {
        var startPos = transform.position;
        for(float t = 0.0f; t < curve[curve.length - 1].time; t += Time.deltaTime)
        {
            var amount = curve.Evaluate(t);
            transform.position = startPos - new Vector3(0.0f, amount * riseAmount, 0.0f);
            tooltipText.color = new Color(1.0f, 1.0f, 1.0f, amount);
            yield return null;
        }

        Destroy(gameObject);
    }
}
