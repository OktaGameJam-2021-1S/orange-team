using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Movable
{
    IEntity Owner;


    public GameObject impactParticle; // Effect spawned when projectile hits a collider
    public GameObject projectileParticle; // Effect attached to the gameobject as child
    public GameObject muzzleParticle; // Effect instantly spawned when gameobject is spawned


    public void Setup(IEntity owner)
    {
        Owner = owner;

        StopAllCoroutines();
        StartCoroutine(DestroyMe());
        if (muzzleParticle)
        {
            var obj = SimpleObjectPooling.Instance.Instantiate(muzzleParticle, transform.position);
            obj.transform.position = transform.position;
            SimpleObjectPooling.Instance.Destroy(obj, 1.5f);
        }
    }
    private void Start()
    {
        projectileParticle = SimpleObjectPooling.Instance.Instantiate(projectileParticle, transform.position);
        projectileParticle.transform.parent = transform;
        projectileParticle.transform.rotation = transform.rotation;
    }
    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(5f);
        SimpleObjectPooling.Instance.Destroy(gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //if(other.tag == k.TagPlayer)
    //    //{
    //    //
    //    //}
    //    var entity = other.GetComponent<IEntity>();
    //    if(entity != null)
    //    {
    //        entity.TakeDamage(Owner, Owner.Data.AttackDamage);
    //    }
    //
    //    GameObject impactP = SimpleObjectPooling.Instance.Instantiate(impactParticle, transform.position);
    //    //impactP = Quaternion.FromToRotation(Vector3.up, hit.normal)
    //    SimpleObjectPooling.Instance.Destroy(impactP, 1.5f);
    //    SimpleObjectPooling.Instance.Destroy(gameObject);
    //}
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollisionEnter", collision.gameObject);
        var entity = collision.gameObject.GetComponent<IEntity>();
        if (entity != null)
        {
            //Debug.Log("OnCollisionEnter on Entity!", collision.gameObject);
            entity.TakeDamage(Owner, Owner.Data.AttackDamage);
        }

        GameObject impactP = SimpleObjectPooling.Instance.Instantiate(impactParticle, transform.position);
        //impactP = Quaternion.FromToRotation(Vector3.up, hit.normal)
        SimpleObjectPooling.Instance.Destroy(impactP, 1.5f);
        SimpleObjectPooling.Instance.Destroy(gameObject);
    }
}
