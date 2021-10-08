using UnityEngine;  //���̃X�N���v�g�́A�X�N���v�^���I�u�W�F�N�g���Ǘ����邽�߂̃X�N���v�g

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    public CharaDataSO charaDataSO;

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
}
