using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    [Header("角度為零,線條的長度")]
    public float length0;
    [Header("角度為九十度,線條的長度")]
    public float length90;
    /// <summary>
    /// 紀錄目前的角度
    /// </summary>
    public float length;


    /// <summary>
    /// 繪製圖示
    /// </summary>
    private void OnDrawGizmos()
    {
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
            Gizmos.DrawRay(transform.position, Vector3.right * length90);
        }
    }

    private void Start()
    {
        //紀錄遊戲開始的角度
        length = length0;
    }

    private void Update()
    {
        CheckWall();
    }
    /// <summary>
    /// 是否碰撞到右邊牆壁
    /// </summary>
    public bool wallRight;

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
    }


}
