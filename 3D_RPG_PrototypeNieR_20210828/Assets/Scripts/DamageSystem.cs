using UnityEngine;

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
    #endregion

    #region 欄位：私人
    private Animator ani;
    #endregion

    #region 事件
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    #endregion

    #region 方法：公開
    /// <summary>
    /// 受傷，接收到攻擊力並處理血量與受傷行為
    /// </summary>
    /// <param name="getAttack">接收到的攻擊力</param>
    public void Damage(float getAttack)
    {
        hp -= getAttack;
        ani.SetTrigger(parameterDamage);

        if (hp <= 0) Dead();
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
    }
    #endregion
}
