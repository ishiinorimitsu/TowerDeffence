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
        this.charaGenerator = charaGenerator; 　　 //送られてきたcharaGeneratorの値をここでのharaGeneratorに入れる。

        //canvasGroupを一度見えない状態にしておく
        canvasGroup.alpha = 0;

        //徐々にポップアップを表示する
        ShowPopUp();

        //ボタンを押せなくする。
        SwitchActivateButton(false);

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
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => charaGenerator.InactivePlacementCharaSelectPopUp);
    }
}
