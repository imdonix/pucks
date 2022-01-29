using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorArrow : MonoBehaviour
{
    [SerializeField] private Transform graphicTransform;

    private bool rotate = false;
    private bool power = false;
    private bool ascend = false;
    private bool holdRotation = false;

    private float rotationSpeed;
    private float minimumSize = 0.5f;
    private float maximumSize = 1;
    private float time = 0;

    private Quaternion rotationToHold;

    public float CurrentPower = 3;

    private void Update()
    {
        if (rotate)
            transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);

        if (power)
        {
            time += Time.deltaTime;
            float sinValue = (Mathf.Sin(time) + 2) / 2;
            CurrentPower = sinValue * 4;

            graphicTransform.localScale = Vector3.one * sinValue;
        }

        if (holdRotation)
            transform.rotation = rotationToHold;
    }

    public void ResetArrow()
    {
        transform.rotation = Quaternion.identity;
        rotate = false;
        power = false;
        ascend = false;
        holdRotation = false;
        graphicTransform.localScale = Vector3.one;
    }

    public void HoldRotation()
    {
        rotationToHold = transform.rotation;

        holdRotation = true;
    }

    public void Rotate(float speed)
    {
        rotate = true;
        rotationSpeed = speed;
    }

    public void StopRotating()
    {
        rotate = false;
        HoldRotation();
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
