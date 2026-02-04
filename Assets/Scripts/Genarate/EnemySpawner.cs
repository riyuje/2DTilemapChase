using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Chaser enemyPrefab;

    [SerializeField]
    private float spawnInterval;　　　　//　敵を生成するまでの待機時間の設定値用の変数

    [SerializeField]
    private Transform[] spawnTrans;　　　//　ランダムに生成するときに利用する範囲を設定するゲームオブジェクト群の Transform

    public bool isSpawnEnemy;         //　生成処理を行うかどうかの判定値。true なら生成する

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Tilemap tilemap;


    [SerializeField]
    private float timer;               //  ゲーム内時間の計測用の変数

    [SerializeField]
    private int maxEnemyCount;        //  最大生成数の設定用の変数

    private int currentEnemyCount;    //  現在の生成数を把握するための変数


    void Start()
    {
        // 指定した範囲内のランダムな位置に敵を生成
        RandomSpawnEnemy();


        // 時間の計測監視
        StartCoroutine(ObserveTimer());


    }

    /// <summary>
    /// 時間の計測監視
    /// </summary>
    /// <returns></returns>
    private IEnumerator ObserveTimer()
    {

        // isSpawnEnemy が true の間、敵の生成を繰り返す
        while (isSpawnEnemy && maxEnemyCount >= currentEnemyCount)
        {

            // 指定した秒数だけ処理を中断(待機)
            yield return new WaitForSeconds(spawnInterval);

            // 指定した範囲内のランダムな位置に敵を生成
            RandomSpawnEnemy();

            currentEnemyCount++;
        }
    }


    /// <summary>
    /// 指定した範囲内のランダムな位置に敵を生成
    /// </summary>
    private void RandomSpawnEnemy()
    {

        // 生成位置をランダムに決定
        Vector3 spawnPos = new(Random.Range(spawnTrans[0].position.x, spawnTrans[1].position.x),
                               Random.Range(spawnTrans[0].position.y, spawnTrans[1].position.y),
                               0);

        // 生成
        Chaser chaser = SpawnEnemy(spawnPos);

        // プレイヤーの移動範囲外に敵を生成していないか判定し、生成してしまっている場合には最も近い移動範囲内に移動させる

        // SamplePosition は、sourcePosition（第1引数）を基準に、maxDistance（第3引数）以内で、
        // areaMask（第4引数）に含まれる、NavMesh 上の「最も近い点」を検索する。
        // 見つかった場合は true を返し、その位置情報が hit に格納される。
        // maxDistance 以内に NavMesh が存在しない場合のみ false になる。
        if (NavMesh.SamplePosition(chaser.transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {

            // hit.position には必ず NavMesh 上の座標が入る。
            // sourcePosition がすでに NavMesh 上にある場合は、ほぼ同じ座標になるが、NavMesh 外の場合は、最も近い NavMesh 上の座標に補正される。
            chaser.transform.position = hit.position;
            chaser.transform.rotation = Quaternion.identity;


            // Z座標を0に固定
            chaser.transform.position = new Vector3(chaser.transform.position.x, chaser.transform.position.y, 0);

            // 敵のターゲットとタイルマップの設定
            chaser.Setup(target, tilemap);



            //Debug.Log("敵の位置調整して配置");
        }
        else
        {
            Debug.Log("敵の配置不可");
        }
    }

    /// <summary>
    /// 敵の生成
    /// </summary>
    /// <param name="spawnPos">ランダムに取得した生成用座標</param>
    /// <returns></returns>
    public Chaser SpawnEnemy(Vector3 spawnPos)
    {

        // 生成
        Chaser enemy = Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);

        //Debug.Log("敵生成");

        return enemy;
    }
}