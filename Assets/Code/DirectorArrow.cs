using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorArrow : MonoBehaviour
{
    private bool rotate = false;
    private bool power = false;
    private bool ascend = false;

    private float rotationSpeed;
    private float minimumSize = 0.5f;
    private float maximumSize = 1;
    private float time = 0;

    private Vector3 originalSize;

    public float CurrentPower = 2;

    private void Start()
    {
        originalSize = transform.localScale;
    }

    private void Update()
    {
        if (rotate)
            transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed);

        if (power)
        {
            time += Time.deltaTime;
            float sinValue = (Mathf.Sin(time) + 2) / 2;
            CurrentPower = sinValue * 2;

            transform.localScale = Vector3.one * sinValue;


        }

    }

    public void Rotate(float speed)
    {
        rotate = true;
        rotationSpeed = speed;
    }

    public void StopRotating()
    {
        rotate = false;
    }

    public void StartPowering()
    { 
        power = true;
    }

    public void StopPowering()
    {
        power = false;
    }
}
