using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    [Header("�R�X�g�p�̒ʉ�")]
    public int currency;

    [Header("�J�����V�[�̍ő�l")]
    public int maxCurrency;

    [Header("���b���ƂɃJ�����V�[�������邩")]
    public int currencyIntervalTime;

    [Header("�J�����V�[�������Â����邩")]
    public int addCurrencyPoint;

    public int maxCharaPlacementCount;

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
