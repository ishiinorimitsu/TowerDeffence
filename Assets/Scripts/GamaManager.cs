using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour   //�G�̐����������ǂ�
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private CharaGenerator charaGenerator;

    public bool isEnemyGenerate;  //��������switch�itrue���Ɛ�������@false���Ɛ�����~�j

    public int generateIntervalTime;    //���b�����ɍ�邩

    public int generateEnemyCount;  //���������G�̐��𐔂���

    public int maxEnemyCount;   //��������G�̍ő吔

    public enum GameState
    {
        Preparate,      //�Q�[���J�n�O�A������
        Play,      //�Q�[���v���C��
        Stop,      //�Q�[�����ꎞ���f
        GameUp      //�Q�[���̏I���i�N���A�A�Q�[���I�[�o�[�����j
    }

    public List<EnemyController> enemiesList = new List<EnemyController>();

    public GameState currentGameState;

    // Start is called before the first frame update
    void Start()
    {
        //�Q�[���̐i�s��Ԃ��������ɐݒ�
        SetGameState(GameState.Preparate);

        StartCoroutine(charaGenerator.SetUpCharaGenerter(this));

        isEnemyGenerate = true;  //�����̋���

        // �Q�[���̐i�s��Ԃ��v���C���ɕύX
        SetGameState(GameState.Play);

        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));
    }



    public void AddEnemyList(EnemyController enemy)
    {
        enemiesList.Add(enemy);

        //�G�̐����J�E���g
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

    public void PauseEnemies()    //�G���~�߂�
    {
        for(int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].PauseMove();
        }
    }

    public void ResumeEnemies()     //�G���Ăѓ������n�߂�
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
