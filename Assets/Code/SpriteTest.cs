using System.Collections;
using UnityEngine;

namespace Assets.Code
{
    public class SpriteTest : MonoBehaviour
    {

        public Vector2 point = Vector2.zero;
        public float radius = 5;
        public int type = 1;
        public SpriteRenderer render;
        private Map map;


        private void Awake()
        {
            map = new Map(Color.black, Color.white);
        }

        private void Update()
        {
            render.sprite = map.GetSprite();
            map.Pain(point, radius, type);
            for (int i = 0; i < 6; i++)
            {
                map.CheckConnected(new Vector2(0, .1F), 0);
            }
            
        }

    }
}