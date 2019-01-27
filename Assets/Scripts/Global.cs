using UnityEngine;
using UnityEngine.SceneManagement;

public class Global
{
    public static PlayerScript player;

    public static void Dead ()
    {
        if (PlayerPrefs.HasKey("best_score"))
        {
            if (player.GetScore() > PlayerPrefs.GetInt("best_score")) PlayerPrefs.SetInt("best_score", player.GetScore());
        }
        else
        {
            PlayerPrefs.SetInt("best_score", player.GetScore());
        }

        PlayerPrefs.SetInt("previous_score", player.GetScore());

        SceneManager.LoadScene(2);
    }
}