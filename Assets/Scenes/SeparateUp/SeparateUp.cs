using UnityEditorInternal;
using UnityEngine;

public class SeparateUp : MonoBehaviour
{
    public Animator m_animator;
    public float m_fireAnimationTime = 0.5f;
    public float m_StopFireSpeed = 1f;
    private bool m_fired = false;
    private float m_firedTime = 0f;
    private float m_upLayerWeight = 0f;
    public Transform m_arrow;
    private Quaternion m_arrowInitRot;
    [Range(0.05f, 4f)]
    public float m_playSpeed = 1f;
    [Range(0f, 360f)]
    public float m_direction = 0f;

    void Start()
    {
        m_arrowInitRot = m_arrow.rotation;
    }

    void Update()
    {
        Time.timeScale = m_playSpeed;
        if (m_animator != null)
        {
            SetDirectionVar();
            CheckFireKey();
            
            // 발사중이면
            if (m_fired)
            {
                if ( (Time.time - m_firedTime) > m_fireAnimationTime )
                {
                    StopFire();
                }
            }
        }
    }

    private void SetDirectionVar()
    {
        Quaternion dirQuat = Quaternion.Euler(0f, m_direction, 0f);
        Vector3 dirVec = dirQuat * Vector3.forward;
        m_animator.SetFloat("WalkHor", dirVec.x);
        m_animator.SetFloat("WalkVer", dirVec.z);

        if (m_arrow != null)
        {
            m_arrow.rotation = dirQuat * m_arrowInitRot;
        }
    }

    private void CheckFireKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    public void Fire()
    {
        m_fired = true;
        m_upLayerWeight = 1f;
        m_firedTime = Time.time; // 발사 시간 기록
        if (m_animator != null)
        {
            m_animator.SetLayerWeight(1, m_upLayerWeight);
            m_animator.Play("WalkUp", 1, 0f);
        }    
    }

    private void StopFire()
    {
        if (m_animator != null)
        {
            if (m_upLayerWeight < 0f)
            {
                m_fired = false;
            }
            else
            {
                m_upLayerWeight -= Time.deltaTime * m_StopFireSpeed;
            }

            m_animator.SetLayerWeight(1, m_upLayerWeight);
        }
    }
}