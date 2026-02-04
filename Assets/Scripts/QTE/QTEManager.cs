using UnityEngine;

public class QTEManager : MonoBehaviour
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
    private bool isPlayingQTE;

    //タイムリミットスクリプト
    [SerializeField]
    private TimeLimit timeLimit;
    //制限時間
    [SerializeField]
    private float limit = 10f;

    //キャラクター操作スクリプト
    [SerializeField]
    private PlayerController playerController;

    //どのボタンを押すか
    [SerializeField]
    private PushButton pushButton;

    public static QTEManager instance;

    //UIManager呼び出し
    [SerializeField]
    private UIManager uiManager;

    //敵情報
    [SerializeField]
    private EnemyData qteEnemyData;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isPlayingQTE = false;

        //TimeLimitスクリプトの取得
        timeLimit = QTEUI.GetComponentInChildren<TimeLimit>();
    }


    void Update()
    {
        
    }

    public bool IsPlayingQTE()
    {
        return isPlayingQTE;
    }

    public void OpenQTE(EnemyData enemyData)
    {

        if (isPlayingQTE  == true)
        {
            return;
        }

        qteEnemyData = enemyData;

        isPlayingQTE = true;

        //キャラクターの状態変更と自身(PlayerController)を渡す
        playerController.SetState(PlayerController.PlayerState.QTE);

        //UIを表示
        QTEUI.SetActive(true);

        //TimeLimitスクリプトに主人公操作スクリプト、押すべきボタン、制限時間を保存
        timeLimit.SetParam(playerController, pushButton, limit);
    }

    //QTE終了設定
    public void SetFinish()
    {
        //イベントが成功後、QTEイベント初期化
        isPlayingQTE = false;
        //QTE用のUIを非表示
        QTEUI.SetActive(false);
    }

    //押すべきボタンを返す関数
    public PushButton GetPushButton()
    {
        return pushButton;
    }

    //UIManagerとTimeLimitを繋ぐ中継地点
    public void PrepareGameOverUI()
    {
        //UIManagerのゲームオーバー処理を呼び出す
        uiManager.DisplayGameOverInfo();
    }

    public void CheckWeakItemid(int itemid)
    {
        if(itemid == qteEnemyData.weakItemId) //弱点チェック
        {
            Debug.Log("弱点");
        }
        else
        {
            Debug.Log("弱点ではない");
        }
    }
}
