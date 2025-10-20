public class SessionData
{
    public HealthData HealthData { get; private set; }
    public EconomicData EconomicData { get; private set; }
    public SessionData(int maxHealth)
    {
        HealthData = new(maxHealth);
        EconomicData = new();
        ServiceLocator.RegisterService(this);
    }
}