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

            // ��v�� �ؼ� �]�w�� �ثe��ܪ��ҫ�
            if (goTransformBefore.activeInHierarchy) cam.SetTarget(goTransformBefore.transform);
            else if (goTransformAfter.activeInHierarchy) cam.SetTarget(goTransformAfter.transform);

            // �P�B�y��
            // goTransformBefore.transform.position = goTransformAfter.transform.position;
            // goTransformAfter.transform.position = goTransformBefore.transform.position;


            print("�ܨ��e�y�СG" + goTransformBefore.transform.position);
            print("�ܨ���y�СG" + goTransformAfter.transform.position);
        }
    }
    #endregion
}
