//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class TestScript : MonoBehaviour
//{
//    public Image pictureHolder;
//    // Start is called before the first frame update
//    void Start()
//    {
//        ShowPictureHolder(false);
//    }

//    private void ShowPictureHolder(bool visible)
//    {
//        pictureHolder.gameObject.SetActive(visible);
//    }

//    public void TakeScreenshot()
//    {
//        GleyShare.Manager.CaptureScreenshot(ScreenshotCaptured);
//    }

//    private void ScreenshotCaptured(Sprite sprite)
//    {
//        if (sprite != null)
//        {
//            pictureHolder.sprite = sprite;
//            ShowPictureHolder(true);
//        }
//    }

//    public void Cancel()
//    {
//        ShowPictureHolder(false);
//    }

//    public void Share()
//    {
//        GleyShare.Manager.SharePicture();
//    }
//}
