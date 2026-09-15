using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CalibrationController : MonoBehaviour
{
    [Header("画面")]
    public Image calibrationImage;

    // 1枚目：キャリブレーション中
    public Sprite calibratingSprite;

    // 2枚目：キャリブレーション完了
    public Sprite completedSprite;

    public Button startButton;

    [SerializeField]
    private TextMeshProUGUI calibrationText;

    private string commandPath;
    private string calibrationPath;

    private bool calibrationCompleted = false;

    // コルーチンを停止できるように変数で保持
    private Coroutine fadeCoroutine;

    void Start()
    {
        // 最初は「キャリブレーション中」の画像
        calibrationImage.sprite = calibratingSprite;

        // キャリブレーションが終わるまで開始ボタンは押せない
        startButton.interactable = false;

        // 脳血流側へキャリブレーション開始命令
        StartCalibration();

        // calibration.txt の監視開始
        StartCoroutine(CheckCalibration());

        // フェード処理の開始（正しい名前を指定）
        fadeCoroutine = StartCoroutine(FadeInOutRoutine());
    }

    // 徐々に透明・不透明を繰り返すコルーチン
    IEnumerator FadeInOutRoutine()
    {
        float duration = 1.0f; // フェードにかける時間（秒）

        while (true)
        {
            // フェードアウト（透明にする）
            float elapsedTime = 0f;
            Color startColor = calibrationText.color;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                calibrationText.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }

            // フェードイン（不透明にする）
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
                calibrationText.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }
        }
    }

    void StartCalibration()
    {
        try
        {
            // 1 = キャリブレーション開始
            File.WriteAllText(commandPath, "1");
            Debug.Log("脳血流センサへキャリブレーション開始を送信しました");
        }
        catch (System.Exception e)
        {
            Debug.LogError("command.txt に書き込めませんでした：" + e.Message);
        }
    }

    IEnumerator CheckCalibration()
    {
        bool sawCalibrationStart = false;

        while (!calibrationCompleted)
        {
            if (File.Exists(calibrationPath))
            {
                try
                {
                    string value = File.ReadAllText(calibrationPath).Trim();

                    if (value == "0")
                    {
                        sawCalibrationStart = true;
                    }

                    if (sawCalibrationStart && value == "1")
                    {
                        CompleteCalibration();
                        yield break;
                    }
                }
                catch
                {
                    // ファイル競合時は次回ループへ
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    void CompleteCalibration()
    {
        calibrationCompleted = true;

        // フェードコルーチンを安全に停止
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        // テキストオブジェクトを非アクティブにする（バグ修正）
        calibrationText.gameObject.SetActive(false);

        // 完了画像へ切り替え
        calibrationImage.sprite = completedSprite;

        // 開始ボタンを押せるようにする
        startButton.interactable = true;

        Debug.Log("キャリブレーション完了");
    }

    public void OnClickStart()
    {
        if (!calibrationCompleted) return;

        try
        {
            // 2 = リアルタイム判定開始
            File.WriteAllText(commandPath, "2");
            Debug.Log("脳血流判定開始");
        }
        catch (System.Exception e)
        {
            Debug.LogError("command.txt に書き込めませんでした：" + e.Message);
            return;
        }

        // ゲーム画面へ
        SceneManager.LoadScene("game");
    }
}
