using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  //Select��ToArray()�Ȃǂ֗̕��ȃ��\�b�h������B���ɔz���List�̒�������������Afilter���������肷����̂�����B �R���N�V�������ϐ��̒��ɓ����^�C�v�̂��̂�����B�iList�A�z��A�f�B�N�V���i���[�Ȃǁj

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyControllerPrefab;

    [SerializeField]
    private PathData[] pathDatas;

    private GamaManager gameManager;

    [SerializeField]
    private DrawPathLine pathLinePrefab;

    public IEnumerator PreparateEnemyGenerate(GamaManager gameManager)
    {
        this.gameManager = gameManager;

        int timer = 0;  //�����p�̃^�C�}�[��p��

        while (gameManager.isEnemyGenerate)
        {
            if(gameManager.currentGameState == GamaManager.GameState.Play)
            {
                timer++;

                if (timer > gameManager.generateIntervalTime)
                {
                    timer = 0;

                    //GenerateEnemy();

                    gameManager.AddEnemyList(GenerateEnemy());  //�G�̐������̃J�E���g�A�b�v�ƃ��X�g�ւ̒ǉ�

                    gameManager.JudgeGenerateEnemyEnd();
                }
                yield return null;
            }
        }
    }

    public void GenerateEnemy(int generateNo = 0)
    {
        int randomValue = Random.Range(0, pathDatas.Length);

        EnemyController enemyController = Instantiate(enemyControllerPrefab, pathDatas[randomValue].generateTran.position, Quaternion.identity);

        //�ړ�����n�_���擾
        Vector3[] paths = pathDatas[randomValue].pathTranArray.Select(x => x.position).ToArray();�@�@//���o���������ł͂Ȃ��A�z��ɂ��Ȃ��Ǝg��Ȃ���ԂɂȂ��Ă��܂��B

        //�G�L�����̏����ݒ�����Ĉꎞ��~���Ă���
        enemyController.SetUpEnemyController(paths);


        // �G�̈ړ��o�H�̃��C���\���𐶐��̏���
        StartCoroutine(PreparateCreatePathLine(paths, enemyController));

        return enemyController;
    }

    private IEnumerator PreparateCreatePathLine(Vector3[] paths, EnemyController enemyController)
    {
        yield return StartCoroutine(CreatePathLine(paths));

        enemyController.ResumeMove();
    }

    private IEnumerator CreatePathLine(Vector3[] paths)
    {
        //List�̐錾�Ə������@List��z��́A��������̂��L�����Ă����Ƃ����g����������B
        List<DrawPathLine> drawPathLinesList = new List<DrawPathLine>();  //�z��E�E75�s�ڂ̂悤�ɁA���g����������邩���ŏ��ɐ錾���A�r���ŕς����Ȃ��B�@�@List�E�E�錾����Ƃ��Ɍː��̎w������Ȃ��Ă����v�B

        //1��Path���ƂɂP�����ԂɃ��C�����`��
        for (int i = 0; i < paths.Length - 1; i++)
        {
            DrawPathLine drawPathLine = Instantiate(pathLinePrefab, transform.position, Quaternion.identity); //�X�N���v�g���w�肷�邾����GameObject�����������B�i�������������炨����������Ă���j

            Vector3[] drawPaths = new Vector3[2] { paths[i], paths[i + 1] };

            drawPathLine.CreatePathLine(drawPaths);

            drawPathLinesList.Add(drawPathLine);�@//Add�̏�����List�̒��g����������Ă���BList�̈�ԍŌ�ɒǉ������

            yield return new WaitForSeconds(0.1f);
        }

        // ���ׂẴ��C���𐶐����đҋ@
        yield return new WaitForSeconds(0.5f);

        // �P�̃��C�������Ԃɍ폜����
        for (int i = 0; i < drawPathLinesList.Count; i++)�@//List�̎���Length�ł͂Ȃ�Count
        {
            Destroy(drawPathLinesList[i].gameObject);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
