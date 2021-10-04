using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
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

}
