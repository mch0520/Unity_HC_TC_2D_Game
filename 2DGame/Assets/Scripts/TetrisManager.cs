using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TetrisManager : MonoBehaviour
{
    #region 控制器欄位
    [Header("掉落速度"), Range(0.1f, 3)]
    public float timeFall = 1.5f;
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

    /// <summary>
    /// 下一個方塊的編號
    /// </summary>
    private int nextIndex;
    [Header("畫布")]
    public Transform traCanvas;
    /// <summary>
    /// 目前俄羅斯方塊
    /// </summary>
    public RectTransform currentTetris;
    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;

    [Header("生成的起始位置")]
    public Vector2[] posSpawn =
        {
        new Vector2(70,360),
        new Vector2(70,405),
        new Vector2(70,345),
        new Vector2(70,375),
        new Vector2(55,360),
        new Vector2(70,375),
        };

    #endregion
    public void Start()
    {
        RandomTetris();

    }

    /// <summary>
    /// 更新事件:一秒執行約60次
    /// </summary>
    private void Update()
    {
        ControlTetis();
    }

    /// <summary>
    /// 控制俄羅斯方塊
    /// </summary>
    private void ControlTetis()
    {
        //如果 已經有 目前的俄羅斯方塊 
        if (currentTetris)
        {
            timer += Time.deltaTime;        //計時器累加一禎的時間
            if (timer >= timeFall)
            {
                timer = 0;
                currentTetris.anchoredPosition -= new Vector2(0, 34);
            }


            #region 控制俄羅斯方塊移動、旋轉、加速掉落
            //取得 目前俄羅斯方塊的腳本
            Tetris tetris = currentTetris.GetComponent<Tetris>();

            //如果x 座標小於280才能往右移動
            //if (currentTetris.anchoredPosition.x<400)
            //如果目前俄羅斯方塊 沒有 碰到右邊牆壁
            if (!tetris.wallRight)
            {
                //KeyCode 列舉(下拉式選單)
                //或者        ||
                //按下D 或 右方向鍵 往右50
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currentTetris.anchoredPosition += new Vector2(30, 0);
                }
            }

            //如果x 座標小於280才能往左移動
            if (currentTetris.anchoredPosition.x > -290)
            {
                //按下A 或 左方向鍵 往左50
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currentTetris.anchoredPosition -= new Vector2(30, 0);
                }
            }

            //按下 W或下方向鍵 逆時針旋轉90度
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                //屬性面板上的 Rotation 必須用eulerAngles 控制
                currentTetris.eulerAngles += new Vector3(0, 0, 90);
            }
            //按住S或下方向鍵 加快掉落速度
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                timeFall = 0.2f;
            }
            //否則恢復速度
            else
            {
                timeFall = 1.5f;
            }
            #endregion

            if (currentTetris.anchoredPosition.y == -340)
            {
                StartGame();
            }

        }

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
    public void StartGame()
    {
        //保存上一次的方塊
        GameObject tetris = nextGoArea.GetChild(nextIndex).gameObject;
        //生成物件(生成物件，父物件)
        GameObject current = Instantiate(tetris, traCanvas);
        //GetComponent<任何元件>()
        //<>    指所有類型
        //目前的俄羅斯方塊 - 取得元件<界面變形>()   座標=生成座標陣列[編號]
        current.GetComponent<RectTransform>().anchoredPosition = posSpawn[nextIndex];

        //上一個方塊隱藏
        tetris.SetActive(false);
        //隨機取得下一個方塊
        RandomTetris();

        //將生成的俄羅斯方塊 RectTransform 元件儲存
        currentTetris = current.GetComponent<RectTransform>();

    }

    private void AddGo()
    {




    }

    public int ScoreADD()
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
