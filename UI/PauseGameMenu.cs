using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGameMenu : MonoBehaviour
{
    public static PauseGameMenu instance;
    public string mainMenu, levelrestart;
    public GameObject pauseScreen;
    public GameObject gameControlPanel;
    public GameObject gameTipsPanel;
    public bool isPaused;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.Play("Pause");
            PauseUnpause();
        }
    }
    public void PauseUnpause()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void MainMenu()
    {
        AudioManager.instance.Play("Tap");
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void ShowGameInstructions()
    {
        AudioManager.instance.Play("Tap");
        gameControlPanel.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void NextGameInstruction()
    {
        AudioManager.instance.Play("Tap");
        gameControlPanel.SetActive(false);
        gameTipsPanel.SetActive(true);
    }
}
