using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float WaitTime;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(WaitTime);
        Destroy(gameObject);
    }
}
