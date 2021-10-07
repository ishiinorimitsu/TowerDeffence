using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private CharaGenerator charaGenerator;

    public bool isEnemyGenerate;  //¶¬‚·‚éswitchitrue‚¾‚Æ¶¬‚·‚é@false‚¾‚Æ¶¬’âŽ~j

    public int generateIntervalTime;    //‰½•b‚¨‚«‚Éì‚é‚©

    public int generateEnemyCount;  //¶¬‚µ‚½“G‚Ì”‚ð”‚¦‚é

    public int maxEnemyCount;   //¶¬‚·‚é“G‚ÌÅ‘å”

    // Start is called before the first frame update
    void Start()
    {
        isEnemyGenerate = true;  //¶¬‚Ì‹–‰Â

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
