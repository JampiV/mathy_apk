using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenURL : MonoBehaviour
{
    // URLs para abrir en el navegador
    public string URL_video = "";
    public string URL_tutorial = "";

    // Referencias a los paneles de la interfaz de usuario
    public GameObject mainPanel;
    public GameObject optionsPanel;

    // Método para abrir un PDF online (video)
    public void abrirPdfOnline()
    {
        Application.OpenURL(URL_video);
    }

    // Método para abrir un tutorial online
    public void abrirTutorial()
    {
        Application.OpenURL(URL_tutorial);
    }

    // Método para abrir un panel específico
    public void OpenPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);

        panel.SetActive(true);
    }

    // Método para salir del juego
    public void ExitGame()
    {
        Application.Quit();
    }

    public void nave(string navegar)
    {
        SceneManager.LoadScene(navegar);
    }


    // Update se llama una vez por frame
    void Update()
    {
        
    }
}
