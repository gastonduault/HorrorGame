using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplayService : MonoBehaviour
{

    public static TextDisplayService Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private float defaultDuration = 3f;
    [SerializeField] private float fadeDuration = 0.5f;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// Displays the given text for a specified duration.
    /// </summary>
    /// <param name="message">The text to display.</param>
    /// <param name="duration">How long to display the text. If not provided use defaultDuration.</param>
    public void ShowText(string message, float duration = -1f)
    {
        if (duration <= 0f) duration = defaultDuration;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(DisplayTextRoutine(message, duration));
    }

    private IEnumerator DisplayTextRoutine(string message, float duration)
    {
        displayText.text = message;
        SetTextAlpha(0f);

        yield return FadeText(displayText, fadeDuration, fadeIn: true);

        yield return new WaitForSeconds(duration);

        yield return FadeText(displayText, fadeDuration, fadeIn: false);

    }

    /// <summary>
    /// Coroutine to fade the text's alpha over time.
    /// </summary>
    /// <param name="text">The TextMeshProUGUI component.</param>
    /// <param name="duration">Duration of the fade.</param>
    /// <param name="fadeIn">True to fade in (alpha 0 to 1), false to fade out (alpha 1 to 0).</param>
    private IEnumerator FadeText(TextMeshProUGUI text, float duration, bool fadeIn)
    {
        float startAlpha = fadeIn ? 0f : 1f;
        float endAlpha = fadeIn ? 1f : 0f;
        float timer = 0f;

        SetTextAlpha(startAlpha);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / duration);
            SetTextAlpha(alpha);
            yield return null;
        }

        SetTextAlpha(endAlpha);
    }

    /// <summary>
    /// Helper method to set the alpha of the text color.
    /// </summary>
    private void SetTextAlpha(float alpha)
    {
        Color color = displayText.color;
        color.a = alpha;
        displayText.color = color;
    }
}

