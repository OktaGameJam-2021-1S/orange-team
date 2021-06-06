using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPooling : Singleton<SimpleObjectPooling>
{
    private Dictionary<GameObject, List<GameObject>> PoolDB = new Dictionary<GameObject, List<GameObject>>();


    public GameObject Instantiate(GameObject reference, Vector3 position)
    {
        //var t = typeof(T).GetType();
        if (!PoolDB.ContainsKey(reference))
        {
            PoolDB.Add(reference, new List<GameObject>());
        }
        var list = PoolDB[reference];
        for (int i = 0; i < list.Count; i++)
        {
            if(!list[i].activeInHierarchy)
            {
                list[i].SetActive(true);
                list[i].transform.position = position;
                return list[i];
            }
        }

        var newInstance = GameObject.Instantiate(reference, position, Quaternion.identity, transform);
        list.Add(newInstance);
        newInstance.SetActive(true);
        //newInstance.transform.position = position;
        return newInstance;
    }

    public void DisableAll()
    {
        foreach (var item in PoolDB)
        {
            foreach (var item1 in item.Value)
            {
                item1.gameObject.SetActive(false);
            }
        }
    }
    public void Destroy(GameObject go, float destroySchedule = -1)
    {
        if (destroySchedule > 0)
        {
            StartCoroutine(CoDestroy(go, destroySchedule));
        }
        else
        {
            go.SetActive(false);
        }
    }
    public IEnumerator CoDestroy(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(false);
    }
}
