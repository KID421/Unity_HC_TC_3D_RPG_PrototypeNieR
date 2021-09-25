using Invector.vCharacterController;
using UnityEngine;

/// <summary>
/// 變身系統
/// </summary>
public class TransformSystem : MonoBehaviour
{
    #region 欄位：公開
    [Header("玩家變身前後模型物件")]
    public GameObject goTransformBefore;
    public GameObject goTransformAfter;
    [Header("攝影機")]
    public vThirdPersonCamera cam;
    #endregion

    #region 靜態資料
    // 靜態資料特性
    // 1. 不會顯示在屬性面板
    // 2. 切換場景時不會還原
    // 3. 存取方式 - 類別.靜態資料名稱
    /// <summary>
    /// 是否變身，True 代表變身後，False 代表變身前
    /// </summary>
    public static bool isTransform;
    #endregion

    #region 欄位：私人
    #endregion

    #region 事件
    private void Update()
    {
        TransformSwitch();
    }
    #endregion

    #region 方法：私人
    /// <summary>
    /// 變身切換
    /// 按下變身按鈕 R 對調模型
    /// </summary>
    private void TransformSwitch()
    {
        // 按下 R 鍵，變身前後模型 顯示狀態 與 原本 顛倒
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 變身布林值 變為本身的 相反值
            isTransform = !isTransform;

            // 如果 變身前 是隱藏 就將 變身前的座標與角度 設定與 變身後相同
            if (!goTransformBefore.activeInHierarchy)
            {
                goTransformBefore.transform.position = goTransformAfter.transform.position;
                goTransformBefore.transform.eulerAngles = goTransformAfter.transform.eulerAngles;
            }
            // 如果 變身後 是隱藏 就將 變身後的座標與角度 設定與 變身前相同
            else if (!goTransformAfter.activeInHierarchy)
            {
                goTransformAfter.transform.position = goTransformBefore.transform.position;
                goTransformAfter.transform.eulerAngles = goTransformBefore.transform.eulerAngles;
            }

            goTransformBefore.SetActive(!goTransformBefore.activeInHierarchy);
            goTransformAfter.SetActive(!goTransformAfter.activeInHierarchy);

            // 攝影機 目標 設定為 目前顯示的模型
            if (goTransformBefore.activeInHierarchy) cam.SetTarget(goTransformBefore.transform);
            else if (goTransformAfter.activeInHierarchy) cam.SetTarget(goTransformAfter.transform);
        }
    }
    #endregion
}
