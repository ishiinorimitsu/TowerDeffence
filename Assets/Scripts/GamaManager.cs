using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour   //敵の生成をつかさどる
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private CharaGenerator charaGenerator;

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

    public void RemveEnemies(EnemyController removeEnemy)
    {
        enemiesList.Remove(removeEnemy);
    }
}
