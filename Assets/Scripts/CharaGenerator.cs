using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharaGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject charaPrefab;   //キャラクターのプレファブを登録する

    [SerializeField]
    private Grid grid;   //タイルマップの座標を取得するためのモノ。Grid＿BaseがわのGrid を指定する。

    private Vector3Int gridPos;  //タイルマップのタイルのセル座標の保持用

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));　　//マウスがある場所を取得し、それをタイルマップでいうとどこにあたるかの情報をgridPosに代入する。
            Debug.Log(gridPos);

            GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);　　//キャラを指定した場所に生成する

            chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);  //キャラをセルの中央に配置する
        }
    }
}
