using UnityEngine;  //このスクリプトは、スクリプタルオブジェクトを管理するためのスクリプト

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    public CharaDataSO charaDataSO;

    public AttackRangeSizeSO attackRangeSizeSO;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector2 GetAttackRangeSize(AttackRangeType attackRangeType)
    {
        return attackRangeSizeSO.attackRangeSizesList.Find(x => x.attackRangeType == attackRangeType).size;  //Listの中から（）内のものを探す。
        //Find()の文は、if(attackRangeSize.attackRangeType == attackRangeType) {
                                 //return attackRangeType.size;
                        //}と同じ。
    }
}
