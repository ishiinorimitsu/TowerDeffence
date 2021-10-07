using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharaGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject charaPrefab;   //�L�����N�^�[�̃v���t�@�u��o�^����

    [SerializeField]
    private Grid grid;   //�^�C���}�b�v�̍��W���擾���邽�߂̃��m�BGrid�QBase�����Grid ���w�肷��B

    [SerializeField]
    private Tilemap tileMap;

    [SerializeField]
    private PlacemantCharaSelectPopUp placementCharaSelectPopUpPrefab;�@�@�@//�@PlacementCharaSelectPopUp �v���t�@�u�Q�[���I�u�W�F�N�g���A�T�C���p

    [SerializeField]
    private Transform canvasTran;�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@ //�@PlacementCharaSelectPopUp �Q�[���I�u�W�F�N�g�̐����ʒu�̓o�^�p

    private PlacemantCharaSelectPopUp placementCharaSelectPopUp;�@�@�@�@�@�@//�@�������ꂽ PlacementCharaSelectPopUp �Q�[���I�u�W�F�N�g�������邽�߂̕ϐ�

    private GamaManager gameManager;


    private Vector3Int gridPos;  //�^�C���}�b�v�̃^�C���̃Z�����W�̕ێ��p

    // Update is called once per frame
    void Update()
    {
        //�}�E�X��������Ă��z�u�L�����̃|�b�v�A�b�v����\����ԂȂ�
        if (Input.GetMouseButtonDown(0) && !placementCharaSelectPopUp.gameObject.activeSelf)
        {
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));�@�@//�}�E�X������ꏊ���擾���A������^�C���}�b�v�ł����Ƃǂ��ɂ����邩�̏���gridPos�ɑ������B
            
            if(tileMap.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                //CreateChara(gridPos);

                //�z�u�L������I������|�b�v�A�b�v��\������
                ActivatePlacementCharaSelectPopUp();
            }
        }
    }

    private void CreateChara(Vector3Int gridPos)
    {

        // �^�b�v�����ʒu�ɃL�����𐶐����Ĕz�u
        GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);  //�L�������w�肵���ꏊ�ɐ�������

        // �L�����̈ʒu���^�C���̍����� 0,0 �Ƃ��Đ������Ă���̂ŁA�^�C���̒����ɂ���悤�Ɉʒu�𒲐�
        chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);  //�L�������Z���̒����ɔz�u����
    }


    public IEnumerator SetUpCharaGenerter(GamaManager gameManager)
    {
        this.gameManager = gameManager;

        yield return CreatePlacementCharaSetUpPopUp();
    }

    //�z�u�L�����I��p�̃|�b�v�A�b�v�̐���
    public IEnumerator CreatePlacementCharaSetUpPopUp()
    {
        placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);  //�|�b�v�A�b�v�𐶐�����

        placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this);  //�|�b�v�A�b�v�̐ݒ�

        placementCharaSelectPopUp.gameObject.SetActive(false);�@//�|�b�v�A�b�v���\��

        yield return null;
    }
    public void ActivatePlacementCharaSelectPopUp()
    {
        placementCharaSelectPopUp.gameObject.SetActive(true); //�|�b�v�A�b�v��\��
        placementCharaSelectPopUp.ShowPopUp();
    }

    public void InActivePlacementSelectPopUp()
    {
        //�z�u�L�����I��p�̃|�b�v�A�b�v���\���ɂ���
        placementCharaSelectPopUp.gameObject.SetActive(false);
    }
}
