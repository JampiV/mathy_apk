using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCamara : MonoBehaviour
{
    public void MoviendoLaCamara(){
        ACalcular Temporal = GameObject.Find("ACalcular").GetComponent<ACalcular>();
        Temporal.AmoverCamara();

    }
}
