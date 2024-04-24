using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameContro : MonoBehaviour
{
    public GameOverScreen GameOverScreen; // Referencia a tu pantalla de Game Over
    public Timer timer; // Referencia a tu script de Timer, aseg�rate de asignarlo en el inspector

    int maxPlatform = 0;

    void Start()
    {
        // Suscribirse al evento OnTiempoAgotado del temporizador para llamar al m�todo GameOver cuando el tiempo se agote
        timer.OnTiempoAgotado += GameOver;
    }

    public void GameOver()
    {
        // Aqu� se mostrar� la pantalla de Game Over
         //Debug.Log("aquí del gameoverscreen le voy a jalar el estado, que es:" + GameOverScreen.estadoActualDelJuego);
        GameOverScreen.Setup(maxPlatform, GameOverScreen.estadoActualDelJuego);
    }
}
