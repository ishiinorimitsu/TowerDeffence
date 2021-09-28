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

    private Animator anim;

    //private Vector3 currentPos;  //�G�L�����̌��݂̈ʒu

    void Start()
    {
        TryGetComponent(out anim);  //Animator�R���|�[�l���g���擾����anim�ɑ��

        // �ړ�����n�_���擾���邽�߂̔z��̏�����
        paths = new Vector3[pathData.pathTranArray.Length];

        // �ړ�����ʒu�������Ԃɔz��Ɏ擾
        for(int i = 1; i < pathData.pathTranArray.Length; i++)
        {
            paths = pathData.pathTranArray.Select(x => x.position).ToArray();
        }

        transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    ChangeAnimDirection();
    //}

    private void ChangeAnimeDirection(int index)
    {
        Debug.Log(index);

        if(index > paths.Length)
        {
            return;
        }

        // �ڕW�̈ʒu�ƌ��݂̈ʒu�Ƃ̋����ƕ������擾���A���K���������s���A�P�ʃx�N�g���Ƃ���(�����̏��͎����A�����ɂ�鑬�x�����Ȃ����Ĉ��l�ɂ���)
        Vector3 direction = (transform.position - paths[index]).normalized;
        Debug.Log(direction);

        // �A�j���[�V������ Palameter �̒l���X�V���A�ړ��A�j���� BlendTree �𐧌䂵�Ĉړ��̕����ƈړ��A�j���𓯊�
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);

        //if (transform.position.x < paths[index].x)
        //{
        //    anim.SetFloat("X", 0);
        //    anim.SetFloat("Y", -1.0f); 
        //    Debug.Log("������");
        //}
        //else if (transform.position.y > paths[index].y)
        //{
        //    anim.SetFloat("X", 0f);
        //    anim.SetFloat("Y", 1.0f);

        //    Debug.Log("�㍶��");
        //}
        //else if (transform.position.y < paths[index].y)
        //{
        //    anim.SetFloat("X", 0f);
        //    anim.SetFloat("Y", -1.0f);

        //    Debug.Log("������");
        //}
        //else
        //{
        //    anim.SetFloat("Y", 0f);
        //    anim.SetFloat("X", 1.0f);

        //    Debug.Log("�E����");
        //}

        // ���݂̈ʒu����ێ�
        //currentPos = transform.position;
    }
}

