using UnityEngine;

/// <summary>
/// �ܨ��t��
/// </summary>
public class TransformSystem : MonoBehaviour
{
    #region ���G���}
    [Header("���a�ܨ��e��ҫ�����")]
    public GameObject goTransformBefore;
    public GameObject goTransformAfter;
    #endregion

    #region ���G�p�H
    #endregion

    #region �ƥ�
    private void Update()
    {
        TransformSwitch();
    }
    #endregion

    #region ��k�G�p�H
    /// <summary>
    /// �ܨ�����
    /// ���U�ܨ����s R ��ռҫ�
    /// </summary>
    private void TransformSwitch()
    {
        // ���U R ��A�ܨ��e��ҫ� ��ܪ��A �P �쥻 �A��
        if (Input.GetKeyDown(KeyCode.R))
        {
            goTransformBefore.SetActive(!goTransformBefore.activeInHierarchy);
            goTransformAfter.SetActive(!goTransformAfter.activeInHierarchy);
        }
    }
    #endregion
}
