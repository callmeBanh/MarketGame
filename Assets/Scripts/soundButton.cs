using UnityEngine;
using UnityEngine.UI;

public class soundButton : MonoBehaviour
{
    public Image icon;
    public Sprite soundOn;
    public Sprite soundOff;

    void Start()
    {
        UpdateIcon();
    }

    public void OnSoundButtonPressed()
    {
        sound.instance.ToggleSound();
        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (sound.instance.isMuted())
        {
            icon.sprite = soundOff;
        }
        else
        {
            icon.sprite = soundOn;
        }
    }
}
