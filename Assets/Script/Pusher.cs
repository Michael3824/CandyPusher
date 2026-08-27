using UnityEngine;

public class Pusher : MonoBehaviour
{
    public float speed = 0.1f;
    public float movepower = 0.5f;
    private Vector3 startPosition;

    // インスペクターでアタッチするか、Startで自動取得します
    public Rigidbody rb;

    void Start()
    {
        startPosition = this.transform.position;

        // もしインスペクターでRigidbodyを入れていない場合の保険
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        Debug.Log("ゲームが開始したよ");
    }

    // KinematicなRigidbodyを動かす場合は、UpdateではなくFixedUpdateを使います
    void FixedUpdate()
    {
        // 現在のSin波の計算をそのまま利用
        float z = Mathf.Sin(Time.time * speed) * movepower;

        // スタート位置を基準に、次のフレームの目標座標（位置）を計算
        Vector3 targetPosition = startPosition + new Vector3(0, 0, z);

        // 物理演算を維持したまま、目標座標へ強制的に移動（押し負けなくなります）
        rb.MovePosition(targetPosition);
    }
}
