using UnityEngine;

namespace DeliveryDash
{
    public static class ShapeFactory
    {


        private static Sprite squareSprite;





        public static GameObject CreateShape(string name, Vector3 position, Vector2 size, Color color, int order, Transform parent, Sprite sprite = null)
        {


            GameObject actor = new GameObject(name);
            actor.transform.SetParent(parent);
            actor.transform.position = position;
            SetSprite(actor, size, color, order, sprite);
            return actor;


        }







        public static void SetSprite(GameObject actor, Vector2 size, Color color, int order, Sprite sprite = null)
        {



            actor.transform.localScale = size;

            SpriteRenderer renderer = actor.GetComponent<SpriteRenderer>();





            if (renderer == null)
            {
                renderer = actor.AddComponent<SpriteRenderer>();
            }

            if (sprite != null)
            {
                renderer.sprite = sprite;
            }
            else
            {
                renderer.sprite = GetSquareSprite();
            }



            renderer.color = color;
            renderer.sortingOrder = order;

        }







        private static Sprite GetSquareSprite()
        {



            if (squareSprite == null)
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.SetPixel(0, 0, Color.white);
                texture.Apply();
                squareSprite = Sprite.Create(texture, new Rect(0f, 0f, 1f, 1f), Vector2.one * 0.5f, 1f);
            }



            return squareSprite;
        }



    }
}
