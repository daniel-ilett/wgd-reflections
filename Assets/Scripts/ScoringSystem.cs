using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    [SerializeField]
    private ScoreDisplay scoreDisplay;

    [SerializeField]
    private int pointsPerEnemy = 100;

    [SerializeField]
    private float modifier = 0.2f;

    private int totalPoints;

    private int enemiesHit;

    public static ScoringSystem instance;

    // Cache singleton reference.
    private void Awake()
    {
        instance = this;
    }

    // When an enemy is killed, score points depending on multiplier.
    public int ScorePoints(Vector2 position)
    {
        int increase = (int)(pointsPerEnemy * (1.0f + enemiesHit * modifier));
        totalPoints += increase;
        ++enemiesHit;
        
        ChangeDisplay();
        TooltipCanvas.instance.CreateTooltip(position, increase.ToString());

        return increase;
    }

    // When the bolt is collected, add bonus points.
    public int CollectBolt(Vector2 position)
    {
        if(enemiesHit > 0)
        {
            int increase = (int)(pointsPerEnemy * (1.0f + enemiesHit * modifier));
            totalPoints += increase;

            TooltipCanvas.instance.CreateTooltip(position, "BONUS" + increase);
            ResetModifier();

            return increase;
        }
        else
        {
            return 0;
        }
    }

    // When the bolt expires, just reset the multiplier.
    public void BoltDeath()
    {
        ResetModifier();
    }

    private void ResetModifier()
    {
        enemiesHit = 0;
        ChangeDisplay();
    }

    // Tell scoreboard to show new point total.
    private void ChangeDisplay()
    {
        scoreDisplay.SetText(totalPoints, 1.0f + enemiesHit * modifier);
    }

    public int GetScore()
    {
        return totalPoints;
    }
}
