using System.Collections;
using UnityEngine;

namespace Assets.Code
{
    public class SpriteTest : MonoBehaviour
    {

        private float start = -1.25F;
        public SpriteRenderer render;
        private Map map;


        private void Awake()
        {
            map = new Map(Color.black, Color.white);
        }

        private void Update()
        {
            start += Time.deltaTime / 5;
            render.sprite = map.GetSprite();
            map.Pain(new Vector2(0, start), 15, 1);
            for (int i = 0; i < 6; i++)
            {
                map.CheckConnected(new Vector2(0, -.8F), 1);
            }
            
        }

    }
}