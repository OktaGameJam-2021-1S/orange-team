using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// moves the transform with sim based addition
/// </summary>
public class SinMovable : MonoBehaviour
{
    public float TimePeriod = 2;
    public float Height = 1f;
    private float TimeSinceStart;
    Rigidbody Body;


    private void Start()
    {
        Height /= 2;
        TimeSinceStart = (3 * TimePeriod) / 4;
        Body = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Vector3 currentPosition = Body.position;
        var sin = Mathf.Sin(((Mathf.PI * 2) / TimePeriod) * TimeSinceStart) / 100;
        Body.position = new Vector3(currentPosition.x, currentPosition.y + sin, currentPosition.z);
        TimeSinceStart += Time.deltaTime;
    }
}
