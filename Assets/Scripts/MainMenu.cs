using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
    public Button helpButton;
    public GameObject helpCanvas;
    public Text bestScoreText;

    private bool helpCanvasVisible = false;
    
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
        helpButton.onClick.AddListener(OnHelpButtonClick);
        bestScoreText.text = PlayerPrefs.GetInt("best_score", 0).ToString();
    }

    void OnPlayButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    void OnExitButtonClick()
    {
        Application.Quit();
    }

    void OnHelpButtonClick()
    {
        helpCanvasVisible = !helpCanvasVisible;
        helpCanvas.SetActive(helpCanvasVisible);
    }
}
