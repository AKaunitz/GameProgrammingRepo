using UnityEngine;

namespace DeliveryDash
{
    public abstract class GameActor : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;






        public float MoveSpeed
        {
            get { return moveSpeed; }
            set
            {
                if (value < 0f)
                {
                    moveSpeed = 0f;
                }
                else
                {
                    moveSpeed = value;
                }
            }
        }





        protected GameManager Game { get; set; }





        public virtual void Setup(GameManager game, float speed)
        {
            Game = game;
            MoveSpeed = speed;
        }






        protected void Move(Vector2 direction)
        {
            if (direction.magnitude > 1f)
            {
                direction.Normalize();
            }

            transform.position += (Vector3)(direction * MoveSpeed * Time.deltaTime);
        }





        protected void RemoveFromGame()
        {
            Destroy(gameObject);
        }
    }





    public abstract class FallingObject : GameActor
    {



        [SerializeField] private float despawnY = -5.8f;




        public float DespawnY
        {
            get { return despawnY; }
            set { despawnY = value; }
        }





        public void SetupFallingObject(GameManager game, float speed, float bottomY)
        {
            Setup(game, speed);
            DespawnY = bottomY;
        }





        protected virtual void Update()
        {
            if (Game != null && !Game.IsPlaying)
            {
                return;
            }

            Move(Vector2.down);

            if (transform.position.y < DespawnY)
            {
                RemoveFromGame();
            }
        }




        public abstract void TouchPlayer(PlayerController player);
    }
}
