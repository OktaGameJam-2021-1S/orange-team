using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnter : MonoBehaviour
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
        Hit();
        Destroy(other.gameObject);
    }
}
