using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Modern UI Animator - Provides smooth transitions, scale effects, and fade animations
/// </summary>
public class UIAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    public AnimationType animationType = AnimationType.ScaleAndFade;
    public float animationDuration = 0.3f;
    public AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public bool animateOnEnable = true;
    public bool loopAnimation = false;

    [Header("Scale Settings")]
    public Vector3 startScale = new Vector3(0.8f, 0.8f, 0.8f);
    public Vector3 endScale = Vector3.one;

    [Header("Fade Settings")]
    public float startAlpha = 0f;
    public float endAlpha = 1f;

    [Header("Position Settings")]
    public Vector3 startPositionOffset = Vector3.zero;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private Vector3 originalScale;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        if (canvasGroup == null && (animationType == AnimationType.Fade || animationType == AnimationType.ScaleAndFade))
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        originalPosition = rectTransform != null ? rectTransform.anchoredPosition : transform.localPosition;
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        if (animateOnEnable)
        {
            PlayAnimation();
        }
    }

    /// <summary>
    /// Play the configured animation
    /// </summary>
    public void PlayAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateCoroutine());
    }

    /// <summary>
    /// Play animation in reverse
    /// </summary>
    public void PlayReverseAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateCoroutine(true));
    }

    private IEnumerator AnimateCoroutine(bool reverse = false)
    {
        float elapsed = 0f;

        // Set initial state
        if (!reverse)
        {
            SetInitialState();
        }

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);
            float curveValue = animationCurve.Evaluate(reverse ? 1 - t : t);

            ApplyAnimation(reverse ? 1 - curveValue : curveValue);

            yield return null;
        }

        // Ensure final state
        ApplyAnimation(reverse ? 0f : 1f);

        if (loopAnimation)
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(AnimateCoroutine(!reverse));
        }
    }

    private void SetInitialState()
    {
        switch (animationType)
        {
            case AnimationType.Scale:
                transform.localScale = startScale;
                break;

            case AnimationType.Fade:
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = startAlpha;
                }
                break;

            case AnimationType.ScaleAndFade:
                transform.localScale = startScale;
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = startAlpha;
                }
                break;

            case AnimationType.Position:
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = originalPosition + startPositionOffset;
                }
                break;

            case AnimationType.All:
                transform.localScale = startScale;
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = startAlpha;
                }
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = originalPosition + startPositionOffset;
                }
                break;
        }
    }

    private void ApplyAnimation(float t)
    {
        switch (animationType)
        {
            case AnimationType.Scale:
                transform.localScale = Vector3.Lerp(startScale, endScale, t);
                break;

            case AnimationType.Fade:
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
                }
                break;

            case AnimationType.ScaleAndFade:
                transform.localScale = Vector3.Lerp(startScale, endScale, t);
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
                }
                break;

            case AnimationType.Position:
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = Vector3.Lerp(originalPosition + startPositionOffset, originalPosition, t);
                }
                break;

            case AnimationType.All:
                transform.localScale = Vector3.Lerp(startScale, endScale, t);
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
                }
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = Vector3.Lerp(originalPosition + startPositionOffset, originalPosition, t);
                }
                break;
        }
    }

    /// <summary>
    /// Pulse animation for button feedback
    /// </summary>
    public void PlayPulse(float intensity = 0.1f, float duration = 0.2f)
    {
        StopAllCoroutines();
        StartCoroutine(PulseCoroutine(intensity, duration));
    }

    private IEnumerator PulseCoroutine(float intensity, float duration)
    {
        Vector3 targetScale = originalScale * (1f + intensity);
        float halfDuration = duration / 2f;
        float elapsed = 0f;

        // Scale up
        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / halfDuration;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        elapsed = 0f;

        // Scale down
        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / halfDuration;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        transform.localScale = originalScale;
    }

    public enum AnimationType
    {
        Scale,
        Fade,
        ScaleAndFade,
        Position,
        All
    }
}
