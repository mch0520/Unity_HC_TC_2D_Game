using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    #region 欄位
    [Header("角度為零,線條的長度")]
    public float length0;
    [Header("角度為九十度,線條的長度")]
    public float length90;
    [Header("旋轉位移左右")]
    public int offsetX;
    [Header("旋轉位移上下")]
    public int offsetY;
    [Header("每一顆小方塊的射線長度"), Range(0f, 2f)]
    public float smallLength=0.5f;

    /// <summary>
    /// 紀錄目前的角度
    /// </summary>
    public float length;
    public float lengthDown;
    #endregion

    #region 事件
    /// <summary>
    /// 繪製圖示
    /// </summary>
    private void OnDrawGizmos()
    {
        #region 判定牆壁與地板
        //圖示 的 顏色
        Gizmos.color = Color.red;
        //將浮點數角度 轉為 整數 -小數點
        int z = (int)transform.eulerAngles.z;

        //圖示 的 顏色
        Gizmos.color = Color.red;
        if (z == 0 || z == 180)
        {
            length = length0;
            //圖示 的 繪製射線(設限,,方向)
            Gizmos.DrawRay(transform.position, Vector3.right * length0);
        }
        else if (z == 90 || z == 270)
        {
            length = length90;
            //圖示 的 繪製射線(設限,,方向)
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * length90);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, -Vector3.right * length90);
            //繪製向下線條
            lengthDown = length0;
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position,-Vector3.up*length0);
        }
        #endregion

        #region 每一顆判定
        for (int i = 0; i < length; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.GetChild(i).position,Vector2.down*smallLength);
        }

        for (int i = 0; i < length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.right * smallLength);
        }

        for (int i = 0; i < length; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.left * smallLength);
        }
        #endregion

    }
    #endregion

    private void Start()
    {
        //紀錄遊戲開始的角度
        length = length0;
    }

    private void Update()
    {
        CheckWall();
        checkBottom();
    }


    /// <summary>
    /// 小方塊底部圖層
    /// </summary>
    public bool smallBottom;

    /// <summary>
    /// 檢查底部是否有其他方塊
    /// </summary>
    private void checkBottom()
    {
        //迴圈執行每一顆小方塊
        for (int i = 0; i < transform.childCount; i++)
        {
            //每一顆小方塊 射線(每一顆小方塊的中心點，向下，長度，圖層)
            RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(i).position,Vector3.down,smallLength,1<<10);

            if (hit && hit.collider.name == "方塊") smallBottom = true;
        }
    }

    #region 方法
    /// <summary>
    /// 是否碰撞到右邊牆壁
    /// </summary>
    public bool wallRight;

    /// <summary>
    /// 是否碰撞到左邊牆壁
    /// </summary>
    public bool wallLeft;

    /// <summary>
    /// 檢查射線是否碰撞到牆壁
    /// </summary>
    private void CheckWall()
    {
        //2D物理碰撞資訊 區域變數名稱=2D 物理.射線碰撞(起點,方向,距離,圖層)
        RaycastHit2D hit= Physics2D.Raycast(transform.position,Vector3.right,length,1<<8);
        
        //並且 &&
        //如果 碰撞到東西 並且 名稱 為 右邊牆壁
        if (hit&&hit.transform.name=="右邊牆壁")
        {
            wallRight = true;
        }
        else 
        {
            wallRight = false;
        }

        //並且 &&
        //如果 碰撞到東西 並且 名稱 為 左邊牆壁
        if (hit && hit.transform.name == "左邊牆壁")
        {
            wallLeft = true;
        }
        else
        {
            wallLeft = false;
        }
    }
    #endregion

}
