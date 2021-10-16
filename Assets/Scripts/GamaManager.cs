using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour   //�G�̐����������ǂ�
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private CharaGenerator charaGenerator;

    [SerializeField]
    private UIManager uiManager;

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

    private int destroyEnemyCount;

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

        StartCoroutine(TimeToCurrency());
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

    public void RemoveEnemyList(EnemyController removeEnemy)
    {
        enemiesList.Remove(removeEnemy);
    }

    public void CountUpDestroyEnemyCount(EnemyController enemyController)
    {
        RemoveEnemyList(enemyController);   //List����j�󂳂ꂽ�G���폜����

        destroyEnemyCount++;

        Debug.Log("�j�󂵂��G�̐��F"+ destroyEnemyCount);   

        JudgeGameClear();   //�Q�[�����N���A���Ă��邩�m���߂�
    }

    private void JudgeGameClear()
    {
        if(destroyEnemyCount >= maxEnemyCount)
        {
            Debug.Log("�Q�[���N���A");
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

            if (timer >= GameData.instance.currencyIntervalTime && GameData.instance.currency < GameData.instance.maxCurrency)  //��莞�Ԍo�߂��Ɓ��J�����V�[���ő�l�łȂ����
            {

                Debug.Log("OK2");

                timer = 0;

                //�J�����V�[�̒l���w�肵���͈͓���addCurrencyPoint�����₵�Ă����B
                GameData.instance.currency = Mathf.Clamp(GameData.instance.currency += GameData.instance.addCurrencyPoint, 0, GameData.instance.maxCurrency);

                uiManager.UpdateDisplayCurrency();
            }
            else
            {
                Debug.Log("�_��");
            }
            yield return null;
        }
    }
}
