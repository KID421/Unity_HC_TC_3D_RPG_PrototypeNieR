using Invector.vCharacterController;
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
    [Header("��v��")]
    public vThirdPersonCamera cam;
    #endregion

    #region �R�A���
    // �R�A��ƯS��
    // 1. ���|��ܦb�ݩʭ��O
    // 2. ���������ɤ��|�٭�
    // 3. �s���覡 - ���O.�R�A��ƦW��
    /// <summary>
    /// �O�_�ܨ��ATrue �N���ܨ���AFalse �N���ܨ��e
    /// </summary>
    public static bool isTransform;
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
            // �ܨ����L�� �ܬ������� �ۤϭ�
            isTransform = !isTransform;

            // �p�G �ܨ��e �O���� �N�N �ܨ��e���y�лP���� �]�w�P �ܨ���ۦP
            if (!goTransformBefore.activeInHierarchy)
            {
                goTransformBefore.transform.position = goTransformAfter.transform.position;
                goTransformBefore.transform.eulerAngles = goTransformAfter.transform.eulerAngles;
            }
            // �p�G �ܨ��� �O���� �N�N �ܨ��᪺�y�лP���� �]�w�P �ܨ��e�ۦP
            else if (!goTransformAfter.activeInHierarchy)
            {
                goTransformAfter.transform.position = goTransformBefore.transform.position;
                goTransformAfter.transform.eulerAngles = goTransformBefore.transform.eulerAngles;
            }

            goTransformBefore.SetActive(!goTransformBefore.activeInHierarchy);
            goTransformAfter.SetActive(!goTransformAfter.activeInHierarchy);

            // ��v�� �ؼ� �]�w�� �ثe��ܪ��ҫ�
            if (goTransformBefore.activeInHierarchy) cam.SetTarget(goTransformBefore.transform);
            else if (goTransformAfter.activeInHierarchy) cam.SetTarget(goTransformAfter.transform);
        }
    }
    #endregion
}
