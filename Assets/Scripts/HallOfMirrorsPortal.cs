using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HallOfMirrorsPortal : MonoBehaviour
{
    public string sceneToLoad = "HallOfMirrors";
    public TextMeshProUGUI completionMessage;
    public TextMeshProUGUI notReadyMessage;

    [Header("Portal Visuals")]
    public GameObject darkSparks;
    public GameObject sparks;
    public GameObject sides;
    public GameObject circleSparks;

    [Header("Colors")]
    public Renderer[] portalRenderers;
    public Color notReadyColor = Color.red;
    private Color[] originalColors;

    [Header("Timing")]
    public float messageDuration = 2f;
    public float fadeDuration = 1f;

    private bool allCollected = false;

    // Not ready message state
    private float notReadyMessageTimer = 0f;
    private float notReadyFadeTimer = 0f;
    private bool notReadyFadingIn = false;
    private bool notReadyFadingOut = false;

    // Completion message state
    private float completionFadeTimer = 0f;
    private bool completionFadingIn = false;

    void Start()
    {
        if (darkSparks != null) darkSparks.SetActive(false);
        if (sparks != null) sparks.SetActive(false);
        if (sides != null) sides.SetActive(false);
        if (circleSparks != null) circleSparks.SetActive(false);

        if (completionMessage != null)
        {
            completionMessage.gameObject.SetActive(false);
            SetAlpha(completionMessage, 0f);
        }
        if (notReadyMessage != null)
        {
            notReadyMessage.gameObject.SetActive(false);
            SetAlpha(notReadyMessage, 0f);
        }

        originalColors = new Color[portalRenderers.Length];
        for (int i = 0; i < portalRenderers.Length; i++)
            originalColors[i] = portalRenderers[i].material.color;

        if (QuestManager.Instance != null)
            QuestManager.Instance.onAllItemsCollected.AddListener(OnAllCollected);
    }

    void Update()
    {
        // --- Completion message fade in ---
        if (completionFadingIn)
        {
            completionFadeTimer += Time.deltaTime;
            float alpha = Mathf.Clamp01(completionFadeTimer / fadeDuration);
            SetAlpha(completionMessage, alpha);
            if (completionFadeTimer >= fadeDuration)
                completionFadingIn = false;
        }

        // --- Not ready message fade in ---
        if (notReadyFadingIn)
        {
            notReadyFadeTimer += Time.deltaTime;
            float alpha = Mathf.Clamp01(notReadyFadeTimer / fadeDuration);
            SetAlpha(notReadyMessage, alpha);
            if (notReadyFadeTimer >= fadeDuration)
            {
                notReadyFadingIn = false;
                notReadyMessageTimer = messageDuration;
            }
        }

        // --- Not ready message hold then fade out ---
        if (notReadyMessageTimer > 0f && !notReadyFadingIn)
        {
            notReadyMessageTimer -= Time.deltaTime;
            if (notReadyMessageTimer <= 0f)
            {
                notReadyFadingOut = true;
                notReadyFadeTimer = fadeDuration;
                ResetColors();
            }
        }

        // --- Not ready message fade out ---
        if (notReadyFadingOut)
        {
            notReadyFadeTimer -= Time.deltaTime;
            float alpha = Mathf.Clamp01(notReadyFadeTimer / fadeDuration);
            SetAlpha(notReadyMessage, alpha);
            if (notReadyFadeTimer <= 0f)
            {
                notReadyFadingOut = false;
                if (notReadyMessage != null)
                    notReadyMessage.gameObject.SetActive(false);
            }
        }
    }

    void OnAllCollected()
    {
        allCollected = true;
        if (darkSparks != null) darkSparks.SetActive(true);
        if (sparks != null) sparks.SetActive(true);
        if (sides != null) sides.SetActive(true);
        if (circleSparks != null) circleSparks.SetActive(true);

        if (completionMessage != null)
        {
            completionMessage.gameObject.SetActive(true);
            SetAlpha(completionMessage, 0f);
            completionFadeTimer = 0f;
            completionFadingIn = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (allCollected)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            if (notReadyMessage != null)
            {
                notReadyMessage.gameObject.SetActive(true);
                SetAlpha(notReadyMessage, 0f);
                notReadyFadeTimer = 0f;
                notReadyFadingIn = true;
                notReadyFadingOut = false;
                notReadyMessageTimer = 0f;
            }
            SetAllColors(notReadyColor);
        }
    }

    void SetAlpha(TextMeshProUGUI text, float alpha)
    {
        if (text == null) return;
        Color c = text.color;
        c.a = alpha;
        text.color = c;
    }

    void SetAllColors(Color color)
    {
        foreach (Renderer r in portalRenderers)
            if (r != null) r.material.color = color;
    }

    void ResetColors()
    {
        for (int i = 0; i < portalRenderers.Length; i++)
            if (portalRenderers[i] != null)
                portalRenderers[i].material.color = originalColors[i];
    }

    void OnDestroy()
    {
        if (QuestManager.Instance != null)
            QuestManager.Instance.onAllItemsCollected.RemoveListener(OnAllCollected);
    }
}