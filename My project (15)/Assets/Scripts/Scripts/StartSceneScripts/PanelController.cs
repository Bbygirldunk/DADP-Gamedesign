using UnityEngine;
using System.Collections;

public class PanelController : MonoBehaviour
{
    public CanvasGroup panel1;
    public CanvasGroup panel2;
    public CanvasGroup panel3;
    public float fadeDuration = 0.5f;

    private Coroutine panel1FadeCoroutine;
    private Coroutine panel2FadeCoroutine;
    private Coroutine panel3FadeCoroutine;

    public void ShowPanel1()
    {
        if (panel1FadeCoroutine != null) StopCoroutine(panel1FadeCoroutine);
        if (panel2FadeCoroutine != null) StopCoroutine(panel2FadeCoroutine);
        if (panel3FadeCoroutine != null) StopCoroutine(panel3FadeCoroutine);

        panel1FadeCoroutine = StartCoroutine(FadeIn(panel1));
        panel2FadeCoroutine = StartCoroutine(FadeOut(panel2));
        panel3FadeCoroutine = StartCoroutine(FadeOut(panel3));
    }

    public void ShowPanel2()
    {
        if (panel1FadeCoroutine != null) StopCoroutine(panel1FadeCoroutine);
        if (panel2FadeCoroutine != null) StopCoroutine(panel2FadeCoroutine);
        if (panel3FadeCoroutine != null) StopCoroutine(panel3FadeCoroutine);

        panel1FadeCoroutine = StartCoroutine(FadeOut(panel1));
        panel2FadeCoroutine = StartCoroutine(FadeIn(panel2));
        panel3FadeCoroutine = StartCoroutine(FadeOut(panel3));
    }

    public void ShowPanel3()
    {
        if (panel1FadeCoroutine != null) StopCoroutine(panel1FadeCoroutine);
        if (panel2FadeCoroutine != null) StopCoroutine(panel2FadeCoroutine);
        if (panel3FadeCoroutine != null) StopCoroutine(panel3FadeCoroutine);

        panel1FadeCoroutine = StartCoroutine(FadeOut(panel1));
        panel2FadeCoroutine = StartCoroutine(FadeOut(panel2));
        panel3FadeCoroutine = StartCoroutine(FadeIn(panel3));
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        canvasGroup.gameObject.SetActive(true); // Make sure the panel is active
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        float time = 0;
        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = false; // Disable interactions during fade-out
        canvasGroup.blocksRaycasts = false;

        float time = 0;
        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false); // Disable the panel completely when fully faded out
    }
}
