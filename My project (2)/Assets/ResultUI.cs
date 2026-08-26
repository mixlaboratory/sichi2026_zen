using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    public TMP_Text focusTimeText;
    public TMP_Text dangerCountText;

    void Start()
    {
        int totalSeconds = Mathf.FloorToInt(StatusUIController.focusTime);

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        focusTimeText.text =
            $"W’†‚Å‚«‚½ŠÔF{minutes:00}:{seconds:00}";

        dangerCountText.text =
            $"Šë‚È‚¢‰ñ”F{StatusUIController.dangerCount}‰ñ";
    }
}