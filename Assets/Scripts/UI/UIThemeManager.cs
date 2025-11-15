using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Modern UI Theme Manager - Handles dynamic theming with gradient backgrounds,
/// smooth animations, and modern color schemes
/// </summary>
public class UIThemeManager : MonoBehaviour
{
    public static UIThemeManager instance { get; private set; }

    [Header("Theme Colors")]
    public Color primaryColor = new Color(0.2f, 0.6f, 1.0f, 1.0f); // Modern Blue
    public Color secondaryColor = new Color(0.4f, 0.2f, 0.8f, 1.0f); // Purple
    public Color accentColor = new Color(1.0f, 0.4f, 0.2f, 1.0f); // Orange
    public Color backgroundColor = new Color(0.1f, 0.1f, 0.15f, 1.0f); // Dark Blue-Gray
    public Color textColor = Color.white;
    public Color textSecondaryColor = new Color(0.8f, 0.8f, 0.8f, 1.0f);

    [Header("UI Elements to Theme")]
    public List<Image> backgroundElements = new List<Image>();
    public List<Image> primaryElements = new List<Image>();
    public List<Image> secondaryElements = new List<Image>();
    public List<Image> accentElements = new List<Image>();
    public List<Text> textElements = new List<Text>();
    public List<Text> secondaryTextElements = new List<Text>();

    [Header("Animation Settings")]
    public float pulseSpeed = 2.0f;
    public float pulseIntensity = 0.1f;
    public bool enablePulseAnimation = true;

    private float animationTime = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ApplyTheme();
    }

    private void Update()
    {
        if (enablePulseAnimation)
        {
            animationTime += Time.deltaTime * pulseSpeed;
            AnimatePrimaryElements();
        }
    }

    /// <summary>
    /// Apply the current theme to all registered UI elements
    /// </summary>
    public void ApplyTheme()
    {
        // Apply colors to background elements
        foreach (var element in backgroundElements)
        {
            if (element != null)
            {
                element.color = backgroundColor;
            }
        }

        // Apply primary color
        foreach (var element in primaryElements)
        {
            if (element != null)
            {
                element.color = primaryColor;
            }
        }

        // Apply secondary color
        foreach (var element in secondaryElements)
        {
            if (element != null)
            {
                element.color = secondaryColor;
            }
        }

        // Apply accent color
        foreach (var element in accentElements)
        {
            if (element != null)
            {
                element.color = accentColor;
            }
        }

        // Apply text colors
        foreach (var text in textElements)
        {
            if (text != null)
            {
                text.color = textColor;
            }
        }

        foreach (var text in secondaryTextElements)
        {
            if (text != null)
            {
                text.color = textSecondaryColor;
            }
        }
    }

    /// <summary>
    /// Animate primary elements with a subtle pulse effect
    /// </summary>
    private void AnimatePrimaryElements()
    {
        float pulse = 1.0f + Mathf.Sin(animationTime) * pulseIntensity;
        
        foreach (var element in accentElements)
        {
            if (element != null)
            {
                Color baseColor = accentColor;
                element.color = new Color(
                    baseColor.r * pulse,
                    baseColor.g * pulse,
                    baseColor.b * pulse,
                    baseColor.a
                );
            }
        }
    }

    /// <summary>
    /// Switch to a new theme preset
    /// </summary>
    public void SetThemePreset(ThemePreset preset)
    {
        switch (preset)
        {
            case ThemePreset.Modern:
                primaryColor = new Color(0.2f, 0.6f, 1.0f, 1.0f);
                secondaryColor = new Color(0.4f, 0.2f, 0.8f, 1.0f);
                accentColor = new Color(1.0f, 0.4f, 0.2f, 1.0f);
                backgroundColor = new Color(0.1f, 0.1f, 0.15f, 1.0f);
                break;

            case ThemePreset.Sunset:
                primaryColor = new Color(1.0f, 0.5f, 0.2f, 1.0f);
                secondaryColor = new Color(1.0f, 0.3f, 0.5f, 1.0f);
                accentColor = new Color(1.0f, 0.8f, 0.2f, 1.0f);
                backgroundColor = new Color(0.15f, 0.1f, 0.12f, 1.0f);
                break;

            case ThemePreset.Ocean:
                primaryColor = new Color(0.1f, 0.5f, 0.7f, 1.0f);
                secondaryColor = new Color(0.2f, 0.7f, 0.6f, 1.0f);
                accentColor = new Color(0.3f, 0.9f, 0.8f, 1.0f);
                backgroundColor = new Color(0.05f, 0.1f, 0.15f, 1.0f);
                break;

            case ThemePreset.Forest:
                primaryColor = new Color(0.3f, 0.7f, 0.3f, 1.0f);
                secondaryColor = new Color(0.5f, 0.8f, 0.2f, 1.0f);
                accentColor = new Color(1.0f, 0.7f, 0.2f, 1.0f);
                backgroundColor = new Color(0.1f, 0.12f, 0.08f, 1.0f);
                break;

            case ThemePreset.Neon:
                primaryColor = new Color(1.0f, 0.0f, 1.0f, 1.0f);
                secondaryColor = new Color(0.0f, 1.0f, 1.0f, 1.0f);
                accentColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                backgroundColor = new Color(0.05f, 0.05f, 0.1f, 1.0f);
                break;
        }

        ApplyTheme();
    }

    /// <summary>
    /// Register a new UI element to be themed
    /// </summary>
    public void RegisterElement(Image element, ElementType type)
    {
        if (element == null) return;

        switch (type)
        {
            case ElementType.Background:
                if (!backgroundElements.Contains(element))
                    backgroundElements.Add(element);
                break;
            case ElementType.Primary:
                if (!primaryElements.Contains(element))
                    primaryElements.Add(element);
                break;
            case ElementType.Secondary:
                if (!secondaryElements.Contains(element))
                    secondaryElements.Add(element);
                break;
            case ElementType.Accent:
                if (!accentElements.Contains(element))
                    accentElements.Add(element);
                break;
        }

        ApplyTheme();
    }

    public enum ThemePreset
    {
        Modern,
        Sunset,
        Ocean,
        Forest,
        Neon
    }

    public enum ElementType
    {
        Background,
        Primary,
        Secondary,
        Accent
    }
}
