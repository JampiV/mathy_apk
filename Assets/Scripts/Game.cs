using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Aseg�rate de incluir esta librer�a para usar el tipo Text
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Text recordTimeText; // Referencia al Text de UI para mostrar el tiempo r�cord
    public Text recordCorrectAnswersText; // Referencia al Text de UI para mostrar las respuestas correctas r�cord

    void Start()
    {
        // Recuperar y mostrar el r�cord de tiempo
        float recordTime = PlayerPrefs.GetFloat("RecordTime", float.MaxValue);
        recordTimeText.text = recordTime != float.MaxValue ? FormatTime(recordTime) : "N/A";

        // Recuperar y mostrar el r�cord de respuestas correctas
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