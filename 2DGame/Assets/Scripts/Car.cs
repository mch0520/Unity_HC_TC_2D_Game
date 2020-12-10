
using UnityEngine;

public class Car : MonoBehaviour
{
    #region 汽車的欄位
    //欄位的屬性
    //標題 Header   字串
    //提示 Tooltip  字串
    //範圍 Range    數字
    [Header("這是車子的尺寸"), Range(50, 150)]
    public int size = 100;
    [Tooltip("這是車子的重量,單位是噸"),Range(0.5f,3.5f)]
    public float weight = 1.5f;
    [Header("品牌"),Tooltip("這是車子的品牌")]
    public string brand ="BMW";
    [Header("是否有天窗")]
    public bool hasWindow =true;

    //其他常用類型
    //Color
    public Color colorA=Color.red;              //使用內建顏色
    public Color colorb =new Color(0.6f,0,0.8f);   //自訂顏色 RBG
    public Color colorc =new Color(0, 1, 0, 0.6f); //自訂顏色 RBGA,A透明值

    //向量2-4
    public Vector2 v2A=Vector2.zero;
    public Vector2 v2B=Vector2.one;
    public Vector2 v2C=new Vector2(1.5f,6);

    public Vector3 V3A = new Vector3(1,6,1.5f);
    public Vector4 V4A = new Vector4(1,6,1.5f,8);

    //音效片段 AudioClip
    public AudioClip soundexplosion;
    //圖片 Sprite 2D圖片與介面圖片
    public Sprite spriteA;

    //遊戲物件與預制物
    public GameObject goa;
    public GameObject gob;

    //元件:面板上的粗體字
    public Transform tra;
    public Camera cam;
    #endregion

    #region 事件
    private void Start()
    {
        print("哈囉，沃德");
        //取得欄位
        print(size);
        print(brand);
        //設定欄位
        weight = 1.3f;
        hasWindow = false;


        //呼叫自訂方法
        //呼叫自訂方法:呼叫自訂方法名稱();
        methoadA();
        methoadB();

        //區域變數
        //變數 區域變數名稱 指定 值;
        //僅限此區域使用(大括號)
        int intA = methoadB();
        print("傳回整數"+intA);

        float pi = PI();
        print(pi);

        Vector3 v345 = V345();
        print(v345);

    }
    #endregion

    #region 方法
    //欄位語法:
    //修飾詞 類型 欄位名稱 指定 值;

    //方法語法
    //修飾詞 傳回類型 方法名稱 () {}
    //無傳回類型 void - 沒有傳回類型
    //自訂方法
    //*必須被呼叫才會被執行

    private void methoadA()
    {
        print("嗨~,我是方法A");
    }

    //如果不適無傳回,必須須是用關鍵字 return
    //而且必須在 return 後加上傳回類型
    private void methoadB()
    {
        return 123;
    }

    private float PI()
    {
        return 3.14f;
    }
    private Vector3 V345()
    {
        return new Vector3(3,4,5);
    }

    #endregion
}
