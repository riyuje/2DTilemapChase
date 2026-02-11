using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour
{
    //QTEが実行できる制限時間
    private float limitTime;
    //経過時間
    private float nowTime;

    //タイマーアイコン
    [SerializeField]
    private Image timerImage;
    //タイマーアイコンの色を変えるパーセンテージ
    [SerializeField]
    private float limitPer = 80f;

    //主人公キャラ操作スクリプト
    private PlayerController playerController;

    //スライダー
    [SerializeField]
    private Slider slider;
    //タイマーが起動しているかどうか
    private bool startTimer = false;
    //押すべきボタンを表示するテキスト
    [SerializeField]
    private Text pushButtonText;

    [SerializeField]
    private GameObject qteUIObj;

    //キャラクタースクリプト、制限時間を保存しておく
    public void SetParam(PlayerController player, QTEManager.PushButton pushButton, float limit)
    {
        playerController = player;
        this.limitTime = limit;

        //ボタンを押すとテキスト表示する
        if (pushButton == QTEManager.PushButton.Jump)
        {
            this.pushButtonText.text = "Push SPACE Key";
        }

        //初期化処理
        nowTime = 0f;
        slider.value = 0f;

        //設定を保存したらタイマースタート
        IsStartTimer(true);

    }
    //タイマーのオンオフ
    void IsStartTimer(bool flag)
    {
        startTimer = flag;
    }

    void Update()
    {
        if (!startTimer) return;

        //成功判定
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //bool IsSuccess = InventryManager.instance.UseSelectedItemInQTE();

            //if (IsSuccess)
            {
                OnSuccess();
            }
        }


        //タイマー処理
        if (nowTime < limitTime)
        {
            nowTime += Time.deltaTime;
            slider.value += slider.maxValue / limitTime * Time.deltaTime;

            if (slider.value >= (slider.maxValue * (limitPer * 0.01f)))
            {
                timerImage.color = Color.red;
            }
        }
        else
        {
            SetFinished();
            playerController.SetState(PlayerController.PlayerState.Dead);
            QTEManager.instance.PrepareGameOverUI();
        }
    }

    void OnSuccess()
    {
        Debug.Log("QTE成功");

        //InventryManager.instance.UseSelectedItemInQTE();

        QTEManager.instance.SetFinish();
        SetFinished();
    }


    //タイマーを止めてUIを非表示
    void SetFinished()
    {
        IsStartTimer(false);
        qteUIObj.SetActive(false);
    }
}
