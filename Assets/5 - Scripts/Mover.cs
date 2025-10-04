
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] float velocidad = 5f;
    [SerializeField] private float m_jumpStrenghtBase = 2f;
    [SerializeField] private float m_jumpStrenghtDecayCoeficent = 0.8f;
    private float m_actualJumpStrenght = 5f;

    private float moverHorizontal;
    private Vector2 direccion;
    private bool m_jumping = false;
    private Rigidbody2D miRigidbody2D;

    // Codigo ejecutado cuando el objeto se activa en el nivel
    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Codigo ejecutado en cada frame del juego (Intervalo variable)
    private void Update()
    {
        moverHorizontal = Input.GetAxis("Horizontal");
        direccion = new Vector2(moverHorizontal, 0f);
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
        miRigidbody2D.AddForce(direccion * velocidad);
        if (m_jumping)
        {
            miRigidbody2D.AddForce(Vector2.up * m_actualJumpStrenght, ForceMode2D.Impulse);
            m_actualJumpStrenght *= m_jumpStrenghtDecayCoeficent;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_actualJumpStrenght = m_jumpStrenghtBase;
        m_jumping = false;
    }
}