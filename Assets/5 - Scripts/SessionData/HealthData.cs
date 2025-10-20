public class HealthData
{
    private int m_maxHealth;
    private int m_currentHealth;
    public float CurrentRatio => (float)m_currentHealth / m_maxHealth;
    public int CurrentHealth => m_currentHealth;
    public int MaxHealth => m_maxHealth;
    public bool IsDead => m_currentHealth <= 0;

    public HealthData(int maxHealth)
    {
        m_maxHealth = maxHealth;
        m_currentHealth = maxHealth;
    }

    public void TakeDamage() => m_currentHealth--;
    public void Heal() => m_currentHealth++;
}