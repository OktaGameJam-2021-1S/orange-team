using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRoom : MonoBehaviour
{
    public int RoomIndex;
    public int PreviousRoomIndex;
    public GameObject Entrance;
    public Door[] Doors;
    public GameObject SceneContent;

    private void Awake()
    {
        Doors = GetComponentsInChildren<Door>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(Entrance.transform.position, .5f);
        for (int i = 0; i < Doors.Length; i++)
        {
            Gizmos.DrawSphere(Doors[i].transform.position, .5f);
        }
        
    }
}
