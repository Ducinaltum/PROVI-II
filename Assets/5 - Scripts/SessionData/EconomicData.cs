public class EconomicData
{
    private int m_coins = 0;
    public int CurrentCoins => m_coins;

    public void AddCoins(int ammount = 1)
    {
        m_coins += ammount;
    }
}