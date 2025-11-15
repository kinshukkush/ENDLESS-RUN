using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Enhanced Score Display with animations and visual effects
/// </summary>
public class EnhancedScoreDisplay : MonoBehaviour
{
    [Header("References")]
    public Text scoreText;
    public Text multiplierText;
    public Text comboText;
    public Image comboFillBar;

    [Header("Animation Settings")]
    public float scoreAnimationSpeed = 2.0f;
    public float popScale = 1.2f;
    public float popDuration = 0.2f;
    public Color multiplierColor = Color.yellow;
    public Color comboColor = Color.cyan;

    [Header("Combo Settings")]
    public int[] comboThresholds = new int[] { 5, 10, 20, 50 };
    public string[] comboMessages = new string[] { "Nice!", "Great!", "Awesome!", "Legendary!" };

    private int displayedScore = 0;
    private int targetScore = 0;
    private int currentCombo = 0;
    private int maxCombo = 100;
    private Coroutine scoreAnimCoroutine;
    private Vector3 originalScoreScale;
    private Vector3 originalMultiplierScale;

    private void Start()
    {
        if (scoreText != null)
        {
            originalScoreScale = scoreText.transform.localScale;
        }

        if (multiplierText != null)
        {
            originalMultiplierScale = multiplierText.transform.localScale;
        }

        UpdateDisplay();
    }

    /// <summary>
    /// Set the score with smooth animation
    /// </summary>
    public void SetScore(int newScore, bool animate = true)
    {
        targetScore = newScore;

        if (animate)
        {
            if (scoreAnimCoroutine != null)
            {
                StopCoroutine(scoreAnimCoroutine);
            }
            scoreAnimCoroutine = StartCoroutine(AnimateScore());
        }
        else
        {
            displayedScore = targetScore;
            UpdateDisplay();
        }
    }

    /// <summary>
    /// Add points to the score
    /// </summary>
    public void AddScore(int points)
    {
        SetScore(targetScore + points, true);
        StartCoroutine(PopAnimation(scoreText, originalScoreScale));
    }

    /// <summary>
    /// Set multiplier value
    /// </summary>
    public void SetMultiplier(int multiplier)
    {
        if (multiplierText != null)
        {
            if (multiplier > 1)
            {
                multiplierText.text = $"x{multiplier}";
                multiplierText.color = Color.Lerp(Color.white, multiplierColor, (float)multiplier / 10f);
                StartCoroutine(PopAnimation(multiplierText, originalMultiplierScale));
            }
            else
            {
                multiplierText.text = "";
            }
        }
    }

    /// <summary>
    /// Increment combo counter
    /// </summary>
    public void IncrementCombo()
    {
        currentCombo++;
        UpdateComboDisplay();
        CheckComboThreshold();
        StartCoroutine(PopAnimation(comboText, Vector3.one));
    }

    /// <summary>
    /// Reset combo counter
    /// </summary>
    public void ResetCombo()
    {
        currentCombo = 0;
        UpdateComboDisplay();
    }

    /// <summary>
    /// Animate score counting up
    /// </summary>
    private IEnumerator AnimateScore()
    {
        while (displayedScore < targetScore)
        {
            int difference = targetScore - displayedScore;
            int increment = Mathf.Max(1, Mathf.CeilToInt(difference * scoreAnimationSpeed * Time.deltaTime));
            
            displayedScore = Mathf.Min(displayedScore + increment, targetScore);
            UpdateDisplay();
            
            yield return null;
        }

        displayedScore = targetScore;
        UpdateDisplay();
    }

    /// <summary>
    /// Update score text display
    /// </summary>
    private void UpdateDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = displayedScore.ToString("N0");
        }
    }

    /// <summary>
    /// Update combo display
    /// </summary>
    private void UpdateComboDisplay()
    {
        if (comboText != null)
        {
            if (currentCombo > 0)
            {
                comboText.text = $"Combo x{currentCombo}";
                comboText.color = Color.Lerp(Color.white, comboColor, (float)currentCombo / maxCombo);
            }
            else
            {
                comboText.text = "";
            }
        }

        if (comboFillBar != null)
        {
            comboFillBar.fillAmount = Mathf.Clamp01((float)currentCombo / maxCombo);
        }
    }

    /// <summary>
    /// Check if combo reached a threshold
    /// </summary>
    private void CheckComboThreshold()
    {
        for (int i = comboThresholds.Length - 1; i >= 0; i--)
        {
            if (currentCombo == comboThresholds[i])
            {
                ShowComboMessage(comboMessages[i]);
                break;
            }
        }
    }

    /// <summary>
    /// Show combo achievement message
    /// </summary>
    private void ShowComboMessage(string message)
    {
        if (comboText != null)
        {
            StartCoroutine(FlashComboMessage(message));
        }
    }

    /// <summary>
    /// Flash combo message animation
    /// </summary>
    private IEnumerator FlashComboMessage(string message)
    {
        string originalText = comboText.text;
        comboText.text = message;
        
        float duration = 1.5f;
        float elapsed = 0f;
        Vector3 originalScale = comboText.transform.localScale;
        Vector3 targetScale = originalScale * 1.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            // Scale animation
            if (t < 0.3f)
            {
                comboText.transform.localScale = Vector3.Lerp(originalScale, targetScale, t / 0.3f);
            }
            else
            {
                comboText.transform.localScale = Vector3.Lerp(targetScale, originalScale, (t - 0.3f) / 0.7f);
            }

            // Flash animation
            comboText.color = new Color(comboColor.r, comboColor.g, comboColor.b, 
                Mathf.PingPong(elapsed * 3, 1));

            yield return null;
        }

        comboText.transform.localScale = originalScale;
        comboText.text = originalText;
        UpdateComboDisplay();
    }

    /// <summary>
    /// Pop animation for UI elements
    /// </summary>
    private IEnumerator PopAnimation(Text text, Vector3 originalScale)
    {
        if (text == null) yield break;

        Vector3 targetScale = originalScale * popScale;
        float elapsed = 0f;
        float halfDuration = popDuration / 2f;

        // Scale up
        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / halfDuration;
            text.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        elapsed = 0f;

        // Scale down
        while (elapsed < halfDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / halfDuration;
            text.transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        text.transform.localScale = originalScale;
    }
}
