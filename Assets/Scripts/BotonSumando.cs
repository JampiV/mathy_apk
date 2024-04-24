using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonSumando : MonoBehaviour
{
    public ACalcular aCalcular;

    private void Start()
    {
        aCalcular = FindObjectOfType<ACalcular>();
        if (aCalcular == null)
        {
            Debug.LogError("No se encontr� el objeto ACalcular en la escena.");
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && aCalcular != null)
        {
            //Debug.Log("Llamando a Acalcular desde la suma");
            aCalcular.FuncionParaSumar();
        }
    }
}
