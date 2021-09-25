using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// AI �� 3D �Ҧ�
/// ���ݡB�樫�B�l�ܪ��a�B�����B���˻P���`
/// </summary>
public class AIBase3D : MonoBehaviour
{
    #region ���G���}
    [Header("���ʳt��"), Range(0, 500)]
    public float speed = 3;
    [Header("�����O"), Range(0, 500)]
    public float attack = 50;
    [Header("��q"), Range(0, 500)]
    public float hp = 300;
    [Header("�l�ܽd��"), Range(0, 100)]
    public float rangeTrack = 10;
    #endregion

    #region ���G�p�H
    private Animator ani;
    private NavMeshAgent nav;
    #endregion

    #region �ƥ�
    private void Awake()
    {
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeTrack);
    }
    #endregion

    #region ��k�G���}
    #endregion

    #region ��k�G�p�H
    #endregion
}
