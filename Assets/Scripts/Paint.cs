using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing;

using Color = UnityEngine.Color;

public class Paint : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField, Range(1,20)] private int brushSize=20;
    private Texture2D signTexture = null;
    

    void Start()
    {
        //サインの書く場所にテクスチャを生成
        var rect = image.gameObject.GetComponent<RectTransform>().rect;
        signTexture=new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGBA32, false);
        image.sprite = Sprite.Create(signTexture, new Rect(0, 0, signTexture.width, signTexture.height), Vector2.zero);
        
    }
        
    
    private void Update()
    {
        var rect = image.gameObject.GetComponent<RectTransform>().rect;
        int brushMin;
        int brushMax;
        Vector2 mousePos=Input.mousePosition;
        mousePos.x-=image.transform.position.x-(rect.width/2);
        mousePos.y-=image.transform.position.y-(rect.height/2);
        
        //ブラシの大きさ変更
        if(brushSize/2==0){
            brushMin=1-(brushSize/2);
            brushMax=brushSize/2;
        }else{
            brushMin=-brushSize/2;
            brushMax=brushSize/2;
        }
        //サインできるエリアの指定
        if(Input.GetMouseButton(0)
            &&mousePos.x>0&&mousePos.x<rect.width
            &&mousePos.y>0&&mousePos.y<rect.height){
            Color black = new(0.0F, 0.0F, 0.0F,1.0F);
            //点の大きさ変更
            for(int i=brushMin; i<=brushMax;i++){
                for(int j=brushMin; j<=brushMax;j++){
                    signTexture.SetPixel((int)mousePos.x+i, (int)mousePos.y+j, black);
                }
            }
        }
            signTexture.Apply();
    }

}


