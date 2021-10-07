using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private CharaGenerator charaGenerator;

    public bool isEnemyGenerate;  //��������switch�itrue���Ɛ�������@false���Ɛ�����~�j

    public int generateIntervalTime;    //���b�����ɍ�邩

    public int generateEnemyCount;  //���������G�̐��𐔂���

    public int maxEnemyCount;   //��������G�̍ő吔

    // Start is called before the first frame update
    void Start()
    {
        isEnemyGenerate = true;  //�����̋���

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
