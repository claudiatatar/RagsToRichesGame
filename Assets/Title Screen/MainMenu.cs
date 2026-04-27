using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private FirstPersonLook firstPersonLook;

    void Start()
    {
        // Free cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;

        // Freeze camera
        firstPersonLook = Camera.main.GetComponent<FirstPersonLook>();
        if (firstPersonLook != null)
            firstPersonLook.enabled = false;
    }

    public void Play()
    {
        // Unfreeze camera before loading
        if (firstPersonLook != null)
            firstPersonLook.enabled = true;

        SceneManager.LoadScene("RTRGame");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
