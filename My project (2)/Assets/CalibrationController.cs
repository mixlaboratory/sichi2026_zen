using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CalibrationController : MonoBehaviour
{
    public Image calibrationImage;

    // 1���ځF�L�����u���[�V������
    public Sprite calibratingSprite;

    // 2���ځF�L�����u���[�V��������
    public Sprite completedSprite;

    public Button startButton;

    private bool isCompleted = false;

    void Start()
    {
        calibrationImage.sprite = calibratingSprite;
        startButton.gameObject.SetActive(false);
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
        startButton.gameObject.SetActive(true);

        Debug.Log("�L�����u���[�V��������");
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("game");
    }
}