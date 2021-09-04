using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// ��ܨt��
/// 1. �M�w���ܪ̦W��
/// 2. �M�w��ܤ��e - �i�H���h�q
/// 3. ��ܨC�Ӭq����ܧ������ϥܰʺA�ĪG
/// </summary>
public class DialogueSystem : MonoBehaviour
{
    #region ���
    [Header("��ܸ��")]
    public DialogueData data;
    [Header("��ܶ��j"), Range(0, 3)]
    public float interval = 0.2f;
    [Header("��ܧ����ϥ�")]
    public GameObject goFinishIcon;
    [Header("��r�����G���ܪ̦W�١B��ܤ�r���e")]
    public Text textTalker;
    public Text textContent;

    /// <summary>
    /// ��ܨt�εe���G�s�դ���
    /// </summary>
    private CanvasGroup groupDialogue;
    #endregion

    private void Start()
    {
        groupDialogue = transform.GetChild(0).GetComponent<CanvasGroup>();

        StartDialogue();
    }

    /// <summary>
    /// �}�l���
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
