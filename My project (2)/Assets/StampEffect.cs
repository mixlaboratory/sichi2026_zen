using System.Collections;
using UnityEngine;

public class StampEffect : MonoBehaviour
{
    public GameObject stampImage;
    public AudioSource stampSound;

    void Start()
    {
        StartCoroutine(ShowStamp());
    }

    IEnumerator ShowStamp()
    {
        // 最初はハンコを隠す
        stampImage.SetActive(false);

        // リザルト画面表示後、1秒待つ
        yield return new WaitForSeconds(1f);

        // ハンコを表示
        stampImage.SetActive(true);

        // 最初は大きく表示
        stampImage.transform.localScale =
            new Vector3(1.3f, 1.3f, 1f);

        // 効果音
        stampSound.Play();

        // 0.15秒で通常サイズまで縮める
        float time = 0f;
        float duration = 0.15f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float scale = Mathf.Lerp(
                2f,
                1f,
                time / duration
            );

            stampImage.transform.localScale =
                new Vector3(scale, scale, 1f);

            yield return null;
        }

        stampImage.transform.localScale = Vector3.one;
    }
}