using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CalibrationController : MonoBehaviour
{
    [Header("画面")]
    public Image calibrationImage;

    // 1枚目：キャリブレーション中
    public Sprite calibratingSprite;

    // 2枚目：キャリブレーション完了
    public Sprite completedSprite;

    public Button startButton;


    // MyBrainApp の output フォルダ
    private readonly string outputFolder =
        @"C:\Users\manam\OneDrive\デスクトップ\MixLab\SICHI\2026\neu_ExBrainSdk_dotnet_v3.1.0\samples\MyBrainApp\bin\Debug\net8.0\output";

    private string commandPath;
    private string calibrationPath;

    private bool calibrationCompleted = false;


    void Start()
    {
        commandPath = Path.Combine(outputFolder, "command.txt");
        calibrationPath = Path.Combine(outputFolder, "calibration.txt");

        // 最初は「キャリブレーション中」の画像
        calibrationImage.sprite = calibratingSprite;

        // キャリブレーションが終わるまで開始ボタンは押せない
        startButton.interactable = false;

        // 脳血流側へキャリブレーション開始命令
        StartCalibration();

        // calibration.txt の監視開始
        StartCoroutine(CheckCalibration());
    }


    void StartCalibration()
    {
        try
        {
            File.WriteAllText(commandPath, "1");

            Debug.Log("command.txt に 1 を書き込みました");
            Debug.Log("書き込み先: " + commandPath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("command.txt 書き込み失敗: " + e.Message);
        }
    }


    IEnumerator CheckCalibration()
    {
        // 前回の calibration.txt = 1 を
        // 間違って完了扱いしないためのフラグ
        bool sawCalibrationStart = false;

        while (!calibrationCompleted)
        {
            if (File.Exists(calibrationPath))
            {
                try
                {
                    string value =
                        File.ReadAllText(calibrationPath).Trim();

                    // MyBrainAppがキャリブレーションを開始すると
                    // calibration.txt = 0
                    if (value == "0")
                    {
                        sawCalibrationStart = true;
                    }

                    // 一度0になったあと1になったら本当に完了
                    if (sawCalibrationStart && value == "1")
                    {
                        CompleteCalibration();
                        yield break;
                    }
                }
                catch
                {
                    // MyBrainAppがファイル書き込み中の場合は
                    // 次の確認まで待つ
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }


    void CompleteCalibration()
    {
        calibrationCompleted = true;

        // 完了画像へ切り替え
        calibrationImage.sprite = completedSprite;

        // 開始ボタンを押せるようにする
        startButton.interactable = true;

        Debug.Log("キャリブレーション完了");
    }


    public void OnClickStart()
    {
        if (!calibrationCompleted)
        {
            return;
        }

        try
        {
            // 2 = リアルタイム判定開始
            File.WriteAllText(commandPath, "2");

            Debug.Log("脳血流判定開始");
        }
        catch (System.Exception e)
        {
            Debug.LogError(
                "command.txt に書き込めませんでした：" + e.Message
            );

            return;
        }

        // ゲーム画面へ
        SceneManager.LoadScene("game");
    }
}