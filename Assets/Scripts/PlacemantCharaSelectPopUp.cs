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
    private SelectCharaDetail selectCharaDetailPrefab;    //キャラを選択するボタン用のPrefabをアサインする。

    [SerializeField]
    private Transform selectCharaDetailTran;   //キャラのボタンを生成する位置をアサインする。

    [SerializeField]
    private List<SelectCharaDetail> selectCharaDetaislList = new List<SelectCharaDetail>();  //生成するキャラのボタンを管理する

    [SerializeField]
    private CharaData chooseCharaData;   //現在選択しているキャラの情報を管理する

    private CharaGenerator charaGenerator;

    //他のスクリプトからここにいろいろな情報を送ってくる。
    public void SetUpPlacementCharaSelectPopUp(CharaGenerator charaGenerator,List<CharaData> haveCharaDataList)　　//第一引数には「何を」生成しますか？　第二引数には「キャラが入っているリスト内の内容」がある。
    {　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　//CharaGeneraterから送られてくる。このメソッドでボタンを作っている。
        Debug.Log("SetUpPlacementCharaSelectPopUpまではOK");

        this.charaGenerator = charaGenerator; 　　

        //canvasGroupを一度見えない状態にしておく
        canvasGroup.alpha = 0;

        //徐々にポップアップを表示する
        ShowPopUp();

        //ボタンを押せなくする。
        SwitchActivateButton(false);

        Debug.Log(haveCharaDataList.Count+"です。");

        //スクリプタルオブジェクトに登録されているキャラ分のボタンを生成していく
        for(int i = 0; i < haveCharaDataList.Count; i++)
        {
            //ボタンのゲームオブジェクトを作成する
            SelectCharaDetail selectCharaDetail = Instantiate(selectCharaDetailPrefab, selectCharaDetailTran, false);

            Debug.Log("OK!");

            //ボタンのゲームオブジェクトの詳細（charaData）を設定していく
            selectCharaDetail.SetUpSelectCharaDetail(this,haveCharaDataList[i]);

            //Listに追加する
            selectCharaDetaislList.Add(selectCharaDetail);

            if(i == 0)　　//初期値の設定
            {
                SetSelectCharaDetail(haveCharaDataList[i]);
            }
        }

        //それぞれのボタンにメソッドを追加する。
        btnChooseChara.onClick.AddListener(OnClickSubmitChooseChara);

        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);

        //各ボタンを押せるようにする
        SwitchActivateButton(true);
    }

    public void SwitchActivateButton(bool isSwitch)
    {
        btnChooseChara.interactable = isSwitch;  //interactableがfalseだと押せない、trueだと押せる。
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
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => { charaGenerator.InActivePlacementSelectPopUp(); });
    }

    public void SetSelectCharaDetail(CharaData charaData)  //各値の設定
    {
        chooseCharaData = charaData;　　　　　//今選んでいるキャラは、CharaDataの中のものです。

        imgPickupChara.sprite = charaData.charaSprite;　　　//今選んでいるキャラの中の絵は、CharaDataの中のCharaSpriteです。

        txtPickupCharaName.text = charaData.charaName;

        txtPickupCharaAttackPower.text = charaData.attackPower.ToString();

        txtPickupCharaAttackRangeType.text = charaData.attackRange.ToString();

        txtPickupCharaCost.text = charaData.cost.ToString();

        txtPickupCharaMaxAttackCount.text = charaData.maxAttackCount.ToString();
    }
}
