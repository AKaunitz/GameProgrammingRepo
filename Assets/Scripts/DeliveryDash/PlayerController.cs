using UnityEngine;
using UnityEngine.InputSystem;

namespace DeliveryDash
{
    public class PlayerController : GameActor, IDamageable
    {


        [SerializeField] private int health = 3;



        private Vector2 minBounds;
        private Vector2 maxBounds;




        public int Health
        {
            get { return health; }
            set
            {
                if (value < 0)
                {
                    health = 0;
                }
                else
                {
                    health = value;
                }
            }
        }




        public void SetupPlayer(GameManager game, Vector2 newMinBounds, Vector2 newMaxBounds, int startHealth, float speed)
        {

            Setup(game, speed);
            minBounds = newMinBounds;
            maxBounds = newMaxBounds;
            Health = startHealth;

        }





        private void Update()
        {

            if (Game != null && !Game.IsPlaying)
            {
                return;
            }

            MovePlayer(ReadMovementInput());
        }





        private void MovePlayer(Vector2 direction)
        {
            base.Move(direction);


            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
            position.y = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);
            transform.position = position;


        }





        public void TakeDamage(int amount)
        {


            if (amount <= 0)
            {
                return;
            }

            Health -= amount;


            if (Health <= 0)
            {
                Game.PlayerLost();
                return;
            }


            Game.PlayerHit(Health);
        }






        private Vector2 ReadMovementInput()
        {

            Keyboard keyboard = Keyboard.current;

            if (keyboard == null)
            {
                return Vector2.zero;
            }

            Vector2 direction = Vector2.zero;




            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            {
                direction.x -= 1f;
            }

            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            {
                direction.x += 1f;
            }

            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            {
                direction.y += 1f;
            }

            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            {
                direction.y -= 1f;
            }



            return direction;
        }







        private void OnTriggerEnter2D(Collider2D other)
        {

            FallingObject fallingObject = other.GetComponent<FallingObject>();

            if (fallingObject != null)
            {
                fallingObject.TouchPlayer(this);
            }


        }


    }
}
