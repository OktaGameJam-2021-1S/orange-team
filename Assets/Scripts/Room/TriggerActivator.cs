using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerActivator : MonoBehaviour
{
    public GameObject[] Activators;

    private void OnTriggerEnter(Collider other)
    {
        foreach (var item in Activators)
        {
            item.SetActive(true);
        }
    }


    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
