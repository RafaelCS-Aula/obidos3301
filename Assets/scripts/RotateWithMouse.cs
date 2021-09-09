using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    public float torque = 1.0f;
    private float baseAngle = 0.0f;
    public Rigidbody rb;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
 
    void OnMouseDown()
    {
 
    }
 
    void OnMouseDrag()
    {
        rb.AddTorque(Vector3.up * torque * -Input.GetAxis("Mouse X"));
 
        rb.AddTorque(transform.right * torque * Input.GetAxis("Mouse Y"));

        print("SPIN");
    }
}
