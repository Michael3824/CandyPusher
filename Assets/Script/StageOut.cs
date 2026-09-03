using UnityEngine;
using UnityEngine.UI;

public class StageOut : MonoBehaviour
{
    // SlotManagerからアクセスできるように public に変更
    public int Score;
    public Text scoreText;
    public TMPro.TextMeshProUGUI scoreTextTMP;

    void OnTriggerEnter(Collider other)
    {
        // 落ちてきたオブジェクトが「Candy」タグを持っている時だけ処理する
        if (other.gameObject.CompareTag("Candy"))
        {
            SlotManager slot = FindFirstObjectByType<SlotManager>();
            if (slot != null)
            {
                slot.OnCandyDrop();
            }
        }

        // 通常のメダル落下時のスコア処理（+1）
        AudioManager.instance.PlaySE();
        AddScoreFromSlot(1);

        Debug.Log($"{other.name}がすり抜けました.");
        Destroy(other.gameObject);
    }

    // 🌟【新要素】スロットや通常落下からスコアを安全に増やすための公開関数
    public void AddScoreFromSlot(int amount)
    {
        Score += amount;
        scoreTextTMP.text = $"Score:{Score}";

        // スコアが1000など一気に増えたときも判定が通るように「> 10」でチェック
        if (Score > 10)
        {
            if (AudioManager.instance.bgmAudioClips != null && AudioManager.instance.bgmAudioClips.Length > 1)
            {
                if (AudioManager.instance.bgmAudioSource.clip != AudioManager.instance.bgmAudioClips[1])
                {
                    AudioManager.instance.bgmAudioSource.clip = AudioManager.instance.bgmAudioClips[1];
                    AudioManager.instance.bgmAudioSource.Play();
                }
            }
        }
    }
}
