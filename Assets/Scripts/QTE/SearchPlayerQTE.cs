using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SearchPlayerQTE : MonoBehaviour
{

    //キャラクター操作スクリプト
    private PlayerController playerController;

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

            QTEManager.instance.OpenQTE();
        }
    }
}
