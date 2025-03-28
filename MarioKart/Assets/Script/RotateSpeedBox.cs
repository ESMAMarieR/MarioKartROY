using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBoxRandom : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 50, 0);

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}