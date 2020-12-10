using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TetrisManager : MonoBehaviour
{
    #region 控制器欄位
    [Header("掉落速度"),Range(0.1f,3)]
    public float downtime=1.5f;
    [Header("目前成績")]
    public int score;
    [Header("最高成績")]
    public int Hscore;
    [Header("等級")]
    public int level=1;
    [Header("結束畫面")]
    public GameObject endimage;
    [Header("音效:掉落、移動、清除、結束")]
    public AudioClip down;
    public AudioClip move;
    public AudioClip clear;
    public AudioClip end;
    #endregion

    private void AddGo()
    {
        return ;

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
