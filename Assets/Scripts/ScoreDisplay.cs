using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text multiplierText;

    // Modify score contents.
    public void SetText(int totalPoints, float multiplier)
    {
        scoreText.text = totalPoints + " pts";
        multiplierText.text = "x" + multiplier;
    }
}
