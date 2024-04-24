using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ShareOnSocialMedia : MonoBehaviour
{
    [SerializeField] GameObject Panel_share;
    [SerializeField] Text txtPanelScore;
    [SerializeField] Text txtHomeScore;
    [SerializeField] Text txtDate;



    public void ShareScore()
    {
        Debug.Log("Se ha llamado a la función ShareScore");
        StartCoroutine("TakeScreenShotAndShare");
    }

    IEnumerator TakeScreenShotAndShare()
    {
        Debug.Log("Se ha llamado a la corutina TakeScreenShotAndShare");
        yield return new WaitForEndOfFrame();

        Texture2D tx = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tx.Apply();

        string path = Path.Combine(Application.temporaryCachePath, "sharedImage.png");//image name
        File.WriteAllBytes(path, tx.EncodeToPNG());

        Destroy(tx); //to avoid memory leaks

        new NativeShare()
            .AddFile(path)
            .SetSubject("This is my score")
            .SetText("share your score with your friends")
            .Share();

        Panel_share.SetActive(true); //hide the panel
    }
}