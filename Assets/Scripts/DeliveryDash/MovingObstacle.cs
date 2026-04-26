using UnityEngine;

namespace DeliveryDash
{


    public class MovingObstacle : FallingObject
    {
        [SerializeField] private int damage = 1;

        public int Damage
        {
            get { return damage; }
            set
            {
                if (value < 0)
                {
                    damage = 0;
                }
                else
                {
                    damage = value;
                }
            }
        }





        public void SetupObstacle(GameManager game, float speed, float bottomY)
        {
            SetupFallingObject(game, speed, bottomY);
        }





        public override void TouchPlayer(PlayerController player)
        {
            player.TakeDamage(Damage);
            RemoveFromGame();
        }
    }
}
