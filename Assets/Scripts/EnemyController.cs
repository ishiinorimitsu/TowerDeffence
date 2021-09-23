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

    void Start()
    {
        // 移動する地点を取得するための配列の初期化
        paths = new Vector3[pathData.pathTranArray.Length];

        // 移動する位置情報を順番に配列に取得
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
