using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SearchPlayerQTE : MonoBehaviour
{
    //InputManagerで設定したボタンを設定する
    public enum PushButton
    {
        Fire1
    }

    //QTEの種類
    public enum QTEType
    {
        //1回押す
        SinglePress
    }

    //QTE用のUIゲームオブジェクト
    [SerializeField]
    private GameObject QTEUI;
    //QTEが終了しているかどうか
    private bool isFinished;

    //タイムリミットスクリプト
    private TimeLimit timeLimit;
    //制限時間
    [SerializeField]
    private float limit = 10f;

    //キャラクター操作スクリプト
    private PlayerController playerController;

    //どのボタンを押すか
    [SerializeField]
    private PushButton pushButton;


    void Start()
    {
        isFinished = false;

        //TimeLimitスクリプトの取得
        timeLimit = QTEUI.GetComponentInChildren<TimeLimit>();
    }

    /// <summary>
    /// Playerを補足
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isFinished)
        {
            return;
        } 
        //一定距離に入ったらplayerを補足
        if (col.gameObject.tag == "Player")
        {
            //コンソールにPlayer表示
            Debug.Log(col.gameObject.tag);

            //キャラクター操作スクリプトの取得
            PlayerController playercontroller = col.GetComponent<PlayerController>();

            //UIを表示
            QTEUI.SetActive(true);

            //TimeLimitスクリプトに主人公操作スクリプト、押すべきボタン、制限時間を保存
            timeLimit.SetParam(playerController, pushButton, limit);

            //キャラクターの状態変更と自身(PlayerController)を渡す
            playerController.SetState(PlayerController.PlayerState.QTE, this);

            
        }
        
    }

    //QTE終了設定
    public void SetFinish()
    {
        //イベントが成功したら2度とイベントを発生させない
        isFinished =true;
        //QTE用のUIを非表示
        QTEUI.SetActive(false);
    }

    //押すべきボタンを返す関数
    public PushButton GetPushButton()
    {
        return pushButton;
    }

}
