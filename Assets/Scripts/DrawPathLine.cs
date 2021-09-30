using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPathLine : MonoBehaviour
{
    [SerializeField]
    private float startLineWidth = 0.5f;

    [SerializeField]
    private float endLineWidth = 0.5f;

    public void CreatePathLine(Vector3[] drawPaths)
    {
        TryGetComponent(out LineRenderer lineRenderer);

        //ラインの太さを調整
        lineRenderer.startWidth = startLineWidth;
        lineRenderer.endWidth = endLineWidth;

        //生成するラインの頂点の数を設定する
        lineRenderer.positionCount = drawPaths.Length;　//頂点が二つで一本の線

        //ラインを一つ生成
        lineRenderer.SetPositions(drawPaths);　//頂点の数に応じた線を引いてくれる。
    }
}
