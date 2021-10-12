using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  //SelectやToArray()などの便利なメソッドがある。特に配列やListの中を検索したり、filterをかけたりするものがある。 コレクションが変数の中に同じタイプのものを入れる。（List、配列、ディクショナリーなど）

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyControllerPrefab;

    [SerializeField]
    private PathData[] pathDatas;

    private GamaManager gameManager;

    [SerializeField]
    private DrawPathLine pathLinePrefab;

    public IEnumerator PreparateEnemyGenerate(GamaManager gameManager)
    {
        this.gameManager = gameManager;

        int timer = 0;  //生成用のタイマーを用意

        while (gameManager.isEnemyGenerate)
        {
            if(gameManager.currentGameState == GamaManager.GameState.Play)
            {
                timer++;

                if (timer > gameManager.generateIntervalTime)
                {
                    timer = 0;

                    //GenerateEnemy();

                    gameManager.AddEnemyList(GenerateEnemy());  //敵の生成数のカウントアップとリストへの追加

                    gameManager.JudgeGenerateEnemyEnd();
                }
                yield return null;
            }
        }
    }

    public void GenerateEnemy(int generateNo = 0)
    {
        int randomValue = Random.Range(0, pathDatas.Length);

        EnemyController enemyController = Instantiate(enemyControllerPrefab, pathDatas[randomValue].generateTran.position, Quaternion.identity);

        //移動する地点を取得
        Vector3[] paths = pathDatas[randomValue].pathTranArray.Select(x => x.position).ToArray();　　//抽出しただけではなく、配列にしないと使わない状態になってしまう。

        //敵キャラの初期設定をして一時停止しておく
        enemyController.SetUpEnemyController(paths);


        // 敵の移動経路のライン表示を生成の準備
        StartCoroutine(PreparateCreatePathLine(paths, enemyController));

        return enemyController;
    }

    private IEnumerator PreparateCreatePathLine(Vector3[] paths, EnemyController enemyController)
    {
        yield return StartCoroutine(CreatePathLine(paths));

        enemyController.ResumeMove();
    }

    private IEnumerator CreatePathLine(Vector3[] paths)
    {
        //Listの宣言と初期化　Listや配列は、作ったものを記憶しておくという使い方もある。
        List<DrawPathLine> drawPathLinesList = new List<DrawPathLine>();  //配列・・75行目のように、中身をいくつ入れるかを最初に宣言し、途中で変えられない。　　List・・宣言するときに戸数の指定をしなくても大丈夫。

        //1つのPathごとに１つずつ順番にラインを形成
        for (int i = 0; i < paths.Length - 1; i++)
        {
            DrawPathLine drawPathLine = Instantiate(pathLinePrefab, transform.position, Quaternion.identity); //スクリプトを指定するだけでGameObjectも生成される。（おかし買ったらおもちゃもついてくる）

            Vector3[] drawPaths = new Vector3[2] { paths[i], paths[i + 1] };

            drawPathLine.CreatePathLine(drawPaths);

            drawPathLinesList.Add(drawPathLine);　//Addの処理でListの中身が一つずつ増えている。Listの一番最後に追加される

            yield return new WaitForSeconds(0.1f);
        }

        // すべてのラインを生成して待機
        yield return new WaitForSeconds(0.5f);

        // １つのラインずつ順番に削除する
        for (int i = 0; i < drawPathLinesList.Count; i++)　//Listの時はLengthではなくCount
        {
            Destroy(drawPathLinesList[i].gameObject);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
