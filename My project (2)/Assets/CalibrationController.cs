using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CalibrationController : MonoBehaviour
{
    public TMP_Text statusText;
    public Button startButton;

    private string calibrationFilePath =
        @"C:\Users\manam\OneDrive\デスクトップ\MixLab\SICHI\2026\neu_ExBrainSdk_dotnet_v3.1.0\samples\MyBrainApp\bin\Debug\net8.0\output\calibration.txt";

    void Start()
    {
        statusText.text = "キャリブレーション待機中";

        // 最初は開始ボタンを押せないようにする
        startButton.interactable = false;

        StartCoroutine(CheckCalibration());
    }

    IEnumerator CheckCalibration()
    {
        while (true)
        {
            if (File.Exists(calibrationFilePath))
            {
                try
                {
                    string value =
                        File.ReadAllText(calibrationFilePath).Trim();

                    if (value == "1")
                    {
                        statusText.text = "キャリブレーション完了";

                        startButton.interactable = true;

                        yield break;
                    }
                    else
                    {
                        statusText.text = "キャリブレーション中...";
                    }
                }
                catch
                {
                    // 書き込み中なら次回もう一度読む
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}