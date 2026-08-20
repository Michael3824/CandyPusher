using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // シーンを切り替えてもAudioManagerが消えないようにする場合
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // gameObject自体を削除するのが正解です
        }
    }

    public AudioClip[] audioClips;
    public AudioSource seAudioSource;

    public void PlaySE()
    {
        // SE配列に要素があるかチェック
        if (audioClips != null && audioClips.Length > 1)
        {
            seAudioSource.clip = audioClips[1];
            seAudioSource.Play();
        }
    }

    public AudioClip[] bgmAudioClips;
    public AudioSource bgmAudioSource;

    public void PlayBGM()
    {
        // 【修正点】BGM配列に要素が登録されているかチェックする
        if (bgmAudioClips != null && bgmAudioClips.Length > 0)
        {
            bgmAudioSource.clip = bgmAudioClips[0];
            bgmAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("BGMが登録されていません！");
        }
    }

    void Start()
    {
        // 既にインスペクターでAudioSourceを付けている場合はAddComponentは不要です
        if (seAudioSource == null) seAudioSource = gameObject.AddComponent<AudioSource>();
        if (bgmAudioSource == null) bgmAudioSource = gameObject.AddComponent<AudioSource>();

        bgmAudioSource.loop = true;

        PlayBGM();
    }
}
