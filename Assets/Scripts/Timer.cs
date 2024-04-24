using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public delegate void TiempoAgotadoHandler();
    public event TiempoAgotadoHandler OnTiempoAgotado;

    public Slider timerSlider;
    public Text timerText;
    public float gameTime;

    private bool stopTimer;
    private float startTime;

    void Start()
    {
        IniciarTemporizador();
    }

    void Update()
    {
        if (!stopTimer)
        {
            float time = (startTime + gameTime) - Time.time;
            if (time <= 0)
            {
                DetenerTemporizador();
                return;
            }
            ActualizarTiempo(time);
        }
    }

    public void IniciarTemporizador()
    {
        stopTimer = false;
        startTime = Time.time;
        ActualizarTiempo(gameTime);
    }

    public void DetenerTemporizador()
    {
        stopTimer = true;
        ActualizarTiempo(0);
        OnTiempoAgotado?.Invoke();
    }

    private void ActualizarTiempo(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerSlider.value = time;
        timerSlider.maxValue = gameTime;
    }

    public void DisminuirTiempo(float cantidad)
    {
        if (!stopTimer)
        {
            startTime -= cantidad; // Disminuye el tiempo de inicio por la cantidad especificada
            float time = (startTime + gameTime) - Time.time;
            if (time < 0)
            {
                DetenerTemporizador();
            }
            else
            {
                ActualizarTiempo(time);
            }
        }
    }
}
