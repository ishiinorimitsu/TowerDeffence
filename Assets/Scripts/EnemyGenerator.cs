using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyControllerPrefab;

    [SerializeField]
    private PathData pathData;

    private GamaManager gameManager;

    //[SerializeField]
    //public bool isEnemyGenerate;  //生成するswitch（trueだと生成する　falseだと生成停止）

    //public int generateIntervalTime;    //何秒おきに作るか

    //public int generateEnemyCount;  //生成した敵の数を数える

    //public int maxEnemyCount;   //生成する敵の最大数

    // Start is called before the first frame update
    //void Start()
    //{
    //    isEnemyGenerate = true;  //生成の許可

    //    StartCoroutine(PreparateEnemyGenerate());
    //}

    public IEnumerator PreparateEnemyGenerate(GamaManager gameManager)
    {
        this.gameManager = gameManager;

        int timer = 0;  //生成用のタイマーを用意

        while (gameManager.isEnemyGenerate)
        {
            timer++;

            if(timer > gameManager.generateIntervalTime)
            {
                timer = 0;

                GenerateEnemy();

                gameManager.AddEnemyList();  //敵の生成数のカウントアップとリストへの追加

                gameManager.JudgeGenerateEnemyEnd();

                //generateEnemyCount++;

                //if(generateEnemyCount >= maxEnemyCount)
                //{
                //    isEnemyGenerate = false;
                //}
            }
            yield return null;
        }
    }

    public void GenerateEnemy()
    {
        EnemyController enemyController = Instantiate(enemyControllerPrefab, pathData.generateTran.position, Quaternion.identity);
    }
}
