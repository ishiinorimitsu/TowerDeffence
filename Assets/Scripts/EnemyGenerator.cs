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
    //public bool isEnemyGenerate;  //��������switch�itrue���Ɛ�������@false���Ɛ�����~�j

    //public int generateIntervalTime;    //���b�����ɍ�邩

    //public int generateEnemyCount;  //���������G�̐��𐔂���

    //public int maxEnemyCount;   //��������G�̍ő吔

    // Start is called before the first frame update
    //void Start()
    //{
    //    isEnemyGenerate = true;  //�����̋���

    //    StartCoroutine(PreparateEnemyGenerate());
    //}

    public IEnumerator PreparateEnemyGenerate(GamaManager gameManager)
    {
        this.gameManager = gameManager;

        int timer = 0;  //�����p�̃^�C�}�[��p��

        while (gameManager.isEnemyGenerate)
        {
            timer++;

            if(timer > gameManager.generateIntervalTime)
            {
                timer = 0;

                GenerateEnemy();

                gameManager.AddEnemyList();  //�G�̐������̃J�E���g�A�b�v�ƃ��X�g�ւ̒ǉ�

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
