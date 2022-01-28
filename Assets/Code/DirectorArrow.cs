using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorArrow : MonoBehaviour
{
    private bool rotate = false;

    private float rotationSpeed;

    private void Update()
    {
        if (!rotate) return;

        transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed);
    }

    public void StartRotating(float speed)
    {
        rotate = true;
        rotationSpeed = speed;
    }

    public void StopRotating()
    {
        rotate = false;
    }

    public Vector3 GetDirection()
    {
        return transform.up;
    }
}
