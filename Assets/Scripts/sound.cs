using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class sound : MonoBehaviour
{
    public static sound instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleSound()
    {
        if(audioSource != null)
        {
            audioSource.mute = !audioSource.mute;
        }
    }

    public bool isMuted()
    {
        return audioSource != null && audioSource.mute;
    }
}
