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
        
        byte[] bytes=image.sprite.texture.EncodeToPNG();

        
        resizedTexture = ResizeTexture(image.sprite.texture,50,50);
        byte[] resizeImage = resizedTexture.EncodeToPNG();


        File.WriteAllBytes(directoryPath+"resize.png", resizeImage);
        File.WriteAllBytes(path, bytes);
    }

    public Texture2D ResizeTexture(Texture2D src, int dst_w, int dst_h)
    {
	    Texture2D dst = new Texture2D(dst_w, dst_h, src.format, false);

	    float inv_w = 1f / dst_w;
	    float inv_h = 1f / dst_h;

	    for (int y = 0; y < dst_h; ++y)
		    for (int x = 0; x < dst_w; ++x)
			    dst.SetPixel(x, y, src.GetPixelBilinear((float)x * inv_w, (float)y * inv_h));

	    return dst;
    }

}
