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
    [Header("攻擊範圍"), Range(0, 20)]
    public float rangeAttack = 2.5f;
    [Header("攻擊冷卻"), Range(0, 10)]
    public float cdAttack = 3.5f;
    [Header("面向速度"), Range(0, 10)]
    public float turn = 2.5f;
    [Header("攻擊範圍尺寸與位移")]
    public Vector3 areaAttackSize = Vector3.one;
    public Vector3 areaAttackOffset;
    #endregion

    #region 欄位：私人
    private Animator ani;
    private NavMeshAgent nav;
    private Transform target;
    private float timerAttack;
    #endregion

    #region 事件
    private void Awake()
    {
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;
        nav.stoppingDistance = rangeAttack;
        timerAttack = cdAttack;
    }

    private void OnDrawGizmos()
    {
        #region 追蹤與攻擊範圍
        Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeTrack);

        Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeAttack);
        #endregion

        #region 攻擊範圍
        Gizmos.color = new Color(0.2f, 0, 1, 0.3f);
        //Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.DrawCube(
            transform.position +
            transform.right * areaAttackOffset.x +
            transform.up * areaAttackOffset.y +
            transform.forward * areaAttackOffset.z,
            areaAttackSize);
        #endregion
    }

    private void Update()
    {
        CheckTargetIsInTrackRange();
        Track();
        Attack();
    }
    #endregion

    #region 方法：公開
    #endregion

    #region 方法：私人
    /// <summary>
    /// 檢查目標物是否進入追蹤範圍內並保存目標資訊
    /// </summary>
    private void CheckTargetIsInTrackRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, rangeTrack, 1 << 3);

        if (hits.Length > 0) target = hits[0].transform;    // 玩家進入後儲存目標資訊
        else target = null;                                 // 離開後將目標資訊設為空值
    }

    /// <summary>
    /// 追蹤目標
    /// </summary>
    private void Track()
    {
        if (target)
        {
            nav.isStopped = false;
            nav.SetDestination(target.position);
        }
        else
        {
            nav.isStopped = true;
        }

        ani.SetBool("走路開關", !nav.isStopped);
    }

    /// <summary>
    /// 攻擊行為
    /// 檢查目標是否在攻擊範圍內
    /// 進行攻擊與冷卻處理並且面向玩家
    /// </summary>
    private void Attack()
    {
        if (!target) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= rangeAttack)
        {
            if (timerAttack >= cdAttack)
            {
                ani.SetTrigger("攻擊觸發");
                timerAttack = 0;
            }
            else
            {
                timerAttack += Time.deltaTime;
                ani.SetBool("走路開關", false);
            }

            LookAtTarget();
        }
    }

    /// <summary>
    /// 面向目標物件
    /// </summary>
    private void LookAtTarget()
    {
        Vector3 posTarget = target.position;        // 取得目標座標
        posTarget.y = transform.position.y;         // 將目標座標 Y 軸設定為與此怪物同樣
        Quaternion lookRotation = Quaternion.LookRotation(posTarget - transform.position);                  // 使用向量取得角度
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, turn * Time.deltaTime);      // 角度差值
    }
    #endregion
}
