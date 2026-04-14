using UnityEngine;
using System.Collections;

public class HeartUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void PlayLoseAnimation()
    {
        StartCoroutine(LoseAnimation());
    }

    IEnumerator LoseAnimation()
    {
        float duration = 0.3f;
        float time = 0;

        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale * 0.5f;

        while (time < duration)
        {
            float t = time / duration;

            // diminui tamanho
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            // fade out
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);

            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        canvasGroup.alpha = 0;

        gameObject.SetActive(false);
    }
}