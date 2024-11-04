using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WonLoseUIManager : MonoBehaviour
{
    public static WonLoseUIManager instance;

    public GameObject loseUI;
    public GameObject winUI;
    public Text scoreText;

    // Scene names to be set in the Inspector
    public string mainMenuScene;
    public string currentLevelScene;
    public string nextLevelScene;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        HideWinUI();
        HideLoseUI();
    }

    public void PlayerLose()
    {
        Time.timeScale = 0f; // Pause the game
        ShowLoseUI();
    }

    public void PlayerWin(int score)
    {
        Time.timeScale = 0f; // Pause the game
        ShowWinUI(score);
    }

    public void RestartLevel()
    {
        AudioManager.instance.Play("Tap");
        SceneManager.LoadScene(currentLevelScene);
        Time.timeScale = 1f; // Resume the game
    }

    public void LoadMainMenu()
    {
        AudioManager.instance.Play("Tap");
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1f; // Resume the game
    }

    public void LoadNextLevel()
    {
        AudioManager.instance.Play("Tap");
        SceneManager.LoadScene(nextLevelScene);
        Time.timeScale = 1f; // Resume the game
    }

    void ShowWinUI(int score)
    {
        winUI.SetActive(true);
        scoreText.text = "Diamond Collected:  " + score;
    }

    void HideWinUI()
    {
        winUI.SetActive(false);
    }

    void ShowLoseUI()
    {
        loseUI.SetActive(true);
    }

    void HideLoseUI()
    {
        loseUI.SetActive(false);
    }
}
