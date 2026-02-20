using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HorrorEventManager : MonoBehaviour
{
    //シングルトンの準備コード
    public static HorrorEventManager instance;

    //ゲーム開始後、HorrorEventが始まるまでの待ち時間
    [SerializeField]
    private float startDelay = 20f;

    //HorrorEventの露出度
    [SerializeField]
    private float exposure = 0f;

    //HorrorEventの露出度増加率
    [SerializeField]
    private float exposureIncreaseRate = 0.002f;

    //最小インターバル
    [SerializeField]
    private float mininterval = 5f;

    //最大インターバル
    [SerializeField]
    private float maxinterval = 12f;

    //基準の確率
    [SerializeField]
    private float baseProbability = 0.2f;

    //HorrorEventSpawner
    [SerializeField]
    private  HorrorEventSpawner spawner;

    //AudioSource
    [SerializeField]
    private AudioSource audioSource;

    //eventSE
    [SerializeField]
    private AudioClip eventSE;

    //次シーンのHorrorEventが始まる時間
    private float nextEventTime;

    //falseなら起こらない,trueなら起こる
    private bool isActive = false;

    /// <summary>
    /// シングルトン(Singleton)パターンの実装
    /// </summary>
    private void Awake()
    {
        if (instance == null)
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
        //指定したStartDelay秒後にActivate()を実行する
        Invoke(nameof(Activate), startDelay);
    }

    private void Activate()
    {
        isActive = true;
        
        //SetNextEventTimeの呼び出し
        SetNextEventTime();
    }
    
    void Update()
    {
        if (!isActive) return;

        //露出度=Time.unscaledDeltaTime(実時間ベース)×露出度増加度
        exposure += Time.unscaledDeltaTime * exposureIncreaseRate;
        //露出度を0～1に制限する(Clamp01)
        //ゲーム開発では正規化された値(Normalized Value)をよく使うため
        exposure = Mathf.Clamp01(exposure);

        //Time.unscaledTimeがnextEventTime以上なら
        if (Time.unscaledTime >= nextEventTime)
        {
            TryTriggerEvent();

            SetNextEventTime();
        }
    }
    /// <summary>
    /// 確率に基づいてイベント発生を試行
    /// 発生した場合は効果音を再生
    /// </summary>
    private void TryTriggerEvent()
    {
        //確率=基準確立+露出度×0.4f
        //露出度が上がるほど発生確率が上がる
        //×0.4fすることで、なめらかに増加するs
        float probability = baseProbability + exposure * 0.4f;
        //確率を0～1に制限する(Clamp01)
        //ゲーム開発では正規化された値(Normalized Value)をよく使うため
        probability = Mathf.Clamp01(probability);

        //ランダム値(0以上1未満のランダムなfloat)が確率より大きいなら処理を中断する
        if (Random.value > probability) return;

        //spwnerが空ではないならSpawnRandomEventが呼び出される
        if (spawner != null)
        {
            spawner.SpawnRandomEvent(exposure);
        }

        //audioSourceとeventSEの両方が存在しているなら効果音を再生する
        if(audioSource != null && eventSE != null)
        {
            audioSource.PlayOneShot(eventSE);
        }
    }
    /// <summary>
    /// 次のHorrorEvent発生時間
    /// </summary>
    private void SetNextEventTime()
    {
        //ランダムな間隔を作る
        float interval = Random.Range(mininterval, maxinterval);
        //次の発生時刻を決める
        nextEventTime = Time.unscaledTime + interval;
    }
}
