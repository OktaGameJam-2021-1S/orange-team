using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRoom : MonoBehaviour
{
    public int RoomIndex;
    public int PreviousRoomIndex;
    public GameObject Entrance;
    public Door[] Doors;

    private void Awake()
    {
        Doors = GetComponentsInChildren<Door>();
    }
}
