using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("移動経路の情報")]
    private PathData pathData;

    [SerializeField, Header("移動速度")]
    private float moveSpeed;

    [SerializeField, Header("最大HP")]
    private int maxHp; //HPの最大値

    [SerializeField]
    private int hp;  //現在のHP

    private Tween tween;

    private Vector3[] paths;

    private Animator anim;

    public int attackPower;

    //private Vector3 currentPos;  //敵キャラの現在の位置

    public void SetUpEnemyController(Vector3[] pathData)
    {
        Debug.Log("aaa");
        hp = maxHp;

        TryGetComponent(out anim);  //Animatorコンポーネントを取得してanimに代入

        // 移動する地点を取得するための配列の初期化
        paths = pathData;  //引数で届いたものを代入

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

        // 目標の位置と現在の位置との距離と方向を取得し、正規化処理を行い、単位ベクトルとする(方向の情報は持ちつつ、距離による速度差をなくして一定値にする)
        Vector3 direction = (transform.position - paths[index]).normalized;
        Debug.Log(direction);

        // アニメーションの Palameter の値を更新し、移動アニメの BlendTree を制御して移動の方向と移動アニメを同期
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);
    }

    public void CulcDamage(int amount)
    {
        //Mathf.Clamp(制御したい数値,最小値,最大値);
        hp = Mathf.Clamp(hp -= amount, 0, maxHp);

        Debug.Log("残りHP:"+hp);

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

