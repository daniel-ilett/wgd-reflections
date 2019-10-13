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

    // When the bolt is collected, 
    public int CollectBolt()
    {
        int increase = (int)(pointsPerEnemy * (1.0f + enemiesHit * modifier));
        totalPoints += increase;
        ResetModifier();

        return increase;
    }

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
}
