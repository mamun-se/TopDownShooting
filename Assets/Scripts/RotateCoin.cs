using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAngle;
    void Update()
    {
        transform.Rotate(rotationAngle * Time.deltaTime);
    }
}
