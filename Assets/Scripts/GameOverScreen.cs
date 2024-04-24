using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public Texture TexturaSuma; // Asignar en el inspector de Unity
    public Texture TexturaResta; // Asignar en el inspector de Unity
    public string estadoActualDelJuego; // Esto deber� ser asignado seg�n el estado del juego, ya sea "suma" o "resta"

    public Timer timer; // Asignar en el inspector de Unity

    public ACalcular ACalcular;

    void Start()
    {
        // Suscribirse al evento OnTiempoAgotado del script Timer
        timer.OnTiempoAgotado += MostrarGameOver;
    }

    public void Setup(int respuestasCorrectas, string estadilloGaming)
    {
        //Debug.Log("Respuestas correctas recibidas en Setup: " + respuestasCorrectas);
        gameObject.SetActive(true);
        pointsText.text = respuestasCorrectas.ToString() + " CORRECTAS"; // Muestra las respuestas correctas
        estadoActualDelJuego = estadilloGaming;
        //Debug.Log("estado del juego que se recibe es :" + estadoActualDelJuego);
    }

    void MostrarGameOver()
    {
        // L�gica para mostrar la pantalla de Game Over
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        // L�gica para el bot�n de reiniciar
        Renderer rendererFondo = GameObject.Find("FondoOperaciones").GetComponent<Renderer>();
        //Debug.Log("El fondo a renderizar es " + rendererFondo);
        if (estadoActualDelJuego == "suma")
        {
            rendererFondo.material.mainTexture = TexturaSuma;
        }
        else if (estadoActualDelJuego == "resta")
        {
            rendererFondo.material.mainTexture = TexturaResta;
        }

        //Debug.Log("Actualizando contadores desde el Gameover");
        ACalcular.ActualizarContadores();

        //Debug.Log("Reinicializando el timer desde el Gameover");
        timer.IniciarTemporizador();

        gameObject.SetActive(false); // Desactivar la pantalla de Game Over
        // Aqu� tambi�n deber�as reiniciar el temporizador y cualquier otro estado del juego necesario
    }

    public void ExitButton()
    {
        // L�gica para el bot�n de salir
        SceneManager.LoadScene("MainMenu");
    }
    public void Navegador(string navegar)
    {
        SceneManager.LoadScene(navegar);
    }
}
