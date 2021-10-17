using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectCharaDetail : MonoBehaviour　　//キャラを選択する「ボタン」を管理するスクリプト
{
    [SerializeField]
    private Button btnSelectCharaDetail;

    [SerializeField]
    private Image imgChara;

    private PlacemantCharaSelectPopUp placementCharaSelectPop;

    private CharaData charaData;

    public void SetUpSelectCharaDetail(PlacemantCharaSelectPopUp placemantCharaSelectPop,CharaData charaData)
    {
        this.placementCharaSelectPop = placemantCharaSelectPop;
        this.charaData = charaData;

        ChangeActiveButton(false);      //ボタンを押せなくする

        imgChara.sprite = this.charaData.charaSprite;

        JudgePermissionCost(GameData.instance.currency);       //コストを支払えるかを確認する

        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);         //ボタンにメソッドを追加する

        ChangeActiveButton(true);       //ボタンを押せるようにする
    }
    // Start is called before the first frame update
    private void OnClickSelectCharaDetail()
    {
        placementCharaSelectPop.SetSelectCharaDetail(charaData);
    }

    public void ChangeActiveButton(bool isSwitch)
    {
        btnSelectCharaDetail.interactable = isSwitch;
    }

    public bool JudgePermissionCost(int value)
    {
        Debug.Log("コスト確認");

        if(charaData.cost <= value)     //コストを支払える場合
        {
            ChangeActiveButton(true);       //ボタンを押せるようにする
            return true;
        }
        return false;
    }

    public bool GetActiveButtonState()
    {
        return btnSelectCharaDetail.interactable;
    }

    //public CharaData GetCharaData()
    //{
    //    retutn charaData;
    //}
}
