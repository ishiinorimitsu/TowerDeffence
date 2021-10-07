using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacemantCharaSelectPopUp : MonoBehaviour
{
    [SerializeField]
    private Button btnClosePopUp;

    [SerializeField]
    private Button btnChooseChara;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private CharaGenerator charaGenerator;

    public void SetUpPlacementCharaSelectPopUp(CharaGenerator charaGenerator)
    {
        this.charaGenerator = charaGenerator; �@�@ //�����Ă���charaGenerator�̒l�������ł�haraGenerator�ɓ����B

        //canvasGroup����x�����Ȃ���Ԃɂ��Ă���
        canvasGroup.alpha = 0;

        //���X�Ƀ|�b�v�A�b�v��\������
        ShowPopUp();

        //�{�^���������Ȃ�����B
        SwitchActivateButton(false);

        //���ꂼ��̃{�^���Ƀ��\�b�h��ǉ�����B
        btnChooseChara.onClick.AddListener(OnClickSubmitChooseChara);

        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);

        //�e�{�^����������悤�ɂ���
        SwitchActivateButton(true);
    }

    public void SwitchActivateButton(bool isSwitch)
    {
        btnChooseChara.interactable = isSwitch;  //interactable��false���Ɖ����Ȃ��Atrue���Ɖ�����B
        btnClosePopUp.interactable = isSwitch;
    }

    public void ShowPopUp()
    {
        canvasGroup.DOFade(1.0f, 0.5f);
    }

    public void OnClickSubmitChooseChara()
    {
        HidePopUp();
    }

    public void OnClickClosePopUp()
    {
        HidePopUp();
    }

    public void HidePopUp()
    {
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => charaGenerator.InactivePlacementCharaSelectPopUp);
    }
}
