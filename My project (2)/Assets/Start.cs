using System.Collections;
using UnityEngine;
using TMPro;

public class StartCountdown : MonoBehaviour
{
    public TMP_Text messageText;
    public TMP_Text countdownText;
    public GameObject startPanel;

    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        messageText.text = "修行を開始します";
        countdownText.text = "";

        yield return new WaitForSeconds(1.5f);

        // 「修行を開始します」を消す
        messageText.text = "";

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "開始";
        yield return new WaitForSeconds(1f);

        startPanel.SetActive(false);
    }
}