using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.Collections;
using UnityEngine.SceneManagement;

public class Question : MonoBehaviour
{
    private Stopwatch stopwatch = new Stopwatch();
    private int totalQuestions = 20;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private int incorrectAnswers = 0;
    private int score = 0;
    private bool correctAnswer;
    private float startTime;
    private bool gameActive;
    private int currentLevel = 1;

    public Button trueButton;
    public Button falseButton;
    public Text questionText;
    public Text levelText;
    public Text progressText;
    public Text timeText;
    public Text errorRatioText;
    public Text successRatioText;
    public Text scoreText;
    public Text recordTimeText; // Nuevo
    public Text recordCorrectAnswersText; // Nuevo
    public Text dateText;

    void Start()
    {
        trueButton.onClick.AddListener(() => CheckAnswer(true));
        falseButton.onClick.AddListener(() => CheckAnswer(false));
        startTime = Time.time;
        gameActive = true;
        // Muestra la fecha actual.
        dateText.text = "" + System.DateTime.Now.ToString("dd/MM/yyyy");
        StartCoroutine(UpdateTimer());
        stopwatch.Start();
        GenerateQuestion();
        ShowRecords(); // Nuevo
        StartCoroutine(UpdateDate());
    }

    IEnumerator UpdateTimer()
    {
        while (gameActive)
        {
            float timeElapsed = Time.time - startTime;
            timeText.text = " " + FormatTime(timeElapsed);
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator UpdateDate()
    {
        while (gameActive)
        {
            dateText.text = "" + System.DateTime.Now.ToString("dd/MM/yyyy");
            // Espera hasta la medianoche para actualizar la fecha
            yield return new WaitForSeconds((float)((24 - System.DateTime.Now.Hour) * 3600 - System.DateTime.Now.Minute * 60 - System.DateTime.Now.Second));
        }
    }

    string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void GenerateQuestion()
    {
        if (currentQuestionIndex >= totalQuestions)
        {
            GameOver(true);
            return;
        }

        DetermineLevel();
        int number1, number2, displayedResult;
        bool isSubtraction = UnityEngine.Random.Range(0, 2) == 0;
        GenerateNumbers(out number1, out number2, isSubtraction);

        int correctResult = isSubtraction ? number1 - number2 : number1 + number2;
        correctAnswer = UnityEngine.Random.value > 0.5f;
        displayedResult = correctAnswer ? correctResult : AdjustResult(correctResult, isSubtraction);

        questionText.text = $"{number1} {(isSubtraction ? "-" : "+")} {number2} = {displayedResult}";
        UpdateLevelText();
        UpdateProgressText();
        currentQuestionIndex++;
    }

    void DetermineLevel()
    {
        if (currentQuestionIndex < 5) currentLevel = 1;
        else if (currentQuestionIndex < 15) currentLevel = 2;
        else currentLevel = 3;
    }

    void GenerateNumbers(out int num1, out int num2, bool isSubtraction)
    {
        if (currentLevel == 1)
        {
            num1 = UnityEngine.Random.Range(1, 10);
            num2 = UnityEngine.Random.Range(1, 10);
        }
        else if (currentLevel == 2)
        {
            num1 = UnityEngine.Random.Range(10, 100);
            num2 = UnityEngine.Random.Range(10, 100);
        }
        else
        {
            num1 = UnityEngine.Random.Range(100, 1000);
            num2 = UnityEngine.Random.Range(100, 1000);
        }

        if (isSubtraction && num1 < num2)
        {
            (num1, num2) = (num2, num1);
        }
    }

    int AdjustResult(int correctResult, bool isSubtraction)
    {
        return correctResult + (isSubtraction ? -UnityEngine.Random.Range(1, 10) : UnityEngine.Random.Range(1, 10));
    }

    void CheckAnswer(bool playerChoice)
    {
        if (playerChoice == correctAnswer)
        {
            correctAnswers++;
            score += 1;
        }
        else
        {
            incorrectAnswers++;
        }

        UpdateStats();

        if (currentQuestionIndex >= totalQuestions)
        {
            GameOver(playerChoice == correctAnswer);
        }
        else
        {
            GenerateQuestion();
        }
    }

    void UpdateStats()
    {
        errorRatioText.text = " " + ((float)incorrectAnswers / currentQuestionIndex).ToString("P2");
        successRatioText.text = " " + ((float)correctAnswers / currentQuestionIndex).ToString("P2");
        scoreText.text = " " + score;
    }

    void UpdateLevelText()
    {
        switch (currentLevel)
        {
            case 1:
                levelText.text = "Nivel: Básico";
                break;
            case 2:
                levelText.text = "Nivel: Intermedio";
                break;
            case 3:
                levelText.text = "Nivel: Avanzado";
                break;
        }
    }

    void UpdateProgressText()
    {
        progressText.text = "Pregunta: " + (currentQuestionIndex + 1) + " / " + totalQuestions;
    }

    void GameOver(bool won)
    {
        gameActive = false;
        float finalTime = Time.time - startTime;
        timeText.text = " " + FormatTime(finalTime);

        if (won)
        {
            float recordTime = PlayerPrefs.GetFloat("RecordTime", float.MaxValue);
            if (finalTime < recordTime)
            {
                PlayerPrefs.SetFloat("RecordTime", finalTime);
                recordTimeText.text = "" + FormatTime(finalTime);
            }

            int recordScore = PlayerPrefs.GetInt("RecordScore", 0);
            if (score > recordScore)
            {
                PlayerPrefs.SetInt("RecordScore", score);
                recordCorrectAnswersText.text = "" + score;
            }

            PlayerPrefs.Save();
        }

        // Actualiza la interfaz de usuario para reflejar el final del juego
        questionText.text = won ? "¡Finalizado!" : "Finalizado";
        trueButton.gameObject.SetActive(false);
        falseButton.gameObject.SetActive(false);

        // Actualiza las estadísticas por última vez
        UpdateStats();
    }

    void ShowRecords()
    {
        float recordTime = PlayerPrefs.GetFloat("RecordTime", float.MaxValue);
        recordTimeText.text = recordTime != float.MaxValue ? " " + FormatTime(recordTime) : "N/A";

        int recordScore = PlayerPrefs.GetInt("RecordScore", 0);
        recordCorrectAnswersText.text = recordScore > 0 ? " " + recordScore : "N/A";
    }

    public void Navegar1(string navegar)
    {
        SceneManager.LoadScene(navegar);
    }

    public void RestartGame()
    {
        // Carga la escena actual nuevamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
