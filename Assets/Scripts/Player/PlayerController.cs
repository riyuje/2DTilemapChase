using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移動速度")]
    public float moveSpeed;  //通常時の移動速度
    [Header("ダッシュ速度")]
    public float dashSpeed;  //ダッシュ時の移動速度

    private Rigidbody2D rb;  //Rigidbodyコンポーネントの取得用

    private float horizontal;  //x軸(水平・横)方向の入力の値の代入用
    private float vertical;    //y軸(垂直・縦)方向の入力の値の代入用

    private bool isDash;  //ダッシュしているか否かを判断する用

    private Animator anim; //Animatorコンポーネントの取得用
    private Vector2 lookDirection = new Vector2(0, -1.0f);  //キャラの向きの情報の設定用

    private PlayerState state = PlayerState.Normal;
    

    public enum PlayerState
    {
        Normal,
        QTE,
        Dead
    }
    
    void Start()
    {
        //Rigidbody2Dコンポーネントを取得してrb変数に代入
        rb = GetComponent<Rigidbody2D>();

        //Animatorコンポーネントを取得してanim変数に代入
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(state == PlayerState.Normal)
        {
            NormalUpdate();
        }
        else if (state == PlayerState.QTE)
        {
            QTEUpdate();
        }
    }

    private void NormalUpdate()
    {
        //InputManagerのHorizontalに登録してあるキーが入力されたら、水平(横)方向の入力値として代入
        horizontal = Input.GetAxis("Horizontal");

        //InputManagerのVerticalに登録してあるキーが入力されたら、垂直(縦)方向の入力値として代入
        vertical = Input.GetAxis("Vertical");

        //Shiftキー長押し入力をisDashに代入
        isDash = Input.GetKey(KeyCode.LeftShift);

        //キャラの向いている方向と移動アニメの同期
        SyncMoveAnimation();
    }

    private void QTEUpdate()
    {

        if (Input.GetButtonDown(QTEManager.instance.GetPushButton().ToString()))
        {
            QTEManager.instance.SetFinish();
            SetState(PlayerState.Normal);
        }
    }

    void FixedUpdate()
    {
        if(state != PlayerState.Normal)
        {
            return;
        }

        //移動
        Move();
    }

    ///<summary>
    ///移動
    ///</summary>
    private void Move()
    {
        //斜めの距離が増えないように正規化処理を行い、単位ベクトルとする(方向の情報を保ちつつ、距離による速度差をなくして一定値にする)
        Vector3 dir = new Vector3(horizontal, vertical, 0).normalized;

        //speed変数にmoveSpeedを代入
        float speed = moveSpeed;

        //Shiftキーを長押しされたら、speed変数にdashSpeedを代入する
        if (isDash == true)
        {
            //speed変数にdashSpeedを代入
            speed = dashSpeed;
        }

        rb.linearVelocity = dir * speed;
    }

    ///<summary>
    ///キャラの向いている方向と移動アニメの同期
    ///</summary>
    private void SyncMoveAnimation()
    {
        //移動のキー入力値を代入
        Vector2 move = new Vector2(horizontal, vertical);

        //いずれかのキー入力があるか確認
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            //向いている方向を更新
            lookDirection.Set(move.x, move.y);

            //正規化
            lookDirection.Normalize();

            //キー入力の値とBlendTreeで設定した移動アニメ用の値を確認し、移動アニメを再生
            anim.SetFloat("Look X", lookDirection.x);
            anim.SetFloat("Look Y", lookDirection.y);
        }

        //停止アニメーション用
        anim.SetFloat("Speed",move.magnitude);
    }

    //キャラクターの状態変更関数
    public void SetState(PlayerState state)
    {
        //状態を変更
        this.state = state;

        if(state == PlayerState.QTE)
        {
            //移動停止
            rb.linearVelocity = Vector2.zero;
            anim.SetFloat("Speed", 0f);
        }
        else if(state == PlayerState.Dead)
        {
            //死亡処理
        }
    }

    public PlayerState GetState()
    {
        return state;
    }
}
