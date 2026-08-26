using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StatusUIController : MonoBehaviour
{
    public TMP_Text statusText;
    public Image statusPanel;

    public Color focusColor = Color.green;
    public Color dangerColor = Color.yellow;
    public Color outColor = Color.red;

    // リザルト画面に渡す値
    public static int dangerCount = 0;
    public static float focusTime = 0f;

    // 1 = 集中している
    // 2 = 危ない
    // 3 = アウト
    private int currentState = 1;

    private bool isFinished = false;

    void Start()
    {
        // ゲーム開始時にリセット
        dangerCount = 0;
        focusTime = 0f;

        SetFocus();
    }

    void Update()
    {
        if (isFinished) return;

        // 集中している間だけ時間を加算
        if (currentState == 1)
        {
            focusTime += Time.deltaTime;
        }

        // テスト用
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            SetFocus();

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            SetDanger();

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
            SetOut();
    }

    public void SetFocus()
    {
        currentState = 1;

        statusText.text = "集中している";
        statusPanel.color = focusColor;
    }

    public void SetDanger()
    {
        currentState = 2;

        // 「危ない」になった回数を1増やす
        dangerCount++;

        statusText.text = "危ない";
        statusPanel.color = dangerColor;
    }

    public void SetOut()
    {
        if (isFinished) return;

        currentState = 3;
        isFinished = true;

        StartCoroutine(OutSequence());
    }

    IEnumerator OutSequence()
    {
        // 「喝！」を表示
        statusText.text = "喝！";
        statusPanel.color = outColor;

        // ここでセンサを発動
        // SendVibration();

        // 3秒待つ
        yield return new WaitForSeconds(3f);

        // リザルト画面へ
        SceneManager.LoadScene("result");
    }
}