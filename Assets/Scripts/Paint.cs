using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paint : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField, Range(1,20)] private int brushSize=10;

    int brush_min;
    int brush_max;
    private int test = 1;


    private Texture2D texture = null;
    void Start()
    {
        var rect = image.gameObject.GetComponent<RectTransform>().rect;
        //texture=new Texture2D((int)image.rectTransform.rect.width, (int)image.rectTransform.rect.height, TextureFormat.RGBA32, false);
        texture=new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGBA32, false);
        image.texture = texture;

    }
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("クリックした。");
            //Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var pos = Input.mousePosition;
            
            int brush_min;
            int brush_max;    
            //ブラシの大きさ変更
            if(brushSize/2==0){
                brush_min=1-brushSize/2;
                brush_max=brushSize/2;
            }else{
                brush_min=-brushSize/2;
                brush_max=brushSize/2;
            }

            for(int i=brush_min; i<=brush_max;i++){
                for(int j=brush_min; j<=brush_max;j++){
                    texture.SetPixel((int)pos.x+i, (int)pos.y-480+j, Color.red);
                    //texture.SetPixel((int)mouse.x+i, (int)mouse.y+j, Color.red);
                }
            }

            texture.Apply();
        }
    }

}
