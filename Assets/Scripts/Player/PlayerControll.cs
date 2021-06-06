using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : BaseUnit, IPlayer
{
    private Rigidbody _rb;
    [Header("Atributes")]
    [SerializeField]
    private float _Damage;
    [SerializeField]
    private float _JumpForce;
    [SerializeField]
    private float _Acceleration;
    [SerializeField]
    private float _MaxVelocity;
    [SerializeField]
    private float _BreakForce;
    [SerializeField]
    private float _RotateVelocity;

    //Variables to controll Moviment
    private Vector3 _Rotation;
    private float _Velocity;
    private float _CurrentAcceleration;
    private float _Height;

    //Variables to controll Jump
    public LayerMask groundMask;
    public bool IsGrounded
    {
        get
        {
            return Physics.Raycast(transform.position, Vector3.down, _Height, groundMask);
        }
    }

    protected override void Start()
    {
        base.Start();
        _rb = GetComponent <Rigidbody>();
        _Height = GetComponent<CapsuleCollider>().height;
        _Rotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //if (IsGrounded)
        {
            RotatePlayer();
            MovePlayer();
            if (IsGrounded)
                Jump();
        }
    }

    private void RotatePlayer()
    {
        _Rotation += new Vector3(0, Input.GetAxis("Horizontal"), 0);
        _rb.MoveRotation(Quaternion.Euler(_Rotation * Time.fixedDeltaTime * _RotateVelocity));
    }

    private void MovePlayer()
    {
        
        if (Input.GetAxis("Vertical") != 0)
        {
            _CurrentAcceleration = _Acceleration * Input.GetAxis("Vertical") * Time.deltaTime;
            _Velocity += _CurrentAcceleration;
            _Velocity = Mathf.Clamp(_Velocity, -_MaxVelocity, _MaxVelocity);
        }
        else
        {
            if (_Velocity != 0)
            {
                _Velocity *= _BreakForce;
            }
        }

        _rb.velocity = (transform.forward * _Velocity) + (transform.up * _rb.velocity.y);

    }

    private void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _JumpForce, _rb.velocity.z);
        }
    }
}
