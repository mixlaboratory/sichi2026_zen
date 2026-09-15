using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CalibrationController : MonoBehaviour
{
    public Image calibrationImage;

    // 1枚目：キャリブレーション中
    public Sprite calibratingSprite;

    // 2枚目：キャリブレーション完了
    public Sprite completedSprite;

    public Button startButton;

    private bool isCompleted = false;

    void Start()
    {
        calibrationImage.sprite = calibratingSprite;
        startButton.interactable = false;
    }

    void Update()
    {
        if (!isCompleted && Keyboard.current.cKey.wasPressedThisFrame)
        {
            CompleteCalibration();
        }
    }

    void CompleteCalibration()
    {
        isCompleted = true;

        calibrationImage.sprite = completedSprite;
        startButton.interactable = true;

        Debug.Log("キャリブレーション完了");
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("game");
    }
}