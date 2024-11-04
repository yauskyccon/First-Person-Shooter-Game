using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameControlPanel;
    public GameObject gameTipsPanel;
    public Button nextButton;
    public Button playButton;

    void Start()
    {
        Time.timeScale = 0f; // Pause the game

        // Ensure the initial state of the UI
        gameControlPanel.SetActive(true);
        gameTipsPanel.SetActive(false);


        // Attach button listeners
        nextButton.onClick.AddListener(OnNextButtonClicked);
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    public void OnNextButtonClicked()
    {
        AudioManager.instance.Play("Tap");
        // Show Game Tips UI and hide Game Control UI
        gameControlPanel.SetActive(false);
        gameTipsPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void OnPlayButtonClicked()
    {
        AudioManager.instance.Play("Tap");
        // Hide Game Tips UI and start the game
        gameTipsPanel.SetActive(false);

        // You can add any additional logic to start the game here
        StartGame();
    }

    void StartGame()
    {
        Time.timeScale = 1f; // Unpause the game
    }
}
