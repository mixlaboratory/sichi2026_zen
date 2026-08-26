using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StatusUIController : MonoBehaviour
{
    public TMP_Text statusText;
    public Image statusPanel;

    public Color focusColor = new Color(0.2f, 0.7f, 0.3f, 0.8f);
    public Color dangerColor = new Color(1.0f, 0.7f, 0.0f, 0.8f);
    public Color outColor = new Color(0.9f, 0.2f, 0.2f, 0.8f);

    void Start()
    {
        SetFocus();
    }

    void Update()
{
    if (Keyboard.current.digit1Key.wasPressedThisFrame)
        SetFocus();

    if (Keyboard.current.digit2Key.wasPressedThisFrame)
        SetDanger();

    if (Keyboard.current.digit3Key.wasPressedThisFrame)
        SetOut();
}

    public void SetFocus()
    {
        statusText.text = "集中している";
        statusPanel.color = focusColor;
    }

    public void SetDanger()
    {
        statusText.text = "危ない";
        statusPanel.color = dangerColor;
    }

    public void SetOut()
    {
        statusText.text = "アウト";
        statusPanel.color = outColor;
    }
}