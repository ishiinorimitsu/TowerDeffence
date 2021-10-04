using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeffenceTower : MonoBehaviour
{
    [SerializeField,Header("耐久値")]
    private int maxDeffenceBaseDuability;

    private int deffenceBaseDuability;  //現在の耐久値

    [SerializeField]
    private EnemyController enemyController;

    private void Start()
    {
        deffenceBaseDuability = maxDeffenceBaseDuability;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        //侵入してきた敵キャラと、そのスクリプト情報を代入
        if (col.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            //耐久値を相手のキャラのアタックパワー分減少、その際決められた範囲内に収める
            deffenceBaseDuability = Mathf.Clamp(deffenceBaseDuability - enemyController.attackPower, 0, maxDeffenceBaseDuability);

            if (deffenceBaseDuability <= 0)
            {
                Debug.Log("GameOver");
            }
            enemyController.DestroyEnemy();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
