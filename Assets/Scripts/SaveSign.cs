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
            return directoryPath + "paint.png";
        }
        Debug.Log(directoryPath+"に保存完了");

        return directoryPath + "sign.png";
    } 

    private void TextureToPng(string path){
        byte[] bytes=image.sprite.texture.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
    }

}
