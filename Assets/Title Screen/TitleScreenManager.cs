using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image fadeImage;          // A full-screen black UI Image
    public float fadeDuration = 1.5f;
    public string nextSceneName = "GameScene";

    public void OnPlayPressed()
    {
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        float elapsed = 0f;
        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;
        fadeImage.gameObject.SetActive(true);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
