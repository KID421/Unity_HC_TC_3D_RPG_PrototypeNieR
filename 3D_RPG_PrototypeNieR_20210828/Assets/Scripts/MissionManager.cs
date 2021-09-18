using UnityEngine;

/// <summary>
/// 任務管理器
/// </summary>
public class MissionManager : MonoBehaviour
{
    #region 欄位：公開
    /// <summary>
    /// 定義列舉，跟系統說明列舉名稱以及包含哪些選項
    /// 任務狀態：接任務前、任務進行中、完成任務
    /// </summary>
    public enum StateMission
    {
        MissionBefore, Missionning, MissionFinish
    }

    [Header("任務狀態")]
    public StateMission state;
    #endregion

    #region 欄位：私人
    #endregion

    #region 事件
    #endregion

    #region 方法：公開
    /// <summary>
    /// 將任務的狀態改為任務進行中
    /// </summary>
    public void ChangeStateToMissionning()
    {
        state = StateMission.Missionning;
    }
    #endregion
}
