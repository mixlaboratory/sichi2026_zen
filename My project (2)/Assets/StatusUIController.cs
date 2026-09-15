using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StatusUIController : MonoBehaviour
{
    public TMP_Text statusText;
    public Image statusPanel;

    public Color focusColor = Color.green;
    public Color dangerColor = Color.yellow;
    public Color outColor = Color.red;

    // MyBrainAppのresult.txt
   public string resultFilePath =
    @"C:\Users\manam\OneDrive\デスクトップ\MixLab\SICHI\2026\neu_ExBrainSdk_dotnet_v3.1.0\samples\MyBrainApp\bin\Debug\net8.0\output\result.txt";

    public static int dangerCount = 0;
    public static float focusTime = 0f;

    private int currentState = 0;
    private bool isFinished = false;

    void Start()
    {
        dangerCount = 0;
        focusTime = 0f;

        SetFocus();

        Debug.Log("読み込むファイル：" + resultFilePath);
        Debug.Log("ファイル存在：" + File.Exists(resultFilePath));

        StartCoroutine(ReadResultLoop());
    }

    void Update()
    {
        if (isFinished) return;

        // 0の状態の間だけ集中時間を加算
        if (currentState == 0)
        {
            focusTime += Time.deltaTime;
        }
    }

    IEnumerator ReadResultLoop()
    {
        while (!isFinished)
        {
            ReadResultFile();

            yield return new WaitForSeconds(0.1f);
        }
    }

    void ReadResultFile()
    {
        if (!File.Exists(resultFilePath))
        {
            Debug.LogWarning("result.txt が見つかりません");
            return;
        }

        try
        {
            string text = File.ReadAllText(resultFilePath).Trim();

            Debug.Log("result.txt の中身：" + text);

            if (int.TryParse(text, out int value))
            {
                Debug.Log("読み取った判定値：" + value);
                ChangeState(value);
            }
            else
            {
                Debug.LogWarning("0/1/2として読み取れません：" + text);
            }
        }
    catch (System.Exception e)
    {
        Debug.LogError("result.txt読み込みエラー：" + e.Message);
    }
}

    void ChangeState(int value)
    {
        if (isFinished) return;

        if (value == 0)
        {
            SetFocus();
        }
        else if (value == 1)
        {
            SetDanger();
        }
        else if (value == 2)
        {
            SetOut();
        }
    }

    public void SetFocus()
    {
        currentState = 0;

        statusText.text = "集中している";
        statusPanel.color = focusColor;
    }

    public void SetDanger()
    {
        // 0→1になった瞬間だけ回数を増やす
        if (currentState != 1)
        {
            dangerCount++;
        }

        currentState = 1;

        statusText.text = "危ない";
        statusPanel.color = dangerColor;
    }

    public void SetOut()
    {
        if (isFinished) return;

        currentState = 2;
        isFinished = true;

        StartCoroutine(OutSequence());
    }

    IEnumerator OutSequence()
    {
        statusText.text = "喝！";
        statusPanel.color = outColor;

        // ここに振動センサ処理を入れる
        // SendVibration();

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("result");
    }
}