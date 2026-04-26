using UnityEngine;

// Hello World 

namespace DeliveryDash
{
    public static class GameBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CreateGame()
        {
            if (Object.FindAnyObjectByType<GameManager>() != null)
            {
                return;
            }

            GameObject gameObject = new GameObject("Delivery Dash");
            gameObject.AddComponent<GameManager>();
        }
    }
}
