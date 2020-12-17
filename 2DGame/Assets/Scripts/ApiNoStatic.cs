using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiNoStatic : MonoBehaviour
{
    //使用一班屬性
    //類型 欄位名稱
    public Transform tran1;
    public Transform tran2;

    public GameObject myGO;

    public Transform tra;

    public void start()
    {
        //一般屬性
        //取得
        //語法:
        //類型的欄位名稱 的 一班屬性
        print("物件1的座標"+tran1);
        //設定
        //語法:
        //類型欄位名稱 的 一班屬性 = 對應的值
        tran2.position = new Vector3(2,6,7);

        print("我的遊戲物件圖曾為:"+myGO.layer);

        myGO.layer = 4;

        //一般方法
        //使用
        //語法:
        //類型欄位名稱 的 方法(對應的參數)
        tra.Rotate(0,0,5);
        tra.Translate(0,2,0);

    }







}
