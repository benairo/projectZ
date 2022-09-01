using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public PlayerInput playerInput;

    public GameObject pauseMenuUI;
    [SerializeField]
    private InputAction pauseButton;
    

    private void OnEnable()
    {
        pauseButton.Enable();
    }

    private void OnDisable()
    {
        pauseButton.Disable();
    }

    private void Start()
    {
        pauseButton.performed += _ => Pause();
    }
    
    public void Pause()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }
    }

    public void Options()
    {
        print("PRESSED OPTIONS");
    }

    public void Quit()
    {
        print("PRESSED QUIT");
    }
}
