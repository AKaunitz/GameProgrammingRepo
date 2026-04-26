using UnityEngine;
using UnityEngine.InputSystem;

namespace DeliveryDash
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int targetScore = 10;
        [SerializeField] private int maxLives = 3;
        [SerializeField] private float playerSpeed = 5f;
        [SerializeField] private float obstacleDelay = 1.1f;
        [SerializeField] private float packageDelay = 1.7f;





        private float spawnY = 5.8f;
        private float despawnY = -5.8f;
        private float[] lanes = { -3f, -1.5f, 0f, 1.5f, 3f };





        private Transform actorRoot;
        private HUDView hud;
        private Sprite playerSprite;
        private Sprite[] trafficSprites;
        private AudioSource soundSource;
        private AudioClip pickupClip;
        private AudioClip hitClip;
        private AudioClip winClip;
        private AudioClip gameOverClip;
        private AudioClip restartClip;
        private int score;
        private bool gameEnded;
        private float obstacleTimer;
        private float packageTimer;







        public bool IsPlaying
        {
            get { return !gameEnded; }
        }





        private void Start()
        {
            SetupCamera();
            LoadAssets();
            SetupAudio();
            BuildWorld();
            BuildHud();
            StartNewGame();
        }






        private void Update()
        {

            if (gameEnded)
            {
                Keyboard keyboard = Keyboard.current;

                if (keyboard != null && keyboard.rKey.wasPressedThisFrame)
                {
                    PlaySound(restartClip);
                    StartNewGame();
                }

                return;
            }



            obstacleTimer -= Time.deltaTime;
            packageTimer -= Time.deltaTime;




            if (obstacleTimer <= 0f)
            {
                SpawnObstacle();

                obstacleTimer = obstacleDelay - score * 0.03f;

                if (obstacleTimer < 0.5f)
                {
                    obstacleTimer = 0.5f;
                }
            }




            if (packageTimer <= 0f)
            {
                SpawnPackage();
                packageTimer = packageDelay;
            }
        }








        public void AddScore(int amount)
        {

            if (gameEnded)
            {
                return;
            }



            score += amount;
            hud.SetScore(score);
            hud.SetMessage("Package collected!");
            PlaySound(pickupClip);



            if (score >= targetScore)
            {
                EndGame("All packages delivered. You win!", winClip);
            }
        }








        public void PlayerLost()
        {
            EndGame("The van is broken. GG!", gameOverClip);
        }







        public void PlayerHit(int lives)
        {
            hud.SetLives(lives);
            hud.SetMessage("Hit received! Lives left: " + lives);
            PlaySound(hitClip);
        }









        private void StartNewGame()
        {
            ClearActors();

            score = 0;
            gameEnded = false;
            obstacleTimer = 0.5f;
            packageTimer = 1f;

            SpawnPlayer();
            hud.SetScore(score);
            hud.SetLives(maxLives);
            hud.SetMessage("Collect " + targetScore + " packages and avoid traffic. Good Luck :)");

        }








        private void SpawnPlayer()
        {


            GameObject playerObject = CreateCar("Player", new Vector3(0f, -3.7f, 0f), new Vector2(0.15f, 0.15f), Color.white, 5, playerSprite);
            MakeTrigger(playerObject, false);



            PlayerController player = playerObject.AddComponent<PlayerController>();
            player.SetupPlayer(this, new Vector2(-3.35f, -4.3f), new Vector2(3.35f, 4.3f), maxLives, playerSpeed);

        }








        private void SpawnObstacle()
        {

            float speed = UnityEngine.Random.Range(2.4f, 3.8f) + score * 0.08f;
            Sprite trafficSprite = trafficSprites[UnityEngine.Random.Range(0, trafficSprites.Length)];


            GameObject obstacleObject = CreateCar("Car",new Vector3(RandomLane(), spawnY, 0f), new Vector2(0.15f, 0.15f), Color.white, 4, trafficSprite);
            MakeTrigger(obstacleObject, false);


            obstacleObject.AddComponent<MovingObstacle>().SetupObstacle(this, speed, despawnY);
        }








        private void SpawnPackage()
        {
            GameObject packageObject = ShapeFactory.CreateShape("Package", new Vector3(RandomLane(), spawnY, 0f), new Vector2(0.55f, 0.55f), new Color(0.12f, 0.82f, 0.12f), 6, actorRoot);

            MakeTrigger(packageObject, true);
            packageObject.AddComponent<PackagePickup>().SetupPickup(this, 1, 1.6f, despawnY);

        }







        private GameObject CreateCar(string name, Vector3 position, Vector2 size, Color color, int order, Sprite sprite)
        {
            return ShapeFactory.CreateShape(name, position, size, color, order, actorRoot, sprite);
        }









        private void MakeTrigger(GameObject actor, bool circle)
        {

            Collider2D collider;



            if (circle)
            {
                collider = actor.AddComponent<CircleCollider2D>();
            }



            else
            {
                BoxCollider2D box = actor.AddComponent<BoxCollider2D>();
                SpriteRenderer renderer = actor.GetComponent<SpriteRenderer>();

                if (renderer != null && renderer.sprite != null)
                {
                    box.size = renderer.sprite.bounds.size;
                }

                collider = box;
            }



            collider.isTrigger = true;

            Rigidbody2D body = actor.AddComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Kinematic;
            body.gravityScale = 0f;
            body.freezeRotation = true;
        }








        private void EndGame(string message, AudioClip sound)
        {

            if (gameEnded)
            {
                return;
            }

            gameEnded = true;
            hud.SetMessage(message + "\nPress R to restart :)");
            PlaySound(sound);
        }







        private void ClearActors()
        {

            if (actorRoot == null)
            {
                return;
            }


            for (int i = actorRoot.childCount - 1; i >= 0; i--)
            {
                Destroy(actorRoot.GetChild(i).gameObject);
            }

        }






        private float RandomLane()
        {

            int randomIndex = UnityEngine.Random.Range(0, lanes.Length);
            return lanes[randomIndex];

        }






        private void LoadAssets()
        {

            playerSprite = LoadSprite("DeliveryDash/Cars/PlayerCar");

            trafficSprites = new Sprite[3];
            trafficSprites[0] = LoadSprite("DeliveryDash/Cars/TrafficCar");
            trafficSprites[1] = LoadSprite("DeliveryDash/Cars/TrafficCar2");
            trafficSprites[2] = LoadSprite("DeliveryDash/Cars/TrafficCar3");

            pickupClip = Resources.Load<AudioClip>("DeliveryDash/Sounds/PackagePickup");
            hitClip = Resources.Load<AudioClip>("DeliveryDash/Sounds/Hit");
            winClip = Resources.Load<AudioClip>("DeliveryDash/Sounds/Win");
            gameOverClip = Resources.Load<AudioClip>("DeliveryDash/Sounds/GameOver");
            restartClip = Resources.Load<AudioClip>("DeliveryDash/Sounds/Restart");

        }





        private Sprite LoadSprite(string path)
        {

            Texture2D texture = Resources.Load<Texture2D>(path);



            if (texture == null)
            {
                return null;
            }



            Rect rect = new Rect(0f, 0f, texture.width, texture.height);
            return Sprite.Create(texture, rect, Vector2.one * 0.5f, 100f);

        }







        private void SetupAudio()
        {

            soundSource = gameObject.AddComponent<AudioSource>();
            soundSource.playOnAwake = false;
            soundSource.volume = 0.8f;


            AudioSource musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.clip = Resources.Load<AudioClip>("DeliveryDash/Music/Music");
            musicSource.loop = true;
            musicSource.volume = 0.25f;


            if (musicSource.clip != null)
            {
                musicSource.Play();
            }

        }






        private void PlaySound(AudioClip clip)
        {

            if (clip != null)
            {
                soundSource.PlayOneShot(clip);
            }

        }





        private void SetupCamera()
        {

            Camera camera = Camera.main;

            if (camera == null)
            {
                GameObject cameraObject = new GameObject("Main Camera");
                cameraObject.tag = "MainCamera";
                camera = cameraObject.AddComponent<Camera>();
            }

            camera.orthographic = true;
            camera.orthographicSize = 5.2f;
            camera.transform.position = new Vector3(0f, 0f, -10f);
            camera.backgroundColor = new Color(0.05f, 0.08f, 0.09f);
        }







        private void BuildWorld()
        {

            Transform worldRoot = new GameObject("World").transform;
            worldRoot.SetParent(transform);

            ShapeFactory.CreateShape("Grass", Vector3.zero, new Vector2(12f, 11f), new Color(0.08f, 0.14f, 0.12f), -20, worldRoot);
            ShapeFactory.CreateShape("Road", Vector3.zero, new Vector2(7.5f, 11f), new Color(0.13f, 0.14f, 0.15f), -19, worldRoot);




            for (int i = 1; i < lanes.Length; i++)
            {
                float lineX = (lanes[i - 1] + lanes[i]) * 0.5f;

                for (int y = -5; y <= 5; y += 2)
                {
                    ShapeFactory.CreateShape("Lane Line", new Vector3(lineX, y, 0f), new Vector2(0.08f, 0.9f), new Color(0.85f, 0.85f, 0.72f), -18, worldRoot);
                }
            }




            actorRoot = new GameObject("Actors").transform;
            actorRoot.SetParent(transform);

        }





        private void BuildHud()
        {

            hud = new GameObject("HUD").AddComponent<HUDView>();
            hud.transform.SetParent(transform);

        }




    }
}
