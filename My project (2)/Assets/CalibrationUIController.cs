using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CalibrationUIController : MonoBehaviour
{
    public Image calibrationImage;

    public Sprite calibratingSprite;   // 1枚目
    public Sprite completedSprite;     // 2枚目

    public Button startButton;

    private bool isCalibrationComplete = false;

    void Start()
    {
        // 最初は1枚目
        calibrationImage.sprite = calibratingSprite;

        // 最初は開始ボタンを押せない
        startButton.interactable = false;
    }

    // キャリブレーション完了時に呼ぶ
    public void SetCalibrationComplete()
    {
        if (isCalibrationComplete) return;

        isCalibrationComplete = true;

        // 2枚目に切り替え
        calibrationImage.sprite = completedSprite;

        // 開始ボタンを押せるようにする
        startButton.interactable = true;
    }

    // 開始ボタンを押したとき
    public void OnClickStart()
    {
        SceneManager.LoadScene("game");
    }
}