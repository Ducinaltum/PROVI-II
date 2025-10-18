
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] float velocidad = 5f;
    [SerializeField] private float m_jumpStrenghtBase = 2f;
    [SerializeField] private float m_jumpStrenghtDecayCoeficent = 0.8f;
    private Rigidbody2D m_rigidbody2D;
    private float m_actualJumpStrenght = 5f;
    private float m_moverHorizontal;
    private Vector2 m_direccion;
    private bool m_jumping = false;

    // Codigo ejecutado cuando el objeto se activa en el nivel
    private void OnEnable()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Codigo ejecutado en cada frame del juego (Intervalo variable)
    private void Update()
    {
        m_moverHorizontal = Input.GetAxis("Horizontal");
        m_direccion = new Vector2(m_moverHorizontal, 0f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_jumping = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) && m_jumping)
        {
            m_jumping = false;
        }
    }

    private void FixedUpdate()
    {
        m_rigidbody2D.AddForce(m_direccion * velocidad);
        if (m_jumping)
        {
            m_rigidbody2D.AddForce(Vector2.up * m_actualJumpStrenght, ForceMode2D.Impulse);
            m_actualJumpStrenght *= m_jumpStrenghtDecayCoeficent;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_actualJumpStrenght = m_jumpStrenghtBase;
        m_jumping = false;
    }
}