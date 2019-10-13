using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;

    public static HealthBar instance;

    // Assign singleton reference.
    private void Awake()
    {
        instance = this;
    }

    // Set the size of the health bar.
    public void SetHealth(float percentage)
    {
        healthBar.fillAmount = percentage;
    }
}
