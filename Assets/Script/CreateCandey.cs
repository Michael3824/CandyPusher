using UnityEngine;
//InputSystemを使用するのでusing UnityEngine.InputSystemを追加
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //スペースが押されたら、Candyprefabを生成する
    //1.スペースキーが押された時の判定
    //2.CandyPrefabを生成する

    //生成したいオブジェクトを変数として定義
    [SerializeField]
    
    private GameObject candyPrefab;

    //スペースが押された時の判定
    void Update()
    {
        //もしも接続状態のキーボードのスペースキーが押されたら
            //デバイス：keyboard => キーボードに関する処理を呼び出す
            //デバイスの状態：current => 現在接続状態のキーボードを取得する
            //デバイスの欲しいキーの情報：spaceKey => スペースキーの情報を取得する
            //キーの状態：wasPressedthisFrame => 押された瞬間かどうかの判定
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            //オブジェクトを生成する処理
            Debug.Log("スペースが押された");
            //Instantiate => オブジェクトを実体化する関数
            //Instantiate(生成したいオブジェクト);
            //変数InstantiatedCandyを定義　初期値に精製したオブジェクトに設定
            GameObject instaniatedCandy = Instantiate(candyPrefab);
            instaniatedCandy.transform.position = this.transform.position;

        }
    }
}
