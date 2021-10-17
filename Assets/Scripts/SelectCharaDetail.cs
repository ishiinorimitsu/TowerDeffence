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

        ChangeActiveButton(false);      //�{�^���������Ȃ�����

        imgChara.sprite = this.charaData.charaSprite;

        JudgePermissionCost(GameData.instance.currency);       //�R�X�g���x�����邩���m�F����

        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);         //�{�^���Ƀ��\�b�h��ǉ�����

        ChangeActiveButton(true);       //�{�^����������悤�ɂ���
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
        Debug.Log("�R�X�g�m�F");

        if(charaData.cost <= value)     //�R�X�g���x������ꍇ
        {
            ChangeActiveButton(true);       //�{�^����������悤�ɂ���
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
