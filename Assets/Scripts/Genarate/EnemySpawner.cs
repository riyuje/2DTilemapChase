using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Chaser enemyPrefab;  //EnemyPrefabアサイン用(Chaser型で宣言→Findメソッドを使わずにChaser.csの情報を手に入れるため)

    [SerializeField]
    private float spawnInterval;  //敵を生成するまでの待機時間の設定値用の変数

    [SerializeField]
    private Transform[] spawnTrans;  //ランダムに生成する時に利用する範囲を設定するゲームオブジェクト群のTransform

    public bool isSpawnEnemy;  //生成処理を行うかどうかの判定値。trueなら生成する

    [SerializeField]
    private Transform target;  //追跡するターゲット情報

    [SerializeField]
    private Tilemap tilemap;  //タイルマップの位置情報

    void Start()
    {
        //敵の生成
        RandomSpawnEnemy();
    }

    ///<summary>
    ///指定した範囲内のランダムな位置に敵を生成
    ///</summary>
    private void RandomSpawnEnemy()
    {
        //生成位置をランダムに決定
        Vector3 spawnPos = new(Random.Range(spawnTrans[0].position.x, spawnTrans[1].position.x), Random.Range(spawnTrans[0].position.y, spawnTrans[1].position.y), 0);

        //生成
        Chaser chaser = SpawnEnemy(spawnPos);

        //プレイヤーの移動範囲外に敵を生成していないか判定し、生成してしまっている場合には最も近い移動範囲内に移動させる

        //SamplePositionは、第4引数に指定した範囲内のNavMeshにおいて、第1引数について、最も近い点を検索する。見つかった場合にはhitに代入される。おけない場合だけfalseになる

        //navMeshHit変数は、NavMesh米九エリアに置ける場合は、chaserのpositionの情報が代入される

        //NavMeshベイクエリアじゃない場合、一番近いNavMesh米九エリアの情報が代入される

        if (NavMesh.SamplePosition(chaser.transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            //hit.positionの値は、ベイクしたエリア内に置けるPositionの場合には、chaserのpositionと同じ値をそのまま代入し直す
            //そうでない場合には、一番近いNavMeshのベイクエリアのPositionの値を代入する
            chaser.transform.position = hit.position;

            //Z座標を0に固定
            chaser.transform.position = new Vector3(chaser.transform.position.x, chaser.transform.position.y, 0);

            //敵のターゲットとタイルマップの設定
            chaser.Setup(target, tilemap);

            Debug.Log("敵の位置調整して配置");
        }
        else
        {
            Debug.Log("敵の配置不可");
        }
    }

    ///<summary>
    ///敵の生成
    ///</summary>
    public Chaser SpawnEnemy(Vector3 spawnPos)
    {
        //生成
        Chaser enemy = Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);

        Debug.Log("敵生成");

        return enemy;
    }
}
