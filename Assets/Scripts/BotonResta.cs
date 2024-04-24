using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonResta : MonoBehaviour
{
    public ACalcular aCalcular;

    private void Start()
    {
        aCalcular = FindObjectOfType<ACalcular>();
        if (aCalcular == null)
        {
            Debug.LogError("No se encontró el objeto ACalcular en la escena.");
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && aCalcular != null)
        {
            aCalcular.FuncionParaRestar();
        }
    }
}
