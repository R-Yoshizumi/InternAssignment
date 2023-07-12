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
    [SerializeField, Range(1,20)] private int brushSize=5;

    int brushMin;
    int brushMax;




    private Texture2D signTexture = null;
    void Start()
    {
        var rect = image.gameObject.GetComponent<RectTransform>().rect;
        signTexture=new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGBA32, false);
        image.sprite = Sprite.Create(signTexture, new Rect(0, 0, signTexture.width, signTexture.height), Vector2.zero);
        //image.sprite = signTexture;

    }
    
    Vector2 beforePos;
    Vector2 afterPos;

    
    
    private void Update()
    {

        //ブラシの大きさ変更
        if(brushSize/2==0){
            brushMin=1-(brushSize/2);
            brushMax=brushSize/2;
        }else{
            brushMin=-brushSize/2;
            brushMax=brushSize/2;
        }

        if(Input.GetMouseButtonDown(0)
            &&Input.mousePosition.x>110&&Input.mousePosition.x<940
            &&Input.mousePosition.y<1390&&Input.mousePosition.y>550)
        {
            beforePos = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0)
            &&Input.mousePosition.x>110&&Input.mousePosition.x<940
            &&Input.mousePosition.y<1390&&Input.mousePosition.y>550)
        {
            afterPos = Input.mousePosition;
            //座標がずれていたので一時的に調整
            afterPos.x-=100;
            afterPos.y+=300;
            for(int i=brushMin; i<=brushMax;i++){
                for(int j=brushMin; j<=brushMax;j++){
                    signTexture.SetPixel((int)afterPos.x+i, (int)afterPos.y+j, Color.red);
                }
            }
        }
            signTexture.Apply();
    }

    //線で描画して隙間をなくすための関数（途中）
    public void DrawLine(Vector3 afterPoint,Vector3 beforePoint){
       
        Debug.Log("beforePosition"+beforePos);
        Debug.Log("afterPosition"+afterPos);
                
    }
}


