using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour　　　//設置したキャラの色々な情報を設定
{
    [SerializeField, Header("攻撃力")]
    private int attackPower;

    [SerializeField, Header("攻撃するための待機時間")]
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
    private BoxCollider2D attackRangeArea;    //データベースにあるそれぞれのキャラデータを読み込む

    [SerializeField]
    private CharaData charaData;

    private GamaManager gamaManager;

    //[SerializeField]
    //private SpriteRenderer spriteRenderer;

    private Animator anim;

    private string overrideClipName = "Chara_0";    //上書きしていくAnimationClip。Prefabで作る最初のやつのmotionの名前を登録する。

    private AnimatorOverrideController overrideController;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isAttack && !enemy)
        {
            Debug.Log("敵発見");

            if(collision.gameObject.TryGetComponent(out enemy))
            {
                isAttack = true;

                StartCoroutine(PrepareAttack());
            }
        }
    }

    public IEnumerator PrepareAttack()
    {
        Debug.Log("攻撃準備開始");

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

                        // キャラのリストから情報を削除
                        gamaManager.RemoveCharaList(this);
                    }
                }
            }
            // １フレーム処理を中断する(この処理を書き忘れると無限ループになり、Unity エディターが動かなくなって再起動することになります。注意！)
            //こうすることで、while文の再選考が行われる。
            yield return null;
        }
    }

    private void Attack()
    {
        Debug.Log("攻撃");

        enemy.CulcDamage(attackPower);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {

            Debug.Log("敵なし");

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

        // DataBaseManager に登録されている AttackRangeSizeSO スクリプタブル・オブジェクトのデータと照合を行い、CharaData の AttackRangeType の情報を元に Size を設定
        attackRangeArea.size = DataBaseManager.instance.GetAttackRangeSize(this.charaData.attackRange);

        attackCount = this.charaData.maxAttackCount;

        // 残りの攻撃回数の表示更新
        UpdateDisplayAttackCount();

        // キャラ画像の設定。アニメを利用するようになったら、この処理はやらない
        //if (TryGetComponent(out spriteRenderer))
        //{

        //    // 画像を配置したキャラの画像に差し替える
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
