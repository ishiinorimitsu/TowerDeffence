using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField, Header("�U����")]
    private int attackPower;

    [SerializeField, Header("�U�����邽�߂̑ҋ@����")]
    private float intervalAttackTime;

    [SerializeField]
    private bool isAttack;

    [SerializeField]
    private EnemyController enemy;

    [SerializeField]
    private int attackCount = 3;

    [SerializeField]
    private UnityEngine.UI.Text txtAttackCount;

   private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isAttack && !enemy)
        {
            Debug.Log("�G����");

            if(collision.gameObject.TryGetComponent(out enemy))
            {
                isAttack = true;

                StartCoroutine(PrepareAttack());
            }
        }
    }

    public IEnumerator PrepareAttack()
    {
        Debug.Log("�U�������J�n");

        int timer = 0;

        while (isAttack)
        {
            timer++;

            if(timer > intervalAttackTime)
            {
                timer = 0;
                Attack();

                attackCount --;

                UpdateDisplayAttackCount();

                if (attackCount <= 0)
                {
                    Destroy(gameObject);
                }
            }
            // �P�t���[�������𒆒f����(���̏����������Y���Ɩ������[�v�ɂȂ�AUnity �G�f�B�^�[�������Ȃ��Ȃ��čċN�����邱�ƂɂȂ�܂��B���ӁI)
            //�������邱�ƂŁAwhile���̍đI�l���s����B
            yield return null;
        }
    }

    private void Attack()
    {
        Debug.Log("�U��");

        enemy.CulcDamage(attackPower);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {

            Debug.Log("�G�Ȃ�");

            isAttack = false;
            enemy = null;
        }
    }

    private void UpdateDisplayAttackCount()
    {
        txtAttackCount.text = attackCount.ToString();
    }

}
