using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// AI 基底 3D 模式
/// 等待、行走、追蹤玩家、攻擊、受傷與死亡
/// </summary>
public class AIBase3D : MonoBehaviour
{
    #region 欄位：公開
    [Header("移動速度"), Range(0, 500)]
    public float speed = 3;
    [Header("攻擊力"), Range(0, 500)]
    public float attack = 50;
    [Header("血量"), Range(0, 500)]
    public float hp = 300;
    [Header("追蹤範圍"), Range(0, 100)]
    public float rangeTrack = 10;
    #endregion

    #region 欄位：私人
    private Animator ani;
    private NavMeshAgent nav;
    #endregion

    #region 事件
    private void Awake()
    {
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeTrack);
    }
    #endregion

    #region 方法：公開
    #endregion

    #region 方法：私人
    #endregion
}
