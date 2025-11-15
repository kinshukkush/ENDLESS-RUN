using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Enhanced Main Menu with modern UI effects and theme support
/// </summary>
public class MainMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public Text versionText;
    public GameObject loadingPanel;
    public Image loadingBar;
    public Text loadingText;

    [Header("Theme Selection")]
    public Button[] themeButtons;
    public int currentTheme = 0;

    [Header("Audio")]
    public AudioClip buttonClickSound;
    public AudioClip themeChangeSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (versionText != null)
        {
            versionText.text = "v" + Application.version;
        }

        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }

        InitializeThemeButtons();
    }

    /// <summary>
    /// Load a scene with smooth transition
    /// </summary>
    public void LoadScene(string name)
    {
        PlayButtonSound();
        StartCoroutine(LoadSceneAsync(name));
    }

    /// <summary>
    /// Asynchronously load scene with loading bar
    /// </summary>
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(true);
        }

        yield return new WaitForSeconds(0.2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (loadingBar != null)
            {
                loadingBar.fillAmount = progress;
            }

            if (loadingText != null)
            {
                loadingText.text = $"Loading... {Mathf.Round(progress * 100)}%";
            }

            if (operation.progress >= 0.9f)
            {
                if (loadingText != null)
                {
                    loadingText.text = "Press any key to continue";
                }

                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    /// <summary>
    /// Quit the application
    /// </summary>
    public void QuitGame()
    {
        PlayButtonSound();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Change UI theme
    /// </summary>
    public void ChangeTheme(int themeIndex)
    {
        currentTheme = themeIndex;
        PlaySound(themeChangeSound);

        if (UIThemeManager.instance != null)
        {
            UIThemeManager.instance.SetThemePreset((UIThemeManager.ThemePreset)themeIndex);
        }

        UpdateThemeButtonHighlight();
    }

    /// <summary>
    /// Initialize theme selection buttons
    /// </summary>
    private void InitializeThemeButtons()
    {
        if (themeButtons == null || themeButtons.Length == 0)
            return;

        for (int i = 0; i < themeButtons.Length; i++)
        {
            int index = i;
            if (themeButtons[i] != null)
            {
                themeButtons[i].onClick.AddListener(() => ChangeTheme(index));
            }
        }

        UpdateThemeButtonHighlight();
    }

    /// <summary>
    /// Update visual highlight for selected theme
    /// </summary>
    private void UpdateThemeButtonHighlight()
    {
        if (themeButtons == null)
            return;

        for (int i = 0; i < themeButtons.Length; i++)
        {
            if (themeButtons[i] != null)
            {
                ColorBlock colors = themeButtons[i].colors;
                colors.normalColor = i == currentTheme ? Color.yellow : Color.white;
                themeButtons[i].colors = colors;

                // Add pulse animation to selected button
                UIAnimator animator = themeButtons[i].GetComponent<UIAnimator>();
                if (animator != null && i == currentTheme)
                {
                    animator.PlayPulse();
                }
            }
        }
    }

    /// <summary>
    /// Play button click sound
    /// </summary>
    private void PlayButtonSound()
    {
        PlaySound(buttonClickSound);
    }

    /// <summary>
    /// Play audio clip
    /// </summary>
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// Open external URL
    /// </summary>
    public void OpenURL(string url)
    {
        PlayButtonSound();
        Application.OpenURL(url);
    }
}
