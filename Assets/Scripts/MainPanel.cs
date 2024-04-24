using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanel : MonoBehaviour
{
    [Header("Options")]
    public Slider volumenFX;
    public Slider volumenMaster;
    public Toggle mute;
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip Clicksound;
    private float lastVolumen;
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject levelselectPanel;

    private void Start()
    {
        volumenFX.onValueChanged.AddListener(ChangeVolumenFX);
        volumenMaster.onValueChanged.AddListener(ChangeVolumenMaster);
    }

    public void Navegador (string navegar)
    {
        SceneManager.LoadScene(navegar);
    }



    public void ExitGame() {
        Application.Quit();
    }

    public void game1(string game)
    {
        SceneManager.LoadScene(game);
    }



    public void SetMute()
    {
        if (mute.isOn)
        {
            mixer.GetFloat("VolMaster", out lastVolumen);
            mixer.SetFloat("VolMaster", -80);
            mixer.SetFloat("volFX", -80); // Añadir esta línea para silenciar los efectos de sonido.
        }
        else
        {
            mixer.SetFloat("VolMaster", lastVolumen);
            mixer.SetFloat("volFX", 0); // Restaurar el volumen de los efectos de sonido.
        }
    }

    public void OpenPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        levelselectPanel.SetActive(false);

        panel.SetActive(true);
        PlaySoundButton();
    }

    public void ChangeVolumenMaster(float v)
    {
        mixer.SetFloat("VolMaster", v);
    }

    public void ChangeVolumenFX(float v)
    {
        mixer.SetFloat("volFX", v);
    }

    public void PlaySoundButton()
    {
        if (Clicksound != null)
        {
            fxSource.PlayOneShot(Clicksound);
        }
    }
}
