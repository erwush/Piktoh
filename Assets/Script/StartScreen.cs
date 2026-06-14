using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class StartScreen : MonoBehaviour
{
    [Header("Objects")]
    public Image[] logo;
    public TextMeshProUGUI textDisplay;
    public Image ireng;

    [Header("Timing")]
    public float fadeTime = 1f;
    public float showTime = 2f;

    [Header("Texts")]
    public string text1 = "Powered By";
    public string text2 = "John Studio";

    [Header("Finish")]
    public UnityEvent onFinished;

    private void Start()
    {
        foreach (var l in logo)
            SetAlpha(l, 0);

        SetAlpha(textDisplay, 0);

        if (ireng != null)
            ireng.gameObject.SetActive(false);
    }

    public void StartSequence()
    {
        if (ireng != null)
            ireng.gameObject.SetActive(true);

        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        // =====================
        // Logo (barengan)
        // =====================
        foreach (var l in logo)
            StartCoroutine(Fade(l, 0, 1));

        yield return new WaitForSeconds(fadeTime);

        yield return new WaitForSeconds(showTime);

        foreach (var l in logo)
            StartCoroutine(Fade(l, 1, 0));

        yield return new WaitForSeconds(fadeTime);

        // =====================
        // Text 1
        // =====================
        textDisplay.text = text1;

        yield return Fade(textDisplay, 0, 1);
        yield return new WaitForSeconds(showTime);
        yield return Fade(textDisplay, 1, 0);

        // =====================
        // Text 2
        // =====================
        textDisplay.text = text2;

        yield return Fade(textDisplay, 0, 1);
        yield return new WaitForSeconds(showTime);
        yield return Fade(textDisplay, 1, 0);

        // =====================
        // Finish
        // =====================
        onFinished?.Invoke();
    }

    IEnumerator Fade(Graphic g, float from, float to)
    {
        float t = 0f;

        Color c = g.color;
        c.a = from;
        g.color = c;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            c.a = Mathf.Lerp(from, to, t / fadeTime);
            g.color = c;

            yield return null;
        }

        c.a = to;
        g.color = c;
    }

    void SetAlpha(Graphic g, float alpha)
    {
        Color c = g.color;
        c.a = alpha;
        g.color = c;
    }
}