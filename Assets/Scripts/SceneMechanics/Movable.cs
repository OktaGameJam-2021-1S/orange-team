using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [SerializeField] private float Speed = 1f;
    [SerializeField] private float Acceleration = 0f;

    Rigidbody Body;
    float CurrentAcceleration;

    private void Start()
    {
        Body = GetComponent<Rigidbody>();
    }
    protected virtual void OnEnable()
    {
        Body = GetComponent<Rigidbody>();
        CurrentAcceleration = 0;
        Body.velocity = Vector3.zero;
    }
    void FixedUpdate()
    {
        CurrentAcceleration += Acceleration;
        Body.position += transform.forward * Time.deltaTime * (Speed + CurrentAcceleration);
    }
}
