using UnityEngine;

public class AutoPingPong : MonoBehaviour
{
    public float moveRange = 3.0f; // 左右に動く幅
    public float speed = 2.0f;     // 往復するスピード

    private Vector3 startPosition; // 最初の位置

    void Start()
    {
        // ゲーム開始時の初期位置を記憶する
        startPosition = transform.position;
    }

    void Update()
    {
        // 時間経過に合わせて -1.0 〜 1.0 の間で値が変化する
        float wave = Mathf.Sin(Time.time * speed);

        // 初期位置を基準に、X座標を左右に変化させる
        transform.position = startPosition + new Vector3(wave * moveRange, 0, 0);
    }
}
