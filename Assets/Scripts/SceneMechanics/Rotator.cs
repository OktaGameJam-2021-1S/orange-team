using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotator : MonoBehaviour
{
    public Vector3 Range1;
    public Vector3 Range2;
    Vector3 RotationSpeed;
    private void OnEnable()
    {
        RotationSpeed = new Vector3(
            Random.Range(Range1.x, Range2.x),
            Random.Range(Range1.y, Range2.y),
            Random.Range(Range1.z, Range2.z)
            );

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotationSpeed * Time.deltaTime);
    }
}
