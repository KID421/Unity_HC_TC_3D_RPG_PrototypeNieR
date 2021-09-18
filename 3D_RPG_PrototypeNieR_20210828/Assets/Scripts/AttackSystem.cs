using UnityEngine;

/// <summary>
/// 攻擊系統
/// 三段攻擊與集氣
/// </summary>
public class AttackSystem : MonoBehaviour
{
    #region 欄位：公開
    [Header("參數名稱")]
    public string parAttackPart = "普攻段數";
    public string parAttackGather = "普攻擊氣";
    [Header("連擊間隔等待時間"), Range(0, 2)]
    public float[] intervalBetweenAttackPart = { 0.5f, 0.5f, 0.7f };
    [Header("集氣時間"), Range(0, 2)]
    public float timeToAttackGather = 1;
    [Header("攻擊段數"), Range(0, 10)]
    public int countAttackPartMax = 3;

    public AvatarMask a;
    #endregion

    #region 欄位：私人
    private Animator ani;
    /// <summary>
    /// 紀錄玩家按下左鍵的時間
    /// </summary>
    private float timerAttackGather;
    /// <summary>
    /// 連段攻擊使用的計時器
    /// </summary>
    private float timerAttackPart;
    /// <summary>
    /// 攻擊段數
    /// </summary>
    private int countAttackPart;
    #endregion

    #region 事件
    // 喚醒事件：遊戲播放後以及 Start 執行之前執行一次
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    // 開始事件：遊戲播放後以及 Awake 執行之後執行一次
    private void Start()
    {

    }

    private void Update()
    {
        ClickTime();
    }
    #endregion

    #region 方法：私人
    // VS 格式化 排版：Ctrl + K D
    /// <summary>
    /// 點擊後時間的累加
    /// </summary>
    private void ClickTime()
    {
        if (Input.GetKey(KeyCode.Mouse0))                           // 按住 左鍵
        {
            timerAttackGather += Time.deltaTime;                    // 累加 計時器集氣
            timerAttackPart += Time.deltaTime;                      // 累加 計時器段數
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))                    // 放開 左鍵
        {
            if (timerAttackGather >= timeToAttackGather)            // 如果 計時器 >= 集氣時間
            {
                AttackGather();
            }
            else                                                    // 否則 
            {
                AttackPart();
            }

            timerAttackGather = 0;                                  // 計時器 歸零
        }
    }

    /// <summary>
    /// 集氣攻擊
    /// </summary>
    private void AttackGather()
    {
        ani.SetTrigger(parAttackGather);
    }

    /// <summary>
    /// 攻擊段數
    /// </summary>
    private void AttackPart()
    {
        if (timerAttackPart <= intervalBetweenAttackPart[countAttackPart])                          // 如果 計時器段數 <= 段數間隔
        {
            CancelInvoke();                                                                         // 取消 延遲呼叫 避免在攻擊時歸零
            Invoke("RestoreAttackPartCountToZero", intervalBetweenAttackPart[countAttackPart]);     // 延遲呼叫歸零
            countAttackPart++;                                                                      // 增加段數
        }
        else                                                                                        // 否則
        {
            countAttackPart = 0;                                                                    // 段數歸零
        }

        timerAttackPart = 0;                                                                        // 計時器歸零
        ani.SetInteger(parAttackPart, countAttackPart);                                             // 更新段數參數
        if (countAttackPart == countAttackPartMax) countAttackPart = 0;
    }

    /// <summary>
    /// 恢復攻擊段數為零
    /// </summary>
    private void RestoreAttackPartCountToZero()
    {
        countAttackPart = 0;
        ani.SetInteger(parAttackPart, countAttackPart);
    }
    #endregion
}
