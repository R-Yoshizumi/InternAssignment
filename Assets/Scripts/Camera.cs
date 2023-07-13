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

    Texture2D texture;
    public void Save(){
        texture = new Texture2D(webCam.width, webCam.height, TextureFormat.ARGB32, false);
        Debug.Log("Width"+webCam.width+ "Height"+webCam.height);
        texture.SetPixels(webCam.GetPixels());
        texture.Apply();

        //サイン読み込み
        string path=Application.persistentDataPath + "/" + IMAGE_SAVE_FOLDER + "/resize.png";
        byte[] signByte = File.ReadAllBytes(path);

        // エンコード
        byte[] cameraBytes = texture.EncodeToPNG();
        Pasting(cameraBytes,signByte);

        byte[] pastedImage = texture.EncodeToPNG();

        Object.Destroy(texture);

        // ファイルを保存
        #if UNITY_ANDROID
        File.WriteAllBytes(Application.persistentDataPath + "/test.png", pastedImage);
        Debug.Log(Application.persistentDataPath+"に保存");

        #else
        File.WriteAllBytes(Application.dataPath + "/test.png", pastedImage);
        Debug.Log(Application.dataPath+"に保存");
        #endif
    }
    //サインの貼り付け関数
    public void Pasting(byte[] target,byte[] source){

        Texture2D loadTexture = new Texture2D(2,2);
        loadTexture.LoadImage(source);
        Debug.Log("loadTexture"+loadTexture.GetPixels().Length);

        
        var pixels = texture.GetPixels32();
        Debug.Log("pixcels.Length"+pixels.Length);

        int a=0;
        for(int i=0;i<loadTexture.GetPixels().Length;i++){            
            if(i%(int)Math.Sqrt((double)loadTexture.GetPixels().Length)==0&&i>0){
                a+=webCam.width-(int)Math.Sqrt((double)loadTexture.GetPixels().Length);
            }
            pixels[a]=loadTexture.GetPixels()[i];
            a++;      
        }
        texture.SetPixels32( pixels );


        Debug.Log("texture"+texture.GetPixels().Length);
        texture.Apply();

    }

  
}
