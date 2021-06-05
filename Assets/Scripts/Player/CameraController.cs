using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public float MaxRotationX;
    public float MaxRotationY;

    [SerializeField]
    private float startRotationX;
    [SerializeField]

    private float startRotationY;

    private float mouseX;
    private float mouseY;

    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("start");
        Debug.Log(transform.localEulerAngles.x);
        startRotationX = transform.localEulerAngles.x;
        Debug.Log(transform.localEulerAngles.y);
        startRotationY = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.localEulerAngles.x);
        mouseX += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseX = Mathf.Clamp(mouseX, -MaxRotationX, MaxRotationX);

        mouseY += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -MaxRotationY, MaxRotationY);

        transform.localRotation = Quaternion.Euler(startRotationX + mouseX, startRotationY + mouseY , transform.localRotation.z);

    }
}
