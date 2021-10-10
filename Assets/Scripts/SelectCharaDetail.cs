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

        imgChara.sprite = this.charaData.charaSprite;

        Debug.Log("ここまでOK");  　　//ここまでもできていない=生成の場所とかではなく生成そのものができていない

        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);
    }
    // Start is called before the first frame update
    private void OnClickSelectCharaDetail()
    {
        placementCharaSelectPop.SetSelectCharaDetail(charaData);
    }
}
