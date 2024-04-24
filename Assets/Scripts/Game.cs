using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Asegúrate de incluir esta librería para usar el tipo Text
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Text recordTimeText; // Referencia al Text de UI para mostrar el tiempo récord
    public Text recordCorrectAnswersText; // Referencia al Text de UI para mostrar las respuestas correctas récord

    void Start()
    {
        // Recuperar y mostrar el récord de tiempo
        float recordTime = PlayerPrefs.GetFloat("RecordTime", float.MaxValue);
        recordTimeText.text = recordTime != float.MaxValue ? FormatTime(recordTime) : "N/A";

        // Recuperar y mostrar el récord de respuestas correctas
        int recordCorrectAnswers = PlayerPrefs.GetInt("RecordScore", 0);
        recordCorrectAnswersText.text = recordCorrectAnswers > 0 ? recordCorrectAnswers.ToString() : "N/A";
    }

    public void Game1(string game)
    {
        SceneManager.LoadScene(game);
    }

    public void Examen1(string examen)
    {
        SceneManager.LoadScene(examen);
    }

    string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}