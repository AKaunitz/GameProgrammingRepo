using UnityEngine;

namespace DeliveryDash
{



    public class PackagePickup : FallingObject, ICollectible
    {
        [SerializeField] private int scoreValue = 1;

        public int ScoreValue
        {
            get { return scoreValue; }
            set
            {
                if (value < 1)
                {
                    scoreValue = 1;
                }
                else
                {
                    scoreValue = value;
                }
            }
        }





        public void SetupPickup(GameManager game, int value, float speed, float bottomY)
        {
            SetupFallingObject(game, speed, bottomY);
            ScoreValue = value;
        }





        protected override void Update()
        {
            base.Update();
            transform.Rotate(0f, 0f, 90f * Time.deltaTime);
        }






        public override void TouchPlayer(PlayerController player)
        {
            Collect(player);
        }




        public void Collect(PlayerController player)
        {
            Game.AddScore(ScoreValue);
            RemoveFromGame();
        }
    }
}
