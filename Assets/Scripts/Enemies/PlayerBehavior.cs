using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : BaseUnit, IPlayer, IEntity
{


    Rigidbody Body;
    float TimeToAttack;

    protected override void Start()
    {
        base.Start();
		Body = GetComponent<Rigidbody>();
	}

    private void Update()
    {
		var pos = transform.position;
		var newPos = pos;
		if (Input.GetKey(KeyCode.D))
		{
			newPos = newPos + Vector3.right * Data.Speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A))
		{
			newPos = newPos + Vector3.left * Data.Speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W))
		{
			newPos = newPos + Vector3.forward * Data.Speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S))
		{
			newPos = newPos + -Vector3.forward * Data.Speed * Time.deltaTime;
		}
		if(newPos != pos)
        {
			Body.MovePosition(newPos);
		}
    }
}
