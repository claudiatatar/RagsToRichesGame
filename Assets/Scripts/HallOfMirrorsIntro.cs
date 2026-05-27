using UnityEngine;
using TMPro;

public class HallOfMirrorsIntro : MonoBehaviour
{
    public TextMeshProUGUI introMessage;
    public float fadeDuration = 2f;
    public float holdDuration = 5f;

    private float fadeInTimer = 0f;
    private float holdTimer = 0f;
    private float fadeOutTimer = 0f;
    private enum State { FadingIn, Holding, FadingOut, Done }
    private State state = State.FadingIn;

    void Start()
    {
        if (introMessage != null)
        {
            introMessage.gameObject.SetActive(true);
            SetAlpha(0f);
        }
    }

    void Update()
    {
        if (introMessage == null) return;

        if (state == State.FadingIn)
        {
            fadeInTimer += Time.deltaTime;
            SetAlpha(Mathf.Clamp01(fadeInTimer / fadeDuration));
            if (fadeInTimer >= fadeDuration)
            {
                state = State.Holding;
                holdTimer = holdDuration;
            }
        }
        else if (state == State.Holding)
        {
            holdTimer -= Time.deltaTime;
            if (holdTimer <= 0f)
            {
                state = State.FadingOut;
                fadeOutTimer = fadeDuration;
            }
        }
        else if (state == State.FadingOut)
        {
            fadeOutTimer -= Time.deltaTime;
            SetAlpha(Mathf.Clamp01(fadeOutTimer / fadeDuration));
            if (fadeOutTimer <= 0f)
            {
                state = State.Done;
                introMessage.gameObject.SetActive(false);
            }
        }
    }

    void SetAlpha(float alpha)
    {
        Color c = introMessage.color;
        c.a = alpha;
        introMessage.color = c;
    }
}