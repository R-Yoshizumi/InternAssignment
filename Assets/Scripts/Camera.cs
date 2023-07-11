using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using Object = UnityEngine.Object;

public class Camera : MonoBehaviour
{
    public RawImage RawImage;
    WebCamTexture webCam;
    private const string IMAGE_SAVE_FOLDER = "Image";

    void Start()
    {
        webCam = new WebCamTexture();
        RawImage.texture = webCam;
        webCam.requestedWidth = 900;
        webCam.requestedHeight = 1400;
        webCam.Play();
    }

    public void Shoot(){
        webCam.Pause();
    }

    public void Cancel(){
        webCam.Play();
    }

    public void Save(){
        Texture2D texture = new Texture2D(webCam.width, webCam.height, TextureFormat.ARGB32, false);
        texture.SetPixels(webCam.GetPixels());
        texture.Apply();

        //サイン読み込み
        string path=Application.persistentDataPath + "/" + IMAGE_SAVE_FOLDER + "/paint.png";
        byte[] bytes = File.ReadAllBytes(path);

        // エンコード
        byte[] bin = texture.EncodeToJPG();

        Pasting(bin,bytes);
        Object.Destroy(texture);

        // ファイルを保存
#if UNITY_ANDROID
        File.WriteAllBytes(Application.persistentDataPath + "/test.jpg", bin);
        Debug.Log(Application.persistentDataPath+"に保存");

#else
        File.WriteAllBytes(Application.dataPath + "/test.jpg", bin);
        Debug.Log(Application.dataPath+"に保存");
#endif
    }
    //サインの貼り付け関数（途中）
    public void Pasting(byte[] target,byte[] source){


    }

  
}
