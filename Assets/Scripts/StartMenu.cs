﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("FireKeyboard") || Input.GetButtonDown("FireController"))
        {
            SceneManager.LoadScene("MainScene");
        }
        else if(Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
