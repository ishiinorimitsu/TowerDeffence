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

    private Vector3Int gridPos;  //�^�C���}�b�v�̃^�C���̃Z�����W�̕ێ��p

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));�@�@//�}�E�X������ꏊ���擾���A������^�C���}�b�v�ł����Ƃǂ��ɂ����邩�̏���gridPos�ɑ������B
            Debug.Log(gridPos);

            GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);�@�@//�L�������w�肵���ꏊ�ɐ�������

            chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);  //�L�������Z���̒����ɔz�u����
        }
    }
}
