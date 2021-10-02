using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// 受傷系統
/// 血量、動畫、死亡
/// </summary>
public class DamageSystem : MonoBehaviour
{
    #region 欄位：公開
    [Header("血量"), Range(0, 1000)]
    public float hp = 100;
    [Header("受傷死亡動畫參數名稱")]
    public string parameterDamage = "受傷觸發";
    public string parameterDead = "死亡開關";
    [Header("介面：血條，需要時請將血條放入")]
    public Image imgHp;
    [Header("受傷事件：受傷之後要處理的行為")]
    public UnityEvent onDamage;
    [Header("死亡事件：死亡之後要處理的行為")]
    public UnityEvent onDead;
    [Header("死亡後通知任務管理器")]
    public bool sendToMissionManager;
    #endregion

    #region 欄位：私人
    private Animator ani;
    private float hpMax;
    #endregion

    #region 事件
    private void Awake()
    {
        ani = GetComponent<Animator>();
        hpMax = hp;
    }
    #endregion

    #region 方法：公開
    /// <summary>
    /// 受傷，接收到攻擊力並處理血量與受傷行為
    /// </summary>
    /// <param name="getAttack">接收到的攻擊力</param>
    public void Damage(float getAttack)
    {
        if (ani.GetBool(parameterDead)) return;     // 如果 已經死亡 就 跳出 不處理

        hp -= getAttack;
        ani.SetTrigger(parameterDamage);
        onDamage.Invoke();

        if (hp <= 0) Dead();
    }

    /// <summary>
    /// 更新血條介面
    /// </summary>
    public void UpdateHpUI()
    {
        imgHp.fillAmount = hp / hpMax;
    }
    #endregion

    #region 方法：私人
    /// <summary>
    /// 死亡方法
    /// </summary>
    private void Dead()
    {
        hp = 0;
        ani.SetBool(parameterDead, true);
        onDead.Invoke();

        if (sendToMissionManager) MissionManager.instance.UpdateMissionCount(1);
    }
    #endregion
}
