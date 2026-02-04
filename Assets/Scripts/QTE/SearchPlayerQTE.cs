using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SearchPlayerQTE : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;

    [SerializeField]
    private int id;

    //キャラクター操作スクリプト
    private PlayerController playerController;

    private void Start()
    {
        enemyData = TableManager.instance.GetEnemyData(id);
    }

    /// <summary>
    /// Playerを補足
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col)
    {
        //一定距離に入ったらplayerを補足
        if (col.gameObject.tag == "Player")
        {
            //コンソールにPlayer表示
            Debug.Log(col.gameObject.tag);

            QTEManager.instance.OpenQTE(enemyData);
        }
    }
}
