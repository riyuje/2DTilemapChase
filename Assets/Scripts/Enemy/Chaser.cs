using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
public class Chaser : MonoBehaviour
{
    [SerializeField]
    private Transform target; //プレイヤーオブジェクトのTransform
    [SerializeField]
    private Tilemap tilemap;  //タイルマップコンポーネント

    [SerializeField]
    private float sightRange = 10f; //視界の範囲
    [SerializeField]
    private float moveSpeed ; //移動速度

    [SerializeField]
    private NavMeshAgent agent;  //NavMeshAgentを使用
    void Start()
    {
        //NavMeshAgentを取得できるか確認する。取得できた場合のみ、if文内の処理を行う
        if (TryGetComponent(out agent))
        {
            //インスタンス対応。ここでNavMeshAgentを有効化する。
            //事前に入れておくと、Bakeしている地点を認識できずエラーになるため
            //今回のようにUpdateメソッドで対応している場合は不要。
            //agent.enabled = true;

            //3D用の設定をオフにする。2Dなので、この処理がないと変な位置に移動する
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            //初期目的地設定(これがないと初期位置からずれる)
            agent.destination = transform.position;
            Debug.Log(agent.destination);

            //取得できたので移動速度を設定
            agent.speed = moveSpeed;
        }
        else
        {
            Debug.Log("NavMeshAgentが取得できません");
        }
    }

    void Update()
    {
        //NavMeshComponentが取得できない、あるいは移動先の対象が設定されていない場合
        if(!agent || !target || !tilemap)
        {
            //処理を行わない(エラーが出てしまうため)
            return;
        }

        //①プレイヤーのワールド座標→セル座標
        Vector3Int cellPos = tilemap.WorldToCell(target.position);

        //②セル座標→セル中心のワールド座標
        Vector3 targetWorldPos = tilemap.GetCellCenterWorld(cellPos);

        //このオブジェクトと移動先の対象までの距離を計算
        float distance = Vector3.Distance(transform.position, targetWorldPos);

        //どのくらい離れているかを表示
        Debug.Log("対象までの距離:" + distance);

        //対象が視界に入った場合(距離が近い場合)
        if(distance <= sightRange)
        {
            //対象の位置を移動目撃地点に設定
            //updateメソッド内で実行しているので、対象が移動すると、目標地点も更新されるので追跡する
            agent.SetDestination(targetWorldPos);
        }
        else
        {
            //対象が視界外の場合(距離が遠い場合)、移動を停止する
            agent.ResetPath();
        }
    }
}
