using UnityEngine;
using UnityEngine.UI;

public class StageOut : MonoBehaviour

{
    private int Score;
    public Text scoreText;
    public TMPro.TextMeshProUGUI scoreTextTMP;
    
    void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.PlaySE();
        //Score = Score + 1;
        Score += 1;
        Debug.Log($"Score:{Score}");
        scoreTextTMP.text = $"Score:{Score}";
        //変数名 otherってなに？
        //A.すり抜けた相手のコライダー情報
        Debug.Log($"{other.name}がすり抜けました。");
        //Destory関数
        //Destory(破棄したいオブジェクト)
        //オブジェクトが使用しているメモリの開放(ガベージコレクション)と描画情報の破棄
        Destroy(other.gameObject);

 
        //スコアが10より大きくなった時に発動する
        if (Score > 10)
        {
            // != (右辺と左辺の値が同じでなかったら)
            if (AudioManager.instance.bgmAudioSource.clip != AudioManager.instance.bgmAudioClips[1])
            
            AudioManager.instance.bgmAudioSource.clip = AudioManager.instance.bgmAudioClips[1];
            AudioManager.instance.bgmAudioSource.Play();
        }
    }
}
