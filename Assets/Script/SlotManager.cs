using System.Collections;
using UnityEngine;
using TMPro; // 使用 TextMeshPro

public class SlotManager : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI centerText;
    public TextMeshProUGUI rightText;

    // インスペクターで CounterText を紐付けるための枠
    public TextMeshProUGUI counterText;

    // スロットの図柄（4, 5, 6, 7）
    private string[] slotSymbols = { "4", "5", "6", "7" };

    private bool isSpinning = false;
    private float slotTimer = 0f;

    // スロット起動に必要なコインのカウンター（20個）
    private int coinCount = 0;

    // 内部的に事前に決定された図柄を保存する箱
    private string finalLeft;
    private string finalCenter;
    private string finalRight;

    void Start()
    {
        // ゲーム開始時にカウンターの表示を初期化
        UpdateCounterUI();
    }

    void Update()
    {
        // スロットが回っていない時は何もしない
        if (!isSpinning) return;

        // ゲームの時間（Time.timeScale=0）が止まっていても進む、演出専用の時間（秒）を加算
        slotTimer += Time.unscaledDeltaTime;

        // 【左リール】：2秒未満の時は元の超高速シャッフル、2秒でピタッと止まる
        if (slotTimer < 2f)
        {
            leftText.text = slotSymbols[Random.Range(0, slotSymbols.Length)];
        }
        else
        {
            leftText.text = finalLeft;
        }

        // 【中央リール】：3秒未満の時は元の超高速シャッフル、3秒でピタッと止まる
        if (slotTimer < 3f)
        {
            centerText.text = slotSymbols[Random.Range(0, slotSymbols.Length)];
        }
        else
        {
            centerText.text = finalCenter;
        }

        // 【右リール】：4秒未満の時は元の超高速シャッフル、4秒でピタッと止まる
        if (slotTimer < 4f)
        {
            rightText.text = slotSymbols[Random.Range(0, slotSymbols.Length)];
        }
        else
        {
            rightText.text = finalRight;

            // 4秒経過したのでリールの回転処理を終了
            isSpinning = false;

            // 揃ったかどうかの判定ログを出す
            CheckResult();

            // 1秒待ってからゲームを再開させ、同時にUIをリセットするコルーチンを起動
            StartCoroutine(WaitAndResumeGame());
        }
    }

    // コイン回収時にStageOutから呼び出される関数
    public void OnCandyDrop()
    {
        // すでにスロットが回っている最中、または終了直後のウェイト中ならカウントしない
        if (isSpinning || Time.timeScale == 0f) return;

        // コインのカウントを1増やす
        coinCount += 1;
        Debug.Log("Coin Collected: " + coinCount + " / 20");

        // カウンターの画面表示を更新する
        UpdateCounterUI();

        // 20個溜まったかどうかの条件分岐
        if (coinCount >= 20)
        {
            StartSlot();   // スロットを起動
        }
    }

    // 画面上のカウンターテキストを書き換える専用の関数
    private void UpdateCounterUI()
    {
        if (counterText != null)
        {
            counterText.text = coinCount + " / 20";
        }
    }

    // スロットの実際の開始処理
    public void StartSlot()
    {
        isSpinning = true;
        slotTimer = 0f;

        // 【時間停止】ゲーム全体の物理や動きをストップさせる
        Time.timeScale = 0f;
        Debug.Log("Game Paused for Slot Effect (Time Scale = 0)");

        // 【確率計算】0〜99の計100個の数字からアタリを分岐する
        int percent = Random.Range(0, 100);

        // ドパガキ（バカ勝ち）用の超ガバガバ確率設定
        if (percent < 10) // 0〜9 (10%の確率)
        {
            finalLeft = "7"; finalCenter = "7"; finalRight = "7";
            Debug.Log("[Internal Result] 777 JACKPOT! (10%)");
        }
        else if (percent < 10 + 20) // 10〜29 (20%の確率)
        {
            finalLeft = "6"; finalCenter = "6"; finalRight = "6";
            Debug.Log("[Internal Result] 666 Win! (20%)");
        }
        else if (percent < 10 + 20 + 25) // 30〜54 (25%の確率)
        {
            finalLeft = "5"; finalCenter = "5"; finalRight = "5";
            Debug.Log("[Internal Result] 555 Win! (25%)");
        }
        else if (percent < 10 + 20 + 25 + 30) // 55〜84 (30%の確率)
        {
            finalLeft = "4"; finalCenter = "4"; finalRight = "4";
            Debug.Log("[Internal Result] 444 Win! (30%)");
        }
        else // 85〜99 (残りわずか15%の確率)
        {
            // ハズレ（絶対にゾロ目にならないようにする）
            finalLeft = slotSymbols[Random.Range(0, slotSymbols.Length)];
            finalCenter = slotSymbols[Random.Range(0, slotSymbols.Length)];
            finalRight = slotSymbols[Random.Range(0, slotSymbols.Length)];

            // もし偶然ゾロ目になってしまった場合の安全ガード
            if (finalLeft == finalCenter && finalCenter == finalRight)
            {
                finalRight = (finalLeft == "7") ? "4" : "7";
            }
            Debug.Log("[Internal Result] MISS (15%)");
        }
    }

    // スロットが揃った瞬間にログを出す関数
    void CheckResult()
    {
        if (finalLeft == finalCenter && finalCenter == finalRight)
        {
            Debug.Log("JACKPOT! " + finalLeft + finalCenter + finalRight + " matched!");
        }
        else
        {
            Debug.Log("MISS");
        }
    }

    // 右リール停止後、1秒待ってからゲームを再開させ、同時に各種UIをリセット・非表示にするコルーチン
    private IEnumerator WaitAndResumeGame()
    {
        // 全リールが停止した状態で正確に1秒間待つ（アタリの数字を確認する時間）
        yield return new WaitForSecondsRealtime(1f);

        // 1秒経ったので、内部数値を0にし、画面表示を「0 / 20」にリセットする
        coinCount = 0;
        UpdateCounterUI();

        // 🌟【新要素】ゲームが動き出すのと「完全に同時」に、3つの数字テキストを空っぽにして非表示にする
        leftText.text = "";
        centerText.text = "";
        rightText.text = "";
        Debug.Log("Slot UI texts hidden immediately as game resumes.");

        // 【時間再開】ゲームの時間を元の速さ（1倍速）に戻す（プッシャーが動き出す！）
        Time.timeScale = 1f;
        Debug.Log("Game Resumed (Time Scale = 1)");

        // 配当スコアに基づいて加算量を決定
        int scoreToAdd = 0;
        if (finalLeft == finalCenter && finalCenter == finalRight)
        {
            if (finalLeft == "7") scoreToAdd = 1000;
            else if (finalLeft == "6") scoreToAdd = 200;
            else if (finalLeft == "5") scoreToAdd = 100;
            else if (finalLeft == "4") scoreToAdd = 50;
        }

        if (scoreToAdd > 0)
        {
            StageOut stageOut = FindFirstObjectByType<StageOut>();
            if (stageOut != null)
            {
                // ゲーム進行・数字が消えるのと同時に、裏で高速加算を開始
                StartCoroutine(AddScoreAnimation(stageOut, scoreToAdd));
            }
        }
    }

    // スコアを高速で1ずつ足していき、UIテキストもリアルタイムに更新する演出アニメーション
    private IEnumerator AddScoreAnimation(StageOut stageOut, int totalAmount)
    {
        for (int i = 0; i < totalAmount; i++)
        {
            stageOut.AddScoreFromSlot(1);

            // 高速加算スピード（0.005秒ウェイト）
            yield return new WaitForSeconds(0.005f);
        }
    }
}
