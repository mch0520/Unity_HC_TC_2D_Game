using System.Collections; //引用 系統.集合 API -偕同程序
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //引用介面 API
using UnityEngine.SceneManagement;
using System.Linq;      //查詢語言


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
    public AudioClip soundDown;
    public AudioClip soundMove;
    public AudioClip soundClear;
    public AudioClip soundRotate;
    public AudioClip soundEnd;
    [Header("下一個俄羅斯方塊區域")]
    public Transform nextGoArea;
    [Header("生成俄羅斯方塊的父物件")]
    public Transform traTetrisParent;

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
    private Vector2[] posSpawn =
        {
        new Vector2(52,360),
        new Vector2(52,405),
        new Vector2(52,345),
        new Vector2(52,375),
        new Vector2(37,360),
        new Vector2(52,375),
        };

    [Header("目前分數")]
    public Text textCurrent;
    [Header("最高分數")]
    public Text textHeight;


    /// <summary>
    /// 是否遊戲結束
    /// </summary>
    private bool gameover;

    #endregion

    private AudioSource aud;

    public void Start()
    {
        RandomTetris();
        aud = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 更新事件:一秒執行約60次
    /// </summary>
    private void Update()
    {
        if (gameover) return;            //如果 遊戲結束 跳出

        ControlTetis();
        FastDowm();
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
                currentTetris.anchoredPosition -= new Vector2(0, 33);
            }



            #region 控制俄羅斯方塊移動、旋轉、加速掉落
            //取得 目前俄羅斯方塊的腳本
            Tetris tetris = currentTetris.GetComponent<Tetris>();

            //如果x 座標小於280才能往右移動
            //if (currentTetris.anchoredPosition.x<400)
            //如果目前俄羅斯方塊 沒有 碰到右邊牆壁
            if (!tetris.wallRight && !tetris.smallRight)
            {
                //KeyCode 列舉(下拉式選單)
                //或者        ||
                //按下D 或 右方向鍵 往右50
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    aud.PlayOneShot(soundMove, Random.Range(0.8f, 1.2f));
                    currentTetris.anchoredPosition += new Vector2(30, 0);
                }
            }

            //如果x 座標小於280才能往左移動
            if (!tetris.wallLeft && !tetris.smallLeft)
            {
                //按下A 或 左方向鍵 往左50
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    aud.PlayOneShot(soundMove, Random.Range(0.8f, 1.2f));
                    currentTetris.anchoredPosition -= new Vector2(30, 0);
                }
            }

            //按下 W或下方向鍵 逆時針旋轉90度
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                aud.PlayOneShot(soundRotate, Random.Range(0.8f, 1.2f));
                //屬性面板上的 Rotation 必須用eulerAngles 控制
                currentTetris.eulerAngles += new Vector3(0, 0, 90);
            }


            if (!fastDown)              //如果 沒有在快速落下 才 加速
            {
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
            }

            #endregion

            if (tetris.wallDown || tetris.smallBottom)
            {
                SetGround();                    //設定為地板
                CheckTetris();                  //檢查並開始消除判定
                StartGame();                    //生成下一顆
                StartCoroutine(ShakeEffect());  //晃動效果
            }


        }

    }

    /// <summary>
    /// 設定掉落後變為方塊
    /// </summary>
    private void SetGround()
    {
        aud.PlayOneShot(soundDown, Random.Range(0.8f, 1.2f));


        int count = currentTetris.childCount;                   //取得 目前 方塊 的 子物件總數

        for (int i = 0; i < count; i++)                         //迴圈 執行 子物件數量的次數
        {
            currentTetris.GetChild(i).name = "方塊";            //名稱改為方塊
            currentTetris.GetChild(i).gameObject.layer = 10;     //圖層改為方塊
        }

        //將俄羅斯小方塊搬到 分數區域
        for (int i = 0; i < count; i++)
        {
            currentTetris.GetChild(0).SetParent(traScoreArea);
        }
        Destroy(currentTetris.gameObject);

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
        fastDown = false;               //碰到地板後，沒有快速落下

        //1.生成俄羅斯方塊要放在正確位置
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

    #region 協同程序
    private IEnumerator ShakeEffect()
    {

        //取得震動效果物件的 Rect
        RectTransform rect = traTetrisParent.GetComponent<RectTransform>();

        //晃動 向上 30>0>20>0
        //等待 0.05
        float interval = 0.05f;

        rect.anchoredPosition += Vector2.up * 30;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition += Vector2.up * 30;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition += Vector2.up * 30;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition += Vector2.up * 30;
        yield return new WaitForSeconds(interval);
    }
    #endregion

    /// <summary>
    /// 是否快速落下
    /// </summary>
    private bool fastDown;

    /// <summary>
    /// 快速掉落功能
    /// </summary>
    private void FastDowm()
    {
        if (currentTetris && !fastDown)                                                            //如果 有 目前方塊 並且 還沒快速落下
        {
            if (Input.GetKeyDown(KeyCode.Space))                                     //如果 按下 空白鍵
            {
                aud.PlayOneShot(soundDown, Random.Range(0.8f, 1.2f));
                fastDown = true;                                                      //正在快速落下
                timeFall = 0.018f;                                                    //掉落時間
                StartCoroutine(ShakeEffect());                                        //啟動協同程序(協同方法());
            }
        }
    }

    [Header("分數判定區域")]
    public Transform traScoreArea;

    public RectTransform[] rectSmall;

    /// <summary>
    /// 檢查方塊是否連線
    /// </summary>
    private void CheckTetris()
    {
        rectSmall = new RectTransform[traScoreArea.childCount];

        for (int i = 0; i < traScoreArea.childCount; i++)
        {
            rectSmall[i] = traScoreArea.GetChild(i).GetComponent<RectTransform>();
            float y = rectSmall[i].anchoredPosition.y;
            if (y >= 400 - 10 && y <= +10) GameOver();
        }



        //檢查有幾棵位置在-300
        var small = rectSmall.Where(x => x.anchoredPosition.y == -309);
        print(small.ToArray().Length);

        //一行消除有幾棵
        if (small.ToArray().Length == 16)
        {
            aud.PlayOneShot(soundMove, Random.Range(0.8f, 1.2f));
            yield return StartCoroutine(Shine(small.ToArray()));        //等待 開始閃爍
            destroyRow[i] = true;                                       //紀錄要刪除的
            AddScore(500);
        }


        downHeight = new float[traScoreArea.ChildCount];

        for (int i = 0; i < downHeighy.Length; int++) downHeight[i] = 0;

        //計算每顆剩下方塊要掉落的高度
        for (int i = 0; i < destroyRow.Length; i++)
        {
            if (!destroyRow[i]) continue;

            for (int j = 0; j < rectSmall.Length; j++)
            {
                if (rectSmall[j].anchorePosition.y > -300 + 50 * i - 10) ;
        }
        }
    }

    /// <summary>
    /// 閃爍效果，閃爍後刪除
    /// </summary>
    /// <param name="smalls"></param>
    /// <returns></returns>
    private IEnumerator Shine(RectTransform[] smalls)
    {
        float interval = 0.005f;
        for (int i = 0; i < 16; i++) smalls[i].GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(interval);
        for (int i = 0; i < 16; i++) smalls[i].GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(interval);
        for (int i = 0; i < 16; i++) smalls[i].GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(interval);
        for (int i = 0; i < 16; i++) smalls[i].GetComponent<Image>().enabled = true;


    }

    /// <summary>
    /// 添加分數
    /// </summary>
    /// <param name="add"></param>
    public void AddScore(int add)
    {

    }

    /// <summary>
    /// 遊戲結束
    /// </summary>
    private void GameOver()
    {
        if (!gameover)
        {
            gameover = true;            //遊戲結束
            StopAllCoroutines();        //停止所有協程
            endimage.SetActive(true);   //顯示結束畫面

            textCurrent.text = "目前分數" + score;


            //如果 分數 > 本機端的 最高分數
            if (score > PlayerPrefs.GetInt("最高分數"))
            {
                //更新 本機端紀錄的最高分數 與介面
                PlayerPrefs.SetInt("最高分數", score);
                textHeight.text = "最高分數" + score;
            }
            //否則 更新最高分數為 本機端紀錄的 最高分數
            else textHeight.text = "最高分數" + PlayerPrefs.GetInt("最高分數");
        }
    }

    /// <summary>
    /// 重新遊戲
    /// </summary>
    public void ReplayGame()
    {
        SceneManager.LoadScene("遊戲場景");
    }

    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
