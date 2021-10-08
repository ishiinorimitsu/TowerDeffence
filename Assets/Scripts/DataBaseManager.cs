using UnityEngine;  //このスクリプトは、スクリプタルオブジェクトを管理するためのスクリプト

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
