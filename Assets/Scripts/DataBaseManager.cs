using UnityEngine;  //���̃X�N���v�g�́A�X�N���v�^���I�u�W�F�N�g���Ǘ����邽�߂̃X�N���v�g

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
        return attackRangeSizeSO.attackRangeSizesList.Find(x => x.attackRangeType == attackRangeType).size;  //List�̒�����i�j���̂��̂�T���B
        //Find()�̕��́Aif(attackRangeSize.attackRangeType == attackRangeType) {
                                 //return attackRangeType.size;
                        //}�Ɠ����B
    }
}
