using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public float MaxRotationX;
    public float MaxRotationY;
    public PlayerControll playerControll;
    private float startRotationX;
    private float startRotationY;

    private float mouseX;
    private float mouseY;


    // Start is called before the first frame update
    private void Awake()
    {
        startRotationX = transform.localEulerAngles.x;
        startRotationY = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseX = Mathf.Clamp(mouseX, -MaxRotationX, MaxRotationX);

        if (!playerControll.IsGrounded)
        {
            mouseY += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Mathf.Clamp(mouseY, -MaxRotationY, MaxRotationY);
        }
        else
        {
            mouseY = 0;
        }

        transform.localRotation = Quaternion.Euler(startRotationX + mouseX, startRotationY + mouseY , transform.localRotation.z);

    }
}
