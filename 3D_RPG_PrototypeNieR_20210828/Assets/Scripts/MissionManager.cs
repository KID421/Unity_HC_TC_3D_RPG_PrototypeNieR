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
    [Header("當前關卡對話資料")]
    public DialogueData data;

    /// <summary>
    /// 任務管理器實體物件
    /// </summary>
    public static MissionManager instance;
    #endregion

    #region 欄位：私人
    #endregion

    #region 事件
    private void Awake()
    {
        instance = this;    // 實體物件 等於 此 物件
    }
    #endregion

    #region 方法：公開
    /// <summary>
    /// 將任務的狀態改為任務進行中
    /// </summary>
    public void ChangeStateToMissionning()
    {
        state = StateMission.Missionning;
    }

    /// <summary>
    /// 更新任務需求數量
    /// </summary>
    /// <param name="count">要更新的數量</param>
    public void UpdateMissionCount(int count)
    {
        data.countNeed -= count;

        if (data.countNeed == 0) MissionFinish();
    }
    #endregion

    #region 方法：私人
    /// <summary>
    /// 任務完成
    /// </summary>
    private void MissionFinish()
    {
        state = StateMission.MissionFinish;
        StartCoroutine(DialogueSystem.instance.ShowEveryDialogue(data.diaogueContentsFinish, false, true));
    }
    #endregion
}
