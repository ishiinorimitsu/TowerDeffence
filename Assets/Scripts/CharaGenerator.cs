using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharaGenerator : MonoBehaviour
{
    [SerializeField]
    private CharaController charaControllerPrefab;

    [SerializeField]
    private Grid grid;   //タイルマップの座標を取得するためのモノ。Grid＿BaseがわのGrid を指定する。

    [SerializeField]
    private Tilemap tileMap;

    [SerializeField]
    private PlacemantCharaSelectPopUp placementCharaSelectPopUpPrefab;　　　//　PlacementCharaSelectPopUp プレファブゲームオブジェクトをアサイン用

    [SerializeField]
    private Transform canvasTran;　　　　　　　　　　　　　　　　　　　　　 //　PlacementCharaSelectPopUp ゲームオブジェクトの生成位置の登録用

    [SerializeField, Header("キャラのデータリスト")]
    private List<CharaData> charaDataList = new List<CharaData>();　　　　　//

    private PlacemantCharaSelectPopUp placementCharaSelectPopUp;　　　　　　//　生成された PlacementCharaSelectPopUp ゲームオブジェクトを代入するための変数

    private GamaManager gameManager;


    private Vector3Int gridPos;  //タイルマップのタイルのセル座標の保持用

    // Update is called once per frame
    void Update()
    {
        //もしすでにキャラの数が最大数に達しているときは、何もせずに処理を終える（return）
        if(gameManager.GetPlacementCharaCount() == GameData.instance.maxCharaPlacementCount)
        {
            Debug.Log("OK0");
            return;
        }

        Debug.Log("OKa");
        //マウスが押されてかつ配置キャラのポップアップが非表示状態ならかつゲームの状態がPlayのときだけ
        if (Input.GetMouseButtonDown(0) && !placementCharaSelectPopUp.gameObject.activeSelf && gameManager.currentGameState == GamaManager.GameState.Play)
        {
            Debug.Log("OKb");
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));  //マウスがある場所を取得し、それをタイルマップでいうとどこにあたるかの情報をgridPosに代入する。

            Debug.Log("OKc");
            if (tileMap.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                Debug.Log("OKd");

                //配置キャラを選択するポップアップを表示する
                ActivatePlacementCharaSelectPopUp();
                Debug.Log("手順1");
            }
        }
    }

    //private void CreateChara(Vector3Int gridPos)
    //{

    //    // タップした位置にキャラを生成して配置
    //    GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);  //キャラを指定した場所に生成する

    //    // キャラの位置がタイルの左下を 0,0 として生成しているので、タイルの中央にくるように位置を調整
    //    chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);  //キャラをセルの中央に配置する

    //    CreateHaveCharaDatasList();
    //}


    public IEnumerator SetUpCharaGenerter(GamaManager gameManager)
    {
        this.gameManager = gameManager;

        CreateHaveCharaDatasList();

        yield return CreatePlacementCharaSetUpPopUp();
    }

    //配置キャラ選択用のポップアップの生成
    public IEnumerator CreatePlacementCharaSetUpPopUp()
    {
        placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);  //ポップアップを生成する

        placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this,charaDataList);  //ポップアップの設定

        placementCharaSelectPopUp.gameObject.SetActive(false);　//ポップアップを非表示

        yield return null;
    }
    public void ActivatePlacementCharaSelectPopUp()    //キャラを配置するポップアップを開いた時の処理
    {
        gameManager.SetGameState(GamaManager.GameState.Stop);       //ゲームの進行状態をゲーム停止に変更

        gameManager.PauseEnemies();     //全ての敵の移動を一時停止

        placementCharaSelectPopUp.gameObject.SetActive(true); //ポップアップを表示

        placementCharaSelectPopUp.ShowPopUp();
    }

    public void InActivePlacementSelectPopUp()   //キャラを配置するポップアップを閉じたときの処理
    {
        //配置キャラ選択用のポップアップを非表示にする
        placementCharaSelectPopUp.gameObject.SetActive(false);

        if(gameManager.currentGameState == GamaManager.GameState.Stop)      //この停止がクリアやゲームオーバーではない場合
        {
            //ゲームの状態をPlayにしてゲーム再開
            gameManager.SetGameState(GamaManager.GameState.Play);

            //敵を再び動かし始める
            gameManager.ResumeEnemies();

            StartCoroutine(gameManager.TimeToCurrency());   //カレンシーの加算処理を再開
        }
    }

    private void CreateHaveCharaDatasList()   //このメソッドでキャラのデータをこのスクリプト内に取り込んでいる。
    {
        //CharaDataSO内のCharaDataを一つずつリストに追加
        for(int i = 0; i < DataBaseManager.instance.charaDataSO.charaDataList.Count;i++)
        {
            charaDataList.Add(DataBaseManager.instance.charaDataSO.charaDataList[i]);
        }
    }

    public void CreateChooseChara(CharaData charaData)      //何のキャラを配置するかを選択したときに呼び出されるメソッド
    {
        //コストを支払う
        GameData.instance.currency -= charaData.cost;

        //カレンシーの画面表示を更新
        gameManager.uiManager.UpdateDisplayCurrency();

        //キャラをタップした位置に生成
        CharaController chara = Instantiate(charaControllerPrefab, gridPos, Quaternion.identity);

        //位置を中央に来るように設置
        chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);

        chara.SetUpChara(charaData, gameManager);

        Debug.Log(charaData.charaName);

        //キャラをリストに追加
        gameManager.AddCharaList(chara);
    }
}
