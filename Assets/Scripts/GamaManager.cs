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

    // Start is called before the first frame update
    void Start()
    {
        isEnemyGenerate = true;  //生成の許可

        StartCoroutine(charaGenerator.SetUpCharaGenerter(this));

        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));
    }

    public void AddEnemyList()
    {
        generateEnemyCount++;
    }

    public void JudgeGenerateEnemyEnd()
    {
        if(generateEnemyCount >= maxEnemyCount)
        {
            isEnemyGenerate = false;
        }
    }
}
