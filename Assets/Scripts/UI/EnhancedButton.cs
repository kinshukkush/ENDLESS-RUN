using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Enhanced Button with modern visual effects and audio feedback
/// </summary>
[RequireComponent(typeof(Button))]
public class EnhancedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Visual Effects")]
    public bool enableHoverEffect = true;
    public bool enableClickEffect = true;
    public bool enableRippleEffect = true;
    public float hoverScale = 1.05f;
    public float clickScale = 0.95f;
    public float transitionSpeed = 10f;

    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color hoverColor = new Color(0.9f, 0.9f, 1f);
    public Color pressedColor = new Color(0.8f, 0.8f, 0.9f);
    public Color disabledColor = new Color(0.5f, 0.5f, 0.5f);

    [Header("Ripple Effect")]
    public GameObject ripplePrefab;
    public float rippleDuration = 0.5f;

    [Header("Audio")]
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public float volume = 1.0f;

    [Header("Particle Effect")]
    public GameObject clickParticlePrefab;

    private Button button;
    private Image buttonImage;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private Color targetColor;
    private AudioSource audioSource;
    private bool isHovered = false;
    private bool isPressed = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        
        originalScale = transform.localScale;
        targetScale = originalScale;
        targetColor = normalColor;

        // Setup audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    private void Update()
    {
        // Smooth scale transition
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * transitionSpeed);

        // Smooth color transition
        if (buttonImage != null)
        {
            buttonImage.color = Color.Lerp(buttonImage.color, targetColor, Time.deltaTime * transitionSpeed);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable) return;

        isHovered = true;

        if (enableHoverEffect)
        {
            targetScale = originalScale * hoverScale;
            targetColor = hoverColor;
        }

        PlaySound(hoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!button.interactable) return;

        isHovered = false;

        if (!isPressed)
        {
            targetScale = originalScale;
            targetColor = normalColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!button.interactable) return;

        isPressed = true;

        if (enableClickEffect)
        {
            targetScale = originalScale * clickScale;
            targetColor = pressedColor;
        }

        if (enableRippleEffect)
        {
            CreateRipple(eventData.position);
        }

        PlaySound(clickSound);
        SpawnClickParticle();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!button.interactable) return;

        isPressed = false;

        if (isHovered)
        {
            targetScale = originalScale * hoverScale;
            targetColor = hoverColor;
        }
        else
        {
            targetScale = originalScale;
            targetColor = normalColor;
        }
    }

    /// <summary>
    /// Create ripple effect at touch position
    /// </summary>
    private void CreateRipple(Vector2 position)
    {
        if (ripplePrefab == null) return;

        GameObject ripple = Instantiate(ripplePrefab, transform);
        RectTransform rippleRect = ripple.GetComponent<RectTransform>();

        if (rippleRect != null)
        {
            // Convert screen position to local position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform as RectTransform,
                position,
                null,
                out Vector2 localPoint
            );

            rippleRect.anchoredPosition = localPoint;
            StartCoroutine(AnimateRipple(ripple));
        }
    }

    /// <summary>
    /// Animate ripple effect
    /// </summary>
    private IEnumerator AnimateRipple(GameObject ripple)
    {
        Image rippleImage = ripple.GetComponent<Image>();
        RectTransform rippleRect = ripple.GetComponent<RectTransform>();

        if (rippleImage == null || rippleRect == null)
        {
            Destroy(ripple);
            yield break;
        }

        Vector2 startSize = Vector2.zero;
        Vector2 endSize = new Vector2(200, 200);
        float elapsed = 0f;

        while (elapsed < rippleDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / rippleDuration;

            // Scale ripple
            rippleRect.sizeDelta = Vector2.Lerp(startSize, endSize, t);

            // Fade out
            Color color = rippleImage.color;
            color.a = Mathf.Lerp(0.5f, 0f, t);
            rippleImage.color = color;

            yield return null;
        }

        Destroy(ripple);
    }

    /// <summary>
    /// Spawn click particle effect
    /// </summary>
    private void SpawnClickParticle()
    {
        if (clickParticlePrefab == null) return;

        GameObject particle = Instantiate(clickParticlePrefab, transform.position, Quaternion.identity);
        Destroy(particle, 2f);
    }

    /// <summary>
    /// Play audio clip
    /// </summary>
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    /// <summary>
    /// Enable or disable the button
    /// </summary>
    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
        targetColor = interactable ? normalColor : disabledColor;
        
        if (!interactable)
        {
            targetScale = originalScale;
            isHovered = false;
            isPressed = false;
        }
    }

    /// <summary>
    /// Shake animation for error feedback
    /// </summary>
    public void Shake(float intensity = 10f, float duration = 0.3f)
    {
        StartCoroutine(ShakeCoroutine(intensity, duration));
    }

    private IEnumerator ShakeCoroutine(float intensity, float duration)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float x = Random.Range(-1f, 1f) * intensity;
            float y = Random.Range(-1f, 1f) * intensity;

            transform.localPosition = originalPosition + new Vector3(x, y, 0f);

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    /// <summary>
    /// Pulse animation
    /// </summary>
    public void Pulse(float intensity = 0.15f, int pulseCount = 1)
    {
        StartCoroutine(PulseCoroutine(intensity, pulseCount));
    }

    private IEnumerator PulseCoroutine(float intensity, int pulseCount)
    {
        for (int i = 0; i < pulseCount; i++)
        {
            Vector3 targetPulseScale = originalScale * (1f + intensity);
            float pulseDuration = 0.15f;
            float elapsed = 0f;

            // Scale up
            while (elapsed < pulseDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / pulseDuration;
                transform.localScale = Vector3.Lerp(originalScale, targetPulseScale, t);
                yield return null;
            }

            elapsed = 0f;

            // Scale down
            while (elapsed < pulseDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / pulseDuration;
                transform.localScale = Vector3.Lerp(targetPulseScale, originalScale, t);
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }

        transform.localScale = originalScale;
    }
}
