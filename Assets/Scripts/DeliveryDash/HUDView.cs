using UnityEngine;

namespace DeliveryDash
{
    public class HUDView : MonoBehaviour
    {


        private int score;
        private int lives;
        private string message = "";
        private GUIStyle labelStyle;
        private GUIStyle centerStyle;




        public void SetScore(int newScore)
        {
            score = newScore;
        }



        public void SetLives(int newLives)
        {
            lives = newLives;
        }




        public void SetMessage(string newMessage)
        {
            message = newMessage;
        }




        private void OnGUI()
        {
            CreateStyles();

            GUI.Label(new Rect(20f, 20f, 300f, 50f), "Score: " + score, labelStyle);
            GUI.Label(new Rect(Screen.width - 220f, 20f, 200f, 50f), "Lives: " + lives, labelStyle);
            GUI.Label(new Rect(0f, 75f, Screen.width, 80f), message, centerStyle);
            GUI.Label(new Rect(0f, Screen.height - 50f, Screen.width, 40f), "Move: WASD or Arrows    Restart: R", centerStyle);
        }





        private void CreateStyles()
        {
            if (labelStyle != null)
            {
                return;
            }

            labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = 28;
            labelStyle.normal.textColor = Color.white;

            centerStyle = new GUIStyle(labelStyle);
            centerStyle.alignment = TextAnchor.MiddleCenter;
            centerStyle.fontSize = 24;
            centerStyle.wordWrap = true;
        }




    }
}
