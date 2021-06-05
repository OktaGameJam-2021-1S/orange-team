using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private float InitialDelay = 1f;
    [SerializeField] private float DelayBetweenObjects = 1f;
    [SerializeField] private int Amount;
    [SerializeField] private GameObject[] SpawnPrefabs;
    [SerializeField] private Transform[] SpawnPosition;
    [SerializeField] private Transform SpawnParent;

    [Header("Preparation")]
    [SerializeField] GameObject Announcement;
    [SerializeField] float AnnounceTime;


    private float TimeToSpawn;
    private int SpawnCount = 5;

    private void Reset()
    {
        SpawnParent = transform;
    }
    void Start()
    {
        TimeToSpawn = InitialDelay;
        SpawnCount = 0;
        if(Announcement != null)
        {
            Announcement.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnCount >= Amount)
            return;

        TimeToSpawn -= Time.deltaTime;
        if(TimeToSpawn <= 0)
        {
            Spawn();
            CalculateNext();
        }
        if(TimeToSpawn <= AnnounceTime)
        {
            if(Announcement != null)
            {
                Announcement.SetActive(true);
            }
        }
    }

    private void Spawn()
    {
        var prefab = SpawnPrefabs[Random.Range(0, SpawnPrefabs.Length)];
        var position = SpawnPosition[Random.Range(0, SpawnPosition.Length)];
        var newGO = Instantiate(prefab, SpawnParent);
        newGO.transform.position = position.position;
        newGO.transform.rotation = position.rotation;
        newGO.SetActive(true);
        SpawnCount++;
    }
    private void CalculateNext()
    {
        TimeToSpawn = DelayBetweenObjects;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var item in SpawnPosition)
        {
            Gizmos.DrawCube(item.transform.position, Vector3.one);
        }
        
    }
}