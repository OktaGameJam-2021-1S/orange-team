using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Movable
{
    IEntity Owner;


    public void Setup(IEntity owner)
    {
        Owner = owner;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if(other.tag == k.TagPlayer)
        //{
        //
        //}
        var entity = other.GetComponent<IEntity>();
        if(entity != null)
        {
            entity.TakeDamage(Owner, Owner.Data.AttackDamage);
        }
    }
}
