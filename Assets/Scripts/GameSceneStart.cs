// GameSceneStart.cs
using UnityEngine;

public class GameSceneStart : MonoBehaviour
{
    void Start()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayGameMusic();
    }
}