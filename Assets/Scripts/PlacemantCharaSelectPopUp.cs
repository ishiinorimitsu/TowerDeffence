using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacemantCharaSelectPopUp : MonoBehaviour
{
    //�{�^�����Ǘ����郊�X�g��selectCharaDetailsList
    //�L�������Ǘ�����̂͂��̃X�N���v�g�ł�haveCharaDataList
    //���݂��̃��X�g���̃C���f�b�N�X�ԍ��ň�v�����Ă���B

    [SerializeField]
    private Button btnClosePopUp;

    [SerializeField]
    private Button btnChooseChara;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Image imgPickupChara;

    [SerializeField]
    private Text txtPickupCharaName;

    [SerializeField]
    private Text txtPickupCharaAttackPower;

    [SerializeField]
    private Text txtPickupCharaAttackRangeType;

    [SerializeField]
    private Text txtPickupCharaCost;

    [SerializeField]
    private Text txtPickupCharaMaxAttackCount;

    [SerializeField]
    private SelectCharaDetail selectCharaDetailPrefab;    //�L������I������{�^���p��Prefab���A�T�C������B

    [SerializeField]
    private Transform selectCharaDetailTran;   //�L�����̃{�^���𐶐�����ʒu���A�T�C������B

    [SerializeField]
    private List<SelectCharaDetail> selectCharaDetailslList = new List<SelectCharaDetail>();  //��������L�����̃{�^�����Ǘ�����

    [SerializeField]
    private CharaData chooseCharaData;   //���ݑI�����Ă���L�����̏����Ǘ�����

    private CharaGenerator charaGenerator;

    //���̃X�N���v�g���炱���ɂ��낢��ȏ��𑗂��Ă���B
    public void SetUpPlacementCharaSelectPopUp(CharaGenerator charaGenerator,List<CharaData> haveCharaDataList)�@�@//�������ɂ́u�����v�������܂����H�@�������ɂ́u�L�����������Ă��郊�X�g���̓��e�v������B
    {�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@//CharaGenerater���瑗���Ă���B���̃��\�b�h�Ń{�^��������Ă���B
        Debug.Log("SetUpPlacementCharaSelectPopUp�܂ł�OK");

        this.charaGenerator = charaGenerator; �@�@

        //canvasGroup����x�����Ȃ���Ԃɂ��Ă���
        canvasGroup.alpha = 0;

        //���X�Ƀ|�b�v�A�b�v��\������
        ShowPopUp();

        //�{�^���������Ȃ�����B
        SwitchActivateButton(false);

        Debug.Log(haveCharaDataList.Count+"�ł��B");

        //�X�N���v�^���I�u�W�F�N�g�ɓo�^����Ă���L�������̃{�^���𐶐����Ă���
        for(int i = 0; i < haveCharaDataList.Count; i++)
        {
            //�{�^���̃Q�[���I�u�W�F�N�g���쐬����
            SelectCharaDetail selectCharaDetail = Instantiate(selectCharaDetailPrefab, selectCharaDetailTran, false);

            Debug.Log("OK!");

            //�{�^���̃Q�[���I�u�W�F�N�g�̏ڍׁicharaData�j��ݒ肵�Ă���
            selectCharaDetail.SetUpSelectCharaDetail(this,haveCharaDataList[i]);

            //List�ɒǉ�����
            selectCharaDetailslList.Add(selectCharaDetail);

            if(i == 0)�@�@//�����l�̐ݒ�
            {
                SetSelectCharaDetail(haveCharaDataList[i]);
            }
        }

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
        CheckAllCharaButtons();             // �e�L�����̃{�^���̐���

        canvasGroup.DOFade(1.0f, 0.5f);
    }

    public void OnClickSubmitChooseChara()      //�z�u����L�����̃{�^�����������Ƃ��̏���
    {

        // �R�X�g�̎x�������\���ŏI�m�F
        if (chooseCharaData.cost > GameData.instance.currency)
        {
            return;
        }

        charaGenerator.CreateChooseChara(chooseCharaData);

        HidePopUp();
    }

    public void OnClickClosePopUp()     //�u����v�{�^�����������Ƃ��̏���
    {
        HidePopUp();
    }


    public void HidePopUp()
    {
        CheckAllCharaButtons();

        canvasGroup.DOFade(0, 0.5f).OnComplete(() => { charaGenerator.InActivePlacementSelectPopUp(); });
    }

    public void SetSelectCharaDetail(CharaData charaData)  //�e�l�̐ݒ�
    {
        chooseCharaData = charaData;�@�@�@�@�@//���I��ł���L�����́ACharaData�̒��̂��̂ł��B

        imgPickupChara.sprite = charaData.charaSprite;�@�@�@//���I��ł���L�����̒��̊G�́ACharaData�̒���CharaSprite�ł��B

        txtPickupCharaName.text = charaData.charaName;

        txtPickupCharaAttackPower.text = charaData.attackPower.ToString();

        txtPickupCharaAttackRangeType.text = charaData.attackRange.ToString();

        txtPickupCharaCost.text = charaData.cost.ToString();

        txtPickupCharaMaxAttackCount.text = charaData.maxAttackCount.ToString();
    }

    public void CheckAllCharaButtons()
    {
        if(selectCharaDetailslList.Count > 0)       //�z�u�ł���L����������ꍇ�̂ݏ������s��
        {
            // �e�L�����̃R�X�g�ƃJ�����V�[���m�F���āA�z�u�ł��邩�ǂ����𔻒肵�ă{�^���̉����L����ݒ�
            for (int i = 0; i < selectCharaDetailslList.Count; i++)
            {
                selectCharaDetailslList[i].ChangeActiveButton(selectCharaDetailslList[i].JudgePermissionCost(GameData.instance.currency));
            }
        }
    }
}
