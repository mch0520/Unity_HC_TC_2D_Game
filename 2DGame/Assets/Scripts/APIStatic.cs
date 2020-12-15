using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

/// <summary>
/// 認識靜態的API
/// 靜態Static
/// </summary>
public class APIStatic : MonoBehaviour
{
    public int cameraCount;


    /// <summary>
    /// 事件開始後撥放一次
    /// </summary>
    void Start()
    {
        //靜態屬性
        //取得
        //語法:類別名稱.屬性名稱
        //print(Mathf.PI);
        //取得相機總數
        //print("攝影機總數:"+Camera.allCamerasCount);
        //隱藏鼠標
        //Cursor.visible = false; 
       // print("隨機範圍:"+Random.Range(1.0f,99.0f));

        //Application.OpenURL("https://docs.unity3d.com/ScriptReference/Application.html");
        //print("去小數點"+Mathf.Floor(10.65f));

    }




    void Update()
    {
        //取得遊戲經過
        //print(Time.time);

        //是否輸入任一鍵
        //if (Input.anyKey) print("已按下某一鍵");

        //if (Input.GetKeyDown("space"))    print("按下空白鍵");
        

    }
}
