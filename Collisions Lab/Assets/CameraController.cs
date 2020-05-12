using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float movementSpeed = 1;
    public float fastMovementSpeed = 1;
    public float rotationSpeed = 1;

    private float x_rot;
    private float y_rot;

    private void Start()
    {
        x_rot = transform.localEulerAngles.x;
        y_rot = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets input values from w,a,s,d,space and mouse rotation 
        float horizontalPos = Input.GetAxis("Horizontal"); 
        float forwardPos = Input.GetAxis("Vertical");
        float verticalPos = Input.GetAxis("Jump");
        float horizontalRotation = Input.GetAxis("Mouse X");
        float speed;
        float verticalRotation = Input.GetAxis("Mouse Y");

        if (Input.GetKey(KeyCode.LeftControl))
            speed = fastMovementSpeed;
        else
            speed = movementSpeed;

        UnityEngine.Vector3 forward_vector_change = new UnityEngine.Vector3(transform.forward.x,0,transform.forward.z);
        UnityEngine.Vector3 right_vector_change = new UnityEngine.Vector3(transform.right.x, 0, transform.right.z);

        transform.position += (forward_vector_change * forwardPos + right_vector_change * horizontalPos + UnityEngine.Vector3.up * verticalPos) * speed * Time.deltaTime;

        x_rot += verticalRotation * rotationSpeed;
        x_rot = Mathf.Clamp(x_rot, -20, 20);

        y_rot += horizontalRotation * rotationSpeed;
        y_rot = Mathf.Clamp(y_rot, -360, 360);

        transform.localEulerAngles = new UnityEngine.Vector3(-x_rot, y_rot, 0);        
    }
}
