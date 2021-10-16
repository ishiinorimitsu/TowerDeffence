using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour   //敵の生成をつかさどる
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private CharaGenerator charaGenerator;

    [SerializeField]
    private UIManager uiManager;

    public bool isEnemyGenerate;  //生成するswitch（trueだと生成する　falseだと生成停止）

    public int generateIntervalTime;    //何秒おきに作るか

    public int generateEnemyCount;  //生成した敵の数を数える

    public int maxEnemyCount;   //生成する敵の最大数

    public enum GameState
    {
        Preparate,      //ゲーム開始前、準備中
        Play,      //ゲームプレイ中
        Stop,      //ゲームを一時中断
        GameUp      //ゲームの終了（クリア、ゲームオーバー両方）
    }

    public List<EnemyController> enemiesList = new List<EnemyController>();

    private int destroyEnemyCount;

    public GameState currentGameState;

    // Start is called before the first frame update
    void Start()
    {
        //ゲームの進行状態を準備中に設定
        SetGameState(GameState.Preparate);

        StartCoroutine(charaGenerator.SetUpCharaGenerter(this));

        isEnemyGenerate = true;  //生成の許可

        // ゲームの進行状態をプレイ中に変更
        SetGameState(GameState.Play);

        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));

        StartCoroutine(TimeToCurrency());
    }



    public void AddEnemyList(EnemyController enemy)
    {
        enemiesList.Add(enemy);

        //敵の数をカウント
        generateEnemyCount++;
    }

    public void JudgeGenerateEnemyEnd()
    {
        if(generateEnemyCount >= maxEnemyCount)
        {
            isEnemyGenerate = false;
        }
    }

    public void SetGameState(GameState nextGameState)
    {
        currentGameState = nextGameState;
    }

    public void PauseEnemies()    //敵を止める
    {
        for(int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].PauseMove();
        }
    }

    public void ResumeEnemies()     //敵を再び動かし始める
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].ResumeMove();
        }
    }

    public void RemoveEnemyList(EnemyController removeEnemy)
    {
        enemiesList.Remove(removeEnemy);
    }

    public void CountUpDestroyEnemyCount(EnemyController enemyController)
    {
        RemoveEnemyList(enemyController);   //Listから破壊された敵を削除する

        destroyEnemyCount++;

        Debug.Log("破壊した敵の数："+ destroyEnemyCount);   

        JudgeGameClear();   //ゲームをクリアしているか確かめる
    }

    private void JudgeGameClear()
    {
        if(destroyEnemyCount >= maxEnemyCount)
        {
            Debug.Log("ゲームクリア");
        }
    }

    public IEnumerator TimeToCurrency()
    {
        int timer = 0;
        while(currentGameState == GameState.Play)
        {
            timer++;

            Debug.Log("OK1");

            Debug.Log(timer);

            Debug.Log(GameData.instance.currencyIntervalTime);

            Debug.Log(GameData.instance.currency);

            Debug.Log(GameData.instance.maxCurrency);

            Debug.Log("aed");

            if (timer >= GameData.instance.currencyIntervalTime && GameData.instance.currency < GameData.instance.maxCurrency)  //一定時間経過ごと＆カレンシーが最大値でなければ
            {

                Debug.Log("OK2");

                timer = 0;

                //カレンシーの値を指定した範囲内でaddCurrencyPointずつ増やしていく。
                GameData.instance.currency = Mathf.Clamp(GameData.instance.currency += GameData.instance.addCurrencyPoint, 0, GameData.instance.maxCurrency);

                uiManager.UpdateDisplayCurrency();
            }
            else
            {
                Debug.Log("ダメ");
            }
            yield return null;
        }
    }
}
