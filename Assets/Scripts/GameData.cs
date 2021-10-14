using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    [Header("コスト用の通貨")]
    public int currency;

    [Header("カレンシーの最大値")]
    public int maxCurrency;

    [Header("何秒ごとにカレンシーが増えるか")]
    public int currencyIntervalTime;

    [Header("カレンシーがいくつづつ増えるか")]
    public int addCurrencyPoint;

    private void Awake()
    {
        if(instance = null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
