namespace DeliveryDash
{

    public interface ICollectible
    {
        int ScoreValue { get; }
        void Collect(PlayerController player);
    }






    public interface IDamageable
    {
        int Health { get; }
        void TakeDamage(int amount);
    }
}
