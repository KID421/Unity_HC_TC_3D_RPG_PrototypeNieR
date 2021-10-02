using UnityEngine;
using System.Collections;
using Invector.vCharacterController;

/// <summary>
/// 攻擊系統
/// 三段攻擊與集氣
/// 判斷變身前後決定攻擊模式
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
    [Header("攻擊相關資料：攻擊力、尺寸與位移"), Range(0, 500)]
    public float[] attack = { 10, 20, 30, 40 };
    public Vector3[] areaAttackSize;
    public Vector3[] areaAttackOffset;
    public Color[] areaAttackColor;
    public float[] delaySendAttackToTarget;
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

    public vThirdPersonController v;
    public AvatarMask am;

    private void Update()
    {
        ClickTime();

        am.SetHumanoidBodyPartActive(AvatarMaskBodyPart.Root, v.verticalSpeed <= 0.1f);
        am.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftLeg, v.verticalSpeed <= 0.1f);
        am.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightLeg, v.verticalSpeed <= 0.1f);
        am.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftFootIK, v.verticalSpeed <= 0.1f);
        am.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightFootIK, v.verticalSpeed <= 0.1f);
    }

    private void OnDrawGizmos()
    {
        #region 攻擊範圍
        for (int i = 0; i < attack.Length; i++)
        {
            Gizmos.color = areaAttackColor[i];
            Gizmos.matrix = Matrix4x4.TRS(
                transform.position +
                transform.right * areaAttackOffset[i].x +
                transform.up * areaAttackOffset[i].y +
                transform.forward * areaAttackOffset[i].z,
                transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, areaAttackSize[i]);
        }
        #endregion
    }
    #endregion

    #region 方法：私人
    // VS 格式化 排版：Ctrl + K D
    /// <summary>
    /// 點擊後時間的累加
    /// </summary>
    private void ClickTime()
    {
        // 變身後，攻擊模式為變身後攻擊
        // 取得其他腳本資訊的方式 
        // 1. 尋找物件並取得物件的資料
        // bool isTransform = GameObject.Find("變身系統").GetComponent<TransformSystem>().isTransform;
        // 2. 將要取得的資料改為靜態
        bool isTransform = TransformSystem.isTransform;

        if (isTransform && Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("變身後攻擊");
            return;
        }

        // 變身前，攻擊模式為變身前攻擊
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
        StartCoroutine(AttackAreaCheck(3));
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
            StartCoroutine(AttackAreaCheck(countAttackPart));
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
    /// 攻擊區域檢查，檢查是否有擊中目標
    /// </summary>
    private IEnumerator AttackAreaCheck(int indexAttack)
    {
        yield return new WaitForSeconds(delaySendAttackToTarget[indexAttack]);

        Collider[] hits = Physics.OverlapBox(
            transform.position +
            transform.right * areaAttackOffset[indexAttack].x +
            transform.up * areaAttackOffset[indexAttack].y +
            transform.forward * areaAttackOffset[indexAttack].z,
            areaAttackSize[indexAttack] / 2, Quaternion.identity, 1 << 6);      // 更換要攻擊的圖層

        hits[0].GetComponent<DamageSystem>().Damage(attack[indexAttack]);
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
