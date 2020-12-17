using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TetrisManager : MonoBehaviour
{
    #region 控制器欄位
    [Header("掉落速度"), Range(0.1f, 3)]
    public float downtime = 1.5f;
    [Header("目前成績")]
    public int score;
    [Header("最高成績")]
    public int Hscore;
    [Header("等級")]
    public int level = 1;
    [Header("結束畫面")]
    public GameObject endimage;
    [Header("音效:掉落、移動、清除、結束")]
    public AudioClip down;
    public AudioClip move;
    public AudioClip clear;
    public AudioClip end;
    [Header("下一個方塊")]
    public Transform nextGoArea;
    [Header("下一個方塊的編號")]
    public int nextIndex;
    [Header("畫布")]
    public Transform traCanvas;

    #endregion
    public void Start()
    {
        RandomTetris();

    }



    private void RandomTetris()
    {
        //整數不會等於最大值
        nextIndex = Random.Range(0, 7);
        //下一個俄羅斯方塊範圍，取得子物件(子物件編號)，轉為遊戲物件，啟動設定(顯示)
        nextGoArea.GetChild(nextIndex).gameObject.SetActive(true);
    }
    /// <summary>
    /// 開始遊戲
    /// 1.生成俄羅斯方塊放在正確位置
    /// 2.上一次俄羅斯方塊要隱藏
    /// 3.隨機取得下一個
    /// </summary>
    public void StarGame()
    {
        //保存上一次的方塊
        GameObject tetris = nextGoArea.GetChild(nextIndex).gameObject;
        //生成物件(生成物件，父物件)
        GameObject current = Instantiate(tetris, traCanvas);
        //GetComponent<任何元件>()
        //<>    指所有類型
        current.GetComponent<RectTransform>().anchoredPosition = new Vector2(-15,360);

        //上一個方塊隱藏
        tetris.SetActive(false);
        //隨機取得下一個方塊
        RandomTetris();

    }

    private void AddGo()
    {
        



    }

    public int ScoreA()
    {
        return score;
    }

    private void GameTime()
    {

    }

    private void End()
    {

    }

    public void Again()
    {

    }

    public void Exit()
    {

    }

}
