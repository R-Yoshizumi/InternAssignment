using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;




public class SaveSign : MonoBehaviour
{    
    [SerializeField] Image image;
    private const string IMAGE_SAVE_FOLDER = "Image";
    public void OnClick(){
        TextureToPng(SavePath(IMAGE_SAVE_FOLDER));
    }

    private string SavePath(string folderName){
        string directoryPath = Application.persistentDataPath + "/" + folderName + "/";

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            return directoryPath + "sign.png";
        }
        Debug.Log(directoryPath+"に保存完了");

        return directoryPath + "sign.png";
    } 

    private void TextureToPng(string path){
        string directoryPath = Application.persistentDataPath + "/Image/";

        var resizedTexture = new Texture2D(50, 50,TextureFormat.ARGB32, false);
        TextureScale.Point(image.sprite.texture, 50, 50);
        
        byte[] bytes=image.sprite.texture.EncodeToPNG();

        
        //Graphics.ConvertTexture(image.sprite.texture, resizedTexture);
        byte[] resizeImage = resizedTexture.EncodeToPNG();

        Debug.Log("bytes"+bytes.Length);
        File.WriteAllBytes(directoryPath+"resize.png", resizeImage);
        File.WriteAllBytes(path, bytes);
    }

}
