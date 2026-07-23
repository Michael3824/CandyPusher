using Unity.VisualScripting;
using UnityEngine;


public class AudioManager : MonoBehaviour
{ 
static public AudioManager instance;

void Awake()
{
        //instanceに何もなければ
        if (instance == null)
    {
        //instanceにthisを登録する
        instance = this;
    } 
        //そうでなければ
        else
    {
        //thisをゲームをオブジェクトから消去する(?)
        Destroy(this.gameObject);
    }
}





    public AudioClip[] audioClips;
    public AudioSource seAudioSource;

    public void PlaySE()
    {
        seAudioSource.clip = audioClips[1];
        seAudioSource.Play();
    }

    public AudioClip[] bgmAudioClips;
    public AudioSource bgmAudioSource;

    public void PlayBGM()
    {
        bgmAudioSource.clip = bgmAudioClips[0];
        bgmAudioSource.Play();
    }
    void Start()
    {
        seAudioSource = this.gameObject.AddComponent<AudioSource>();
        bgmAudioSource = this.gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true; 
        
        PlayBGM ();


    }
}

