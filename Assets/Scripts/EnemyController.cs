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

    private Vector3[] paths;

    private Animator anim;

    //private Vector3 currentPos;  //敵キャラの現在の位置

    void Start()
    {
        TryGetComponent(out anim);  //Animatorコンポーネントを取得してanimに代入

        // 移動する地点を取得するための配列の初期化
        paths = new Vector3[pathData.pathTranArray.Length];

        // 移動する位置情報を順番に配列に取得
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

        // 目標の位置と現在の位置との距離と方向を取得し、正規化処理を行い、単位ベクトルとする(方向の情報は持ちつつ、距離による速度差をなくして一定値にする)
        Vector3 direction = (transform.position - paths[index]).normalized;
        Debug.Log(direction);

        // アニメーションの Palameter の値を更新し、移動アニメの BlendTree を制御して移動の方向と移動アニメを同期
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);

        //if (transform.position.x < paths[index].x)
        //{
        //    anim.SetFloat("X", 0);
        //    anim.SetFloat("Y", -1.0f); 
        //    Debug.Log("左方向");
        //}
        //else if (transform.position.y > paths[index].y)
        //{
        //    anim.SetFloat("X", 0f);
        //    anim.SetFloat("Y", 1.0f);

        //    Debug.Log("上左向");
        //}
        //else if (transform.position.y < paths[index].y)
        //{
        //    anim.SetFloat("X", 0f);
        //    anim.SetFloat("Y", -1.0f);

        //    Debug.Log("下方向");
        //}
        //else
        //{
        //    anim.SetFloat("Y", 0f);
        //    anim.SetFloat("X", 1.0f);

        //    Debug.Log("右方向");
        //}

        // 現在の位置情報を保持
        //currentPos = transform.position;
    }
}

