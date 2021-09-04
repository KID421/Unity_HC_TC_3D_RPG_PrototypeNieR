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

    private void Start()
    {
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

    private IEnumerator ShowEveryDialogue()
    {
        for (int i = 0; i < data.diaogueContents.Length; i++)
        {
            for (int j = 0; j < data.diaogueContents[i].Length; j++)
            {
                print(data.diaogueContents[i][j]);
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
