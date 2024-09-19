using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpinObject : MonoBehaviour
{
    public float rotationSpeed = 100f; 
    public Vector3 rotationAxis = Vector3.up; 
    void Update()
    {
       
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
