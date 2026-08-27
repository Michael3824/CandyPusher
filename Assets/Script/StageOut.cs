using UnityEngine;
using UnityEngine.UI;

public class StageOut : MonoBehaviour
{
    private int Score;
    public Text scoreText;
    public TMPro.TextMeshProUGUI scoreTextTMP;

    void OnTriggerEnter(Collider other)
    {
        // 🌟【重要】落ちてきたオブジェクトが「Candy」タグを持っている時だけ処理する
        if (other.gameObject.CompareTag("Candy"))
        {
            // スロットマネージャーを見つけて、キャンディーが落ちたよ！と伝える
            SlotManager slot = FindFirstObjectByType<SlotManager>();
            if (slot != null)
            {
                slot.OnCandyDrop();
            }
        }

        AudioManager.instance.PlaySE();
        Score += 1;
        Debug.Log($"Score:{Score}");
        scoreTextTMP.text = $"Score:{Score}";

        Debug.Log($"{other.name}がすり抜けました。");
        Destroy(other.gameObject);

        // スコアが10より大きくなった時に発動する
        if (Score > 10)
        {
            // 🌟エラー防止：bgmAudioClipsの箱が2つ以上（0番目と1番目）あるかチェックする安全ガード
            if (AudioManager.instance.bgmAudioClips != null && AudioManager.instance.bgmAudioClips.Length > 1)
            {
                if (AudioManager.instance.bgmAudioSource.clip != AudioManager.instance.bgmAudioClips[1])
                {
                    AudioManager.instance.bgmAudioSource.clip = AudioManager.instance.bgmAudioClips[1];
                    AudioManager.instance.bgmAudioSource.Play();
                }
            }
            else
            {
                Debug.LogWarning("AudioManagerのbgmAudioClipsに2曲目が登録されていないため、BGMの切り替えをスキップしました。");
            }
        }
    }
}
