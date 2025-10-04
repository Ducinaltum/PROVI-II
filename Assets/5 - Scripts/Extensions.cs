using System.Collections;
using UnityEngine;

public static class Extensions
{
    public static IEnumerator Fade(this CanvasGroup canvasGroup, float target, float duration)
    {
        float speedCoeficent = 1 / duration;
        float t = 0;
        while (t < 1)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, target, t);
            t += Time.deltaTime * speedCoeficent;
            yield return null;
        }
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, target, 1);
    }
}