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
            goTransformBefore.SetActive(!goTransformBefore.activeInHierarchy);
            goTransformAfter.SetActive(!goTransformAfter.activeInHierarchy);

            // 攝影機 目標 設定為 目前顯示的模型
            if (goTransformBefore.activeInHierarchy) cam.SetTarget(goTransformBefore.transform);
            else if (goTransformAfter.activeInHierarchy) cam.SetTarget(goTransformAfter.transform);

            // 同步座標
            // goTransformBefore.transform.position = goTransformAfter.transform.position;
            // goTransformAfter.transform.position = goTransformBefore.transform.position;


            print("變身前座標：" + goTransformBefore.transform.position);
            print("變身後座標：" + goTransformAfter.transform.position);
        }
    }
    #endregion
}
