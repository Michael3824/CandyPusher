using System.Collections;
using UnityEngine;
using TMPro;

public class SlotManager : MonoBehaviour
{
    [Header("UI設定")]
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI centerText;
    public TextMeshProUGUI rightText;
    public TextMeshProUGUI counterText;

    [Header("ゲーム設定")]
    public int requiredCandies = 30;
    private int candyCount = 0;

    // 🌟 絵柄をすべて半角の英語・数字に統一（文字化けが絶対に起きません）
    private string[] slotSymbols = { "1", "3", "5", "7" };
    private bool isSpinning = false;
    private float slotTimer = 0f;
    private string currentLeft;
    private string currentCenter;
    private string currentRight;

    void Start()
    {
        UpdateCounterUI();

        // スタート時の初期表示も英語にします
        leftText.text = "7";
        centerText.text = "7";
        rightText.text = "7";
    }

    public void OnCandyDrop()
    {
        if (isSpinning) return;

        candyCount++;
        UpdateCounterUI();

        if (candyCount >= requiredCandies)
        {
            candyCount = 0;
            StartSlot();
        }
    }

    void UpdateCounterUI()
    {
        // 🌟 カウント表示の日本語を「NEXT SLOT」に英語化
        counterText.text = "NEXT SLOT: " + remainingText();
    }

    string remainingText()
    {
        return (requiredCandies - candyCount).ToString();
    }

    void Update()
    {
        if (!isSpinning) return;
        slotTimer += Time.deltaTime;

        if (slotTimer < 1f)
        {
            currentLeft = slotSymbols[Random.Range(0, slotSymbols.Length)];
            leftText.text = currentLeft;
            currentCenter = slotSymbols[Random.Range(0, slotSymbols.Length)];
            centerText.text = currentCenter;
            currentRight = slotSymbols[Random.Range(0, slotSymbols.Length)];
            rightText.text = currentRight;
        }
        else if (slotTimer >= 1f && slotTimer < 2f)
        {
            currentCenter = slotSymbols[Random.Range(0, slotSymbols.Length)];
            centerText.text = currentCenter;
            currentRight = slotSymbols[Random.Range(0, slotSymbols.Length)];
            rightText.text = currentRight;
        }
        else if (slotTimer >= 2f && slotTimer < 3f)
        {
            currentRight = slotSymbols[Random.Range(0, slotSymbols.Length)];
            rightText.text = currentRight;
        }
        else
        {
            isSpinning = false;
            CheckResult();
            UpdateCounterUI();
        }
    }

    void StartSlot()
    {
        isSpinning = true;
        slotTimer = 0f;
    }

    void CheckResult()
    {
        if (currentLeft == currentCenter && currentCenter == currentRight)
        {
            // コンソールログも分かりやすく変更
            Debug.Log("🎉 JACKPOT! " + currentLeft + " matched!");
        }
        else
        {
            Debug.Log("💀 MISS");
        }
    }
}
