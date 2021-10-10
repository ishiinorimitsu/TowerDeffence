using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectCharaDetail : MonoBehaviour�@�@//�L������I������u�{�^���v���Ǘ�����X�N���v�g
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

        Debug.Log("�����܂�OK");  �@�@//�����܂ł��ł��Ă��Ȃ�=�����̏ꏊ�Ƃ��ł͂Ȃ��������̂��̂��ł��Ă��Ȃ�

        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);
    }
    // Start is called before the first frame update
    private void OnClickSelectCharaDetail()
    {
        placementCharaSelectPop.SetSelectCharaDetail(charaData);
    }
}
