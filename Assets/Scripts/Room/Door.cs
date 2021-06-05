using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum DoorDirection
{
    Right,
    Left,
    Front,
    Back,
    Up,
    Down,
}
public class Door : MonoBehaviour
{
    public DoorDirection Direction;
    private int RoomToGo;

    public void SetRoomToGo(int roomToGo)
    {
        RoomToGo = roomToGo;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == k.TagPlayer)
        {

        }
    }
}