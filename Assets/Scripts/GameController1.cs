using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen2 : MonoBehaviour
{
    public Text pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + "  CORRECTAS";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("GAME");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
