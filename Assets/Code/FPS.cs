using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
    public class FPS : MonoBehaviour
    {
        [SerializeField] private Text Text;

        private int _counter;
        private float _time;
        private int _fps;

        private int _lastFPS = 0;

        #region UNITY

        private void Update()
        {
            Count();

            Display();
        }

        #endregion

        private void Count()
        {
            _time += Time.deltaTime;
            _counter++;
            if (_time > 1)
            {
                _fps = _counter;
                {
                    _lastFPS = _fps;
                }
                _counter = 0;
                _time = 0;
            }
        }


        private void Display()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"FPS: {_lastFPS}");
            Text.text = builder.ToString();
        }

    }
}