using UnityEngine;
using UnityEngine.UI;

public class StageOut : MonoBehaviour

{
    // 来週の頭(一限)に適当に選んだ３人を当てる予定
    // 要件定義：StageOutクラスの中に変数 Scoreを作成、オブジェクトがすり抜けたらScoreを1加算する
    // 1.StageOutクラスの中に変数　Scoreを作成
    private int Score;
    public Text scoreText;
    public TMPro.TextMeshProUGUI scoreTextTMP;
    // 2.変数 Score は int型　かつ　private であること
    // 3.オブジェクトがすり抜けたら(OutTriggerEnterが呼ばれたら)変数 Scoreに1加算する

    // 4.加算後の変数 ScoreをDebug.Logでコンソール上に出力する

    //このコードがアタッチされているオブジェクトのisTrigger(コライダー設定)が有効
    //かつ他のオブジェクトがすり抜けた時に中の処理を行うイベント関数
    public AudioManager audioManager; 

    
    void OnTriggerEnter(Collider other)
    {
        audioManager.PlaySE();
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

        if (Score > 10)
        {
            if (audioManager.bgmAudioSource.clip != audioManager.bgmAudioClips[1])
            
            audioManager.bgmAudioSource.clip = audioManager.bgmAudioClips[1];
            audioManager.bgmAudioSource.Play();
        }
    }
}