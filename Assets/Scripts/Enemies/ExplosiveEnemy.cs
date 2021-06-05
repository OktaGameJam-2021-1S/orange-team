using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class ExplosiveEnemy : BaseAlive
{
    public GameObject ObjectToSpawn;

    private void Hit()
    {
        if (ObjectToSpawn)
        {
            var obj = Instantiate(ObjectToSpawn);
            obj.transform.position = transform.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var alive = other.gameObject.GetComponent<IAlive>();
        if(alive != null)
        {
            alive.TakeDamage(this, this.Data.AttackDamage);
            Destroy(gameObject);
            Hit();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        var alive = collision.collider.gameObject.GetComponent<IAlive>();
        if (alive != null)
        {
            alive.TakeDamage(this, this.Data.AttackDamage);
        }
    }
}