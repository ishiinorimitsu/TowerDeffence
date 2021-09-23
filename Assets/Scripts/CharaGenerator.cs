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

    [SerializeField]
    private Tilemap tileMap;

    private Vector3Int gridPos;  //タイルマップのタイルのセル座標の保持用

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));　　//マウスがある場所を取得し、それをタイルマップでいうとどこにあたるかの情報をgridPosに代入する。
            
            if(tileMap.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                CreateChara(gridPos);
            }
        }
    }

    private void CreateChara(Vector3Int gridPos)
    {

        // タップした位置にキャラを生成して配置
        GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);  //キャラを指定した場所に生成する

        // キャラの位置がタイルの左下を 0,0 として生成しているので、タイルの中央にくるように位置を調整
        chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);  //キャラをセルの中央に配置する
    }
}
