using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 對話系統
/// 1. 決定說話者名稱
/// 2. 決定對話內容 - 可以有多段
/// 3. 顯示每個段落對話完成的圖示動態效果
/// </summary>
public class DialogueSystem : MonoBehaviour
{
    #region 欄位
    [Header("對話資料")]
    public DialogueData data;
    [Header("對話間隔"), Range(0, 3)]
    public float interval = 0.2f;
    [Header("對話完成圖示")]
    public GameObject goFinishIcon;
    [Header("文字介面：說話者名稱、對話文字內容")]
    public Text textTalker;
    public Text textContent;

    /// <summary>
    /// 對話系統畫布：群組元件
    /// </summary>
    private CanvasGroup groupDialogue;
    #endregion

    [Header("繼續對話的按鍵")]
    public KeyCode kcContinute = KeyCode.Space;
    [Header("打字音效")]
    public AudioClip soundType;
    [Header("打字音量"), Range(0, 2)]
    public float volume = 1;
    [Header("任務管理器")]
    public MissionManager missionManager;

    private AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        groupDialogue = transform.GetChild(0).GetComponent<CanvasGroup>();

        StartDialogue();
    }

    /// <summary>
    /// 開始對話
    /// </summary>
    private void StartDialogue()
    {
        StartCoroutine(ShowEveryDialogue());
    }

    /// <summary>
    /// 顯示每段對話：並在段落之間等待玩家按繼續按鍵
    /// </summary>
    private IEnumerator ShowEveryDialogue()
    {
        groupDialogue.alpha = 1;                                                // 顯示對話畫布 - 透明度為 1
        textTalker.text = data.diaogueTalkerName;                               // 更新對話者名稱
        textContent.text = "";                                                  // 對話內容清空

        for (int i = 0; i < data.diaogueContents.Length; i++)                   // 迴圈執行每個段落
        {
            for (int j = 0; j < data.diaogueContents[i].Length; j++)            // 迴圈執行每個段落內的每一個字
            {
                textContent.text += data.diaogueContents[i][j];                 // 更新對話內容
                aud.PlayOneShot(soundType, volume);                             // 播放音效
                yield return new WaitForSeconds(interval);                      // 打字間隔
            }

            goFinishIcon.SetActive(true);                                       // 每個段話完成後顯示完成圖示

            while (!Input.GetKeyDown(kcContinute))                              // 等待 玩家按繼續按鈕 - 使用 null 為每幀的時間
            {
                yield return null;
            }

            textContent.text = "";                                              // 玩家按下空白鍵後清空對話內容
            goFinishIcon.SetActive(false);                                      // 關閉完成圖示

            if (i == data.diaogueContents.Length - 1)
            {
                groupDialogue.alpha = 0;                                        // 如果對話段落已經結束就關閉對話介面
                missionManager.ChangeStateToMissionning();                      // 跟任務管理器說進入進行中
            }
        }
    }
}
