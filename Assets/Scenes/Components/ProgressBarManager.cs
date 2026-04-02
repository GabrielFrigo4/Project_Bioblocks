using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ProgressBarManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI labelText;

    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 0.8f;
    [SerializeField] private AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private bool animateOnUpdate = true;

    [Header("Gradient Settings")]
    [Tooltip("Ativa o gradiente de cores entre levels. Requer que fillImage use ImageType=Filled")]
    [SerializeField] private bool useLevelGradient = false;

    private float currentFillAmount = 0f;
    private Coroutine animationCoroutine;
    private Texture2D _gradientTexture;

    private void Awake()
    {
        if (fillImage == null)
            Debug.LogError($"[ProgressBarManager] {gameObject.name}: fillImage não está atribuído!");

        if (fillImage != null)
            fillImage.fillAmount = 0f;
    }

    private void OnDestroy()
    {
        if (_gradientTexture != null)
            Destroy(_gradientTexture);
    }

    // -------------------------------------------------------
    // Gradient
    // -------------------------------------------------------
    public void ApplyLevelGradient(int currentLevel)
    {
        if (!useLevelGradient || fillImage == null) return;

        int nextLevel    = Mathf.Min(currentLevel + 1, 10);
        Color colorStart = UserHeaderManager.LevelColors[Mathf.Clamp(currentLevel - 1, 0, 9)];
        Color colorEnd   = UserHeaderManager.LevelColors[Mathf.Clamp(nextLevel   - 1, 0, 9)];

        const int width = 128;

        if (_gradientTexture == null)
        {
            _gradientTexture          = new Texture2D(width, 1, TextureFormat.RGBA32, false);
            _gradientTexture.wrapMode = TextureWrapMode.Clamp;
            _gradientTexture.filterMode = FilterMode.Bilinear;
        }

        for (int x = 0; x < width; x++)
        {
            float t = x / (float)(width - 1);
            _gradientTexture.SetPixel(x, 0, Color.Lerp(colorStart, colorEnd, t));
        }

        _gradientTexture.Apply();

        // Aplica como sprite mantendo o comportamento de fillAmount intacto
        fillImage.sprite = Sprite.Create(
            _gradientTexture,
            new Rect(0, 0, width, 1),
            new Vector2(0.5f, 0.5f),
            pixelsPerUnit: 100f
        );

        fillImage.color = Color.white; // garante que a cor não tinja o gradiente
    }

    // -------------------------------------------------------
    // Update Progress
    // -------------------------------------------------------
    public void UpdateProgress(int current, int total, string customProgressText = null, string customLabelText = null)
    {
        if (fillImage == null)
        {
            Debug.LogWarning($"[ProgressBarManager] {gameObject.name}: fillImage não está configurado!");
            return;
        }

        float targetProgress = total > 0 ? (float)current / total : 0f;

        if (progressText != null && !string.IsNullOrEmpty(customProgressText))
            progressText.text = customProgressText;

        if (labelText != null && !string.IsNullOrEmpty(customLabelText))
            labelText.text = customLabelText;

        if (animateOnUpdate && gameObject.activeInHierarchy)
            AnimateToProgress(targetProgress);
        else
        {
            fillImage.fillAmount = targetProgress;
            currentFillAmount    = targetProgress;
        }

        Debug.Log($"[ProgressBarManager] {gameObject.name} atualizado: {current}/{total} ({targetProgress:P1}) - ProgressText: '{customProgressText}', LabelText: '{customLabelText}'");
    }

    public void UpdateProgressPercentage(float percentage, string customProgressText = null, string customLabelText = null)
    {
        float targetProgress = Mathf.Clamp01(percentage / 100f);

        if (progressText != null && !string.IsNullOrEmpty(customProgressText))
            progressText.text = customProgressText;

        if (labelText != null && !string.IsNullOrEmpty(customLabelText))
            labelText.text = customLabelText;

        if (animateOnUpdate && gameObject.activeInHierarchy)
            AnimateToProgress(targetProgress);
        else
        {
            if (fillImage != null) fillImage.fillAmount = targetProgress;
            currentFillAmount = targetProgress;
        }

        Debug.Log($"[ProgressBarManager] {gameObject.name} atualizado: {percentage}% - ProgressText: '{customProgressText}', LabelText: '{customLabelText}'");
    }

    public void UpdateProgressNormalized(float normalizedValue, string customProgressText = null, string customLabelText = null)
    {
        float targetProgress = Mathf.Clamp01(normalizedValue);

        if (progressText != null && !string.IsNullOrEmpty(customProgressText))
            progressText.text = customProgressText;

        if (labelText != null && !string.IsNullOrEmpty(customLabelText))
            labelText.text = customLabelText;

        if (animateOnUpdate && gameObject.activeInHierarchy)
            AnimateToProgress(targetProgress);
        else
        {
            if (fillImage != null) fillImage.fillAmount = targetProgress;
            currentFillAmount = targetProgress;
        }

        Debug.Log($"[ProgressBarManager] {gameObject.name} atualizado: {normalizedValue:F2} - ProgressText: '{customProgressText}', LabelText: '{customLabelText}'");
    }

    // -------------------------------------------------------
    // Helpers de texto
    // -------------------------------------------------------
    public void UpdateProgressTextOnly(string text)
    {
        if (progressText != null) progressText.text = text;
    }

    public void UpdateLabelTextOnly(string text)
    {
        if (labelText != null) labelText.text = text;
    }

    public void UpdateBothTexts(string progressTextValue, string labelTextValue)
    {
        if (progressText != null) progressText.text = progressTextValue;
        if (labelText != null)    labelText.text    = labelTextValue;
    }

    // -------------------------------------------------------
    // Animação
    // -------------------------------------------------------
    private void AnimateToProgress(float targetProgress)
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(AnimateProgressCoroutine(targetProgress));
    }

    private IEnumerator AnimateProgressCoroutine(float targetProgress)
    {
        float startProgress = currentFillAmount;
        float elapsedTime   = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime   += Time.deltaTime;
            float t        = Mathf.Clamp01(elapsedTime / animationDuration);
            currentFillAmount = Mathf.Lerp(startProgress, targetProgress, animationCurve.Evaluate(t));

            if (fillImage != null)
                fillImage.fillAmount = currentFillAmount;

            yield return null;
        }

        if (fillImage != null)
            fillImage.fillAmount = targetProgress;

        currentFillAmount  = targetProgress;
        animationCoroutine = null;
    }

    // -------------------------------------------------------
    // Imediato / Reset
    // -------------------------------------------------------
    public void SetProgressImmediate(int current, int total, string customProgressText = null, string customLabelText = null)
    {
        bool prev = animateOnUpdate;
        animateOnUpdate = false;
        UpdateProgress(current, total, customProgressText, customLabelText);
        animateOnUpdate = prev;
    }

    public void SetProgressImmediatePercentage(float percentage, string customProgressText = null, string customLabelText = null)
    {
        bool prev = animateOnUpdate;
        animateOnUpdate = false;
        UpdateProgressPercentage(percentage, customProgressText, customLabelText);
        animateOnUpdate = prev;
    }

    public void ResetProgress()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }

        if (fillImage != null) fillImage.fillAmount = 0f;
        currentFillAmount = 0f;
        if (progressText != null) progressText.text = "";
        if (labelText    != null) labelText.text    = "";

        Debug.Log($"[ProgressBarManager] {gameObject.name} resetado");
    }

    public float GetCurrentProgress() => currentFillAmount;
    public float GetCurrentProgressPercentage() => currentFillAmount * 100f;
    public void  SetAnimationEnabled(bool enabled) => animateOnUpdate = enabled;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (animationDuration < 0.1f) animationDuration = 0.1f;
    }
#endif
}