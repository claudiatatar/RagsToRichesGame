using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{
    public string hallOfMirrorsSceneName = "HallOfMirrors";
    public Animator gateAnimator;
    public GameObject lockedPrompt;

    private bool isUnlocked = false;

    public void UnlockGate()
    {
        isUnlocked = true;
        if (gateAnimator != null) gateAnimator.SetTrigger("Open");
        if (lockedPrompt != null) lockedPrompt.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isUnlocked)
                SceneManager.LoadScene(hallOfMirrorsSceneName);
            else if (lockedPrompt != null)
                lockedPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && lockedPrompt != null)
            lockedPrompt.SetActive(false);
    }
}
