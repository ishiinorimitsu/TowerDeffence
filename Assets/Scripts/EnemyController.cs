using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("�ړ��o�H�̏��")]
    private PathData pathData;

    [SerializeField, Header("�ړ����x")]
    private float moveSpeed;

    private Vector3[] paths;

    void Start()
    {
        // �ړ�����n�_���擾���邽�߂̔z��̏�����
        paths = new Vector3[pathData.pathTranArray.Length];

        // �ړ�����ʒu�������Ԃɔz��Ɏ擾
        for(int i = 1; i < pathData.pathTranArray.Length; i++)
        {
            paths = pathData.pathTranArray.Select(x => x.position).ToArray();
        }

        transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
