using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// 延遲呼叫,等待音效撥放完畢
    /// </summary>
    public void DelayStartGame()
    {
        //延遲呼叫("方法名稱,秒數")
        //Invoke();
        Invoke("Begin",0.9f);
    }


    public void DelayQuitGame()
    {
        Invoke("Quit", 0.9f);
    }

    private void Begin()
    {
        //載入指定場景
        //場景管理器 的 載入場景("場景名稱")
        //場景管理器 的 載入場景(1)
        SceneManager.LoadScene("遊戲場景");
    }

    private void Quit()
    {
        //退出遊戲
        //應用程式 的 離開應用程式
        Application.Quit();
    }



}
