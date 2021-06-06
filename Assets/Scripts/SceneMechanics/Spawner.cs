using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private float InitialDelay = 1f;
    [SerializeField] private float DelayBetweenObjects = 1f;
    [SerializeField] private int Amount;
    [SerializeField] private bool Repeats = true;
    [SerializeField] private GameObject[] SpawnPrefabs;
    [SerializeField] private Transform[] SpawnPosition;
    [SerializeField] private Transform SpawnParent;
    [SerializeField] private Transform KillPoint;

    [Header("Preparation")]
    [SerializeField] GameObject Announcement;
    [SerializeField] float AnnounceTime;


    private float TimeToSpawn;
    private int SpawnCount = 5;

    private void Reset()
    {
        SpawnParent = transform;
        SpawnPosition = new Transform[] { transform };
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
        if (SpawnCount >= Amount && !Repeats)
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
        var newGO = SimpleObjectPooling.Instance.Instantiate(prefab, position.position);
        newGO.transform.rotation = position.rotation;
        newGO.SetActive(true);
        SpawnCount++;
    }
    private void CalculateNext()
    {
        TimeToSpawn = DelayBetweenObjects;
        if (SpawnCount >= Amount && Repeats)
        {
            TimeToSpawn = InitialDelay;
            SpawnCount = 0;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (var item in SpawnPosition)
        {
            if (KillPoint != null)
                Gizmos.DrawLine(KillPoint.position, item.transform.position);
            Gizmos.DrawCube(item.transform.position, Vector3.one);
        }

        
    }
}
