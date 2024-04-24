using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ComandosPuzzle : MonoBehaviour
{

    public void Navegar1(string navegar)
    {
        SceneManager.LoadScene(navegar);
    }
}
