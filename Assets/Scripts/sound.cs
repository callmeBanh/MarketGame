using UnityEngine;

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
            
            // Nếu quên chưa gắn AudioSource, tự động thêm vào để tránh lỗi Null
            if (audioSource == null) {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleSound()
    {
        if (audioSource != null)
        {
            audioSource.mute = !audioSource.mute;
        }
    }

    public bool isMuted()
    {
        return audioSource == null || audioSource.mute;
    }
}