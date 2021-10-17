using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour�@�@�@//�ݒu�����L�����̐F�X�ȏ���ݒ�
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

    [SerializeField]
    private BoxCollider2D attackRangeArea;    //�f�[�^�x�[�X�ɂ��邻�ꂼ��̃L�����f�[�^��ǂݍ���

    [SerializeField]
    private CharaData charaData;

    private GamaManager gamaManager;

    //[SerializeField]
    //private SpriteRenderer spriteRenderer;

    private Animator anim;

    private string overrideClipName = "Chara_0";    //�㏑�����Ă���AnimationClip�BPrefab�ō��ŏ��̂��motion�̖��O��o�^����B

    private AnimatorOverrideController overrideController;

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
            if(gamaManager.currentGameState == GamaManager.GameState.Play)
            {
                timer++;

                if (timer > intervalAttackTime)
                {
                    timer = 0;
                    Attack();

                    attackCount--;

                    UpdateDisplayAttackCount();

                    if (attackCount <= 0)
                    {
                        Destroy(gameObject);

                        // �L�����̃��X�g��������폜
                        gamaManager.RemoveCharaList(this);
                    }
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

    public void SetUpChara(CharaData charaData,GamaManager gamaManager)
    {
        this.charaData = charaData;
        this.gamaManager = gamaManager;

        attackPower = this.charaData.attackPower;

        intervalAttackTime = this.charaData.intervalAttackTime;

        // DataBaseManager �ɓo�^����Ă��� AttackRangeSizeSO �X�N���v�^�u���E�I�u�W�F�N�g�̃f�[�^�Əƍ����s���ACharaData �� AttackRangeType �̏������� Size ��ݒ�
        attackRangeArea.size = DataBaseManager.instance.GetAttackRangeSize(this.charaData.attackRange);

        attackCount = this.charaData.maxAttackCount;

        // �c��̍U���񐔂̕\���X�V
        UpdateDisplayAttackCount();

        // �L�����摜�̐ݒ�B�A�j���𗘗p����悤�ɂȂ�����A���̏����͂��Ȃ�
        //if (TryGetComponent(out spriteRenderer))
        //{

        //    // �摜��z�u�����L�����̉摜�ɍ����ւ���
        //    spriteRenderer.sprite = this.charaData.charaSprite;
        //    Debug.Log("sprite-ok");
        //}

        Debug.Log(this.charaData.charaName);

        SetUpAnimation();
    }

    private void SetUpAnimation()
    {
        if(TryGetComponent(out anim))
        {
            overrideController = new AnimatorOverrideController();

            overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
            anim.runtimeAnimatorController = overrideController;

            AnimatorStateInfo[] layerInfo = new AnimatorStateInfo[anim.layerCount];

            for (int i = 0; i < anim.layerCount; i++)
            {
                layerInfo[i] = anim.GetCurrentAnimatorStateInfo(i);
            }

            overrideController[overrideClipName] = this.charaData.charaAnim;

            anim.runtimeAnimatorController = overrideController;

            anim.Update(0.0f);

            for (int i = 0; i < anim.layerCount; i++)
            {
                anim.Play(layerInfo[i].fullPathHash, i, layerInfo[i].normalizedTime);
            }
        }
    }

}
