using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;




public class SaveSign : MonoBehaviour
{    
    [SerializeField] Image image;
    public int resizing_value=50;
    private const string IMAGE_SAVE_FOLDER = "Image";
    

    public void OnClick(){
        TextureToPng(SavePath(IMAGE_SAVE_FOLDER));
    }
    
    private string SavePath(string folderName){
        string directoryPath = Application.persistentDataPath + "/" + folderName + "/";
        //ディレクトリがなかったら作成
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            return directoryPath;
        }
        Debug.Log(directoryPath+"に保存完了");
        return directoryPath;
    } 
    private void TextureToPng(string path){
        //縮小サイズのテクスチャを生成
        var resizedTexture = new Texture2D(resizing_value, resizing_value,TextureFormat.ARGB32, false);
        resizedTexture = ResizeTexture(image.sprite.texture,resizing_value,resizing_value);
        //エンコード
        byte[] signPng=image.sprite.texture.EncodeToPNG();
        byte[] resizePng = resizedTexture.EncodeToPNG();

        //保存
        File.WriteAllBytes(path+"resize.png", resizePng);
        File.WriteAllBytes(path+"sign.png", signPng);
    }

    public Texture2D ResizeTexture(Texture2D texture, int width, int height)
    {
	    Texture2D dst = new Texture2D(width, height, texture.format, false);

	    float dst_w = 1f / width;
	    float dst_h = 1f / height;

	    for (int y = 0; y < height; ++y)
		    for (int x = 0; x < width; ++x)
                //元画像を等間隔ずつセットしていく
			    dst.SetPixel(x, y, texture.GetPixelBilinear((float)x * dst_w, (float)y * dst_h));

	    return dst;
    }

}
