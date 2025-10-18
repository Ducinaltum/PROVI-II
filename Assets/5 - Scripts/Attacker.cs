using System;
using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private Fireball m_fireballPrefab;
    [SerializeField] private float m_attackRate;
    [SerializeField] private float m_spawnDistance;

    private bool m_canAttack = true;
    WaitForSeconds m_cooldownTime;
    void Start()
    {
        m_cooldownTime = new(m_attackRate);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_canAttack)
        {
            Fireball fireball = ObjectPool<Fireball>.Instance.GetObject();
            if (fireball == null)
            {
                fireball = Instantiate(m_fireballPrefab);
            }
            Vector3 spawnPos = transform.position;
            fireball.Initialize(spawnPos, m_spawnDistance, Mathf.Sign(m_rb.linearVelocityX));
            m_canAttack = false;
            StartCoroutine(WaitForCoolDown());
        }
    }

    private IEnumerator WaitForCoolDown()
    {
        yield return m_cooldownTime;
        m_canAttack = true;
    }
}
