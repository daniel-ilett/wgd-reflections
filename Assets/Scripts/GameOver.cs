using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    // Set the score text.
    private void Start()
    {
        scoreText.text = PlayerController.score + "pts";
    }

    private void Update()
    {
        if(Input.GetButtonDown("FireKeyboard") || Input.GetButtonDown("FireController"))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
