using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public Image panel;
    [SerializeField] private string sceneName;
    [SerializeField] private float fadeDuration = 1f; // seconds to reach full alpha

    void Start()
    {
        if (panel == null)
        {
            Debug.LogError("Panel Image not assigned.");
            return;
        }
        StartCoroutine(FadeOutAndLoad());
    }

    IEnumerator FadeOutAndLoad()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("sceneName is empty.");
            yield break;
        }

        float startAlpha = panel.color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            Color c = panel.color;
            c.a = Mathf.Lerp(startAlpha, 1f, t);
            panel.color = c;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
