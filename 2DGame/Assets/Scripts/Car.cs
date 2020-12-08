
using UnityEngine;

public class Car : MonoBehaviour
{
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

    private void Start()
    {
        print("哈囉，沃德");
        print(size);
        print(brand);

    }
}
