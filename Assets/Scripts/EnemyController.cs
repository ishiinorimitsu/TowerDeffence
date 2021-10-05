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

    [SerializeField, Header("�ő�HP")]
    private int maxHp; //HP�̍ő�l

    [SerializeField]
    private int hp;  //���݂�HP

    private Tween tween;

    private Vector3[] paths;

    private Animator anim;

    public int attackPower;

    //private Vector3 currentPos;  //�G�L�����̌��݂̈ʒu

    public void SetUpEnemyController(Vector3[] pathData)
    {
        Debug.Log("aaa");
        hp = maxHp;

        TryGetComponent(out anim);  //Animator�R���|�[�l���g���擾����anim�ɑ��

        // �ړ�����n�_���擾���邽�߂̔z��̏�����
        paths = pathData;  //�����œ͂������̂���

        tween = transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);

        PauseMove();
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
    }

    public void CulcDamage(int amount)
    {
        //Mathf.Clamp(���䂵�������l,�ŏ��l,�ő�l);
        hp = Mathf.Clamp(hp -= amount, 0, maxHp);

        Debug.Log("�c��HP:"+hp);

        if(hp <= 0)
        {
            DestroyEnemy();
        }

        StartCoroutine(WaitMove());
    }

    public void DestroyEnemy()
    {
        tween.Kill();

        Destroy(gameObject);
    }

    public void PauseMove()
    {
        tween.Pause();
    }

    public void ResumeMove()
    {
        tween.Play();
    }

    private IEnumerator WaitMove()
    {
        tween.timeScale = 0.05f;

        yield return new WaitForSeconds(0.5f);

        tween.timeScale = 1.0f;
    }
}

