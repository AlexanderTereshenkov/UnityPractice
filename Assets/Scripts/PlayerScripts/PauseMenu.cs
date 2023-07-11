using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject fridjeInteraction;
    private bool isGamePaused = false;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void SetValues(bool value)
    {
        if (value) fridjeInteraction.SetActive(false);
        playerController.SetIsMovingPossible(!value);
        Cursor.lockState = value == true ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = value;
        Time.timeScale = value == true ? 0 : 1;
        pauseMenu.SetActive(value);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isGamePaused = !isGamePaused;
        SetValues(isGamePaused);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
