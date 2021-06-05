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
            var newGO = SimpleObjectPooling.Instance.Instantiate(ObjectToSpawn, transform.position);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Hit();
        SimpleObjectPooling.Instance.Destroy(other.gameObject);
    }
}
