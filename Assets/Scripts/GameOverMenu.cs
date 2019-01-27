using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Button replayButton;
    public Button exitButton;
    public Text scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        replayButton.onClick.AddListener(onReplayButtonClick);
        exitButton.onClick.AddListener(onExitButtonClick);
        scoreText.text = PlayerPrefs.GetInt("previous_score", 0).ToString();
    }

    void onReplayButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    void onExitButtonClick()
    {
        Application.Quit();
    }
}
