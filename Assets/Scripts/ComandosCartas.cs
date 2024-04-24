using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ComandosCartas : MonoBehaviour
{
    public void Navegar3(string navegar)
    {
        SceneManager.LoadScene(navegar);
    }
}
