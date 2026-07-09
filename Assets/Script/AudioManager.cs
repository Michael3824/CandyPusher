using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;

    public void PlaySE()
    {
        audioSource.Play();
    }

}

