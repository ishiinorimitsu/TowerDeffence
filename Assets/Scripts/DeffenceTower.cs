using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeffenceTower : MonoBehaviour
{
    [SerializeField,Header("�ϋv�l")]
    private int maxDeffenceBaseDuability;

    private int deffenceBaseDuability;  //���݂̑ϋv�l

    [SerializeField]
    private EnemyController enemyController;

    private void Start()
    {
        deffenceBaseDuability = maxDeffenceBaseDuability;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        //�N�����Ă����G�L�����ƁA���̃X�N���v�g������
        if (col.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            //�ϋv�l�𑊎�̃L�����̃A�^�b�N�p���[�������A���̍ی��߂�ꂽ�͈͓��Ɏ��߂�
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
