using System;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// シーン・画面遷移処理を行う
/// </summary>
public class ChangeScene : MonoBehaviour
{
    private string sceneName;   // 遷移先のシーン名
    [SerializeField] private GameObject currentScreen;    // 現在のキャンバス
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    /// <summary>
    /// 画面切り替え 
    /// </summary>
    /// 引数として変更したい画面をアタッチする
    public void ScreenChange(GameObject changeScreen)   
    {
        if(currentScreen != null && changeScreen != null)
        {
            Debug.Log("現在の画面:" + currentScreen + "変更先の画面:" + changeScreen);
            currentScreen.SetActive(false);
            changeScreen.SetActive(true);
            currentScreen = changeScreen;
        }
    }

    /// <summary>
    /// シーン移動処理
    /// </summary>
    /// <param name="sceneName"></param>
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
