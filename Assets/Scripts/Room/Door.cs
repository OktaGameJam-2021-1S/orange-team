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
    public int RoomToGo;
    
    public void SetRoomToGo(int roomToGo)
    {
        RoomToGo = roomToGo;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == k.TagPlayer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RoomTeleporter.Instance.Teleport(other.GetComponent<IEntity>(), RoomToGo);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            return;
        Gizmos.color = Color.green;
        var dungeon = GameObject.FindObjectOfType<DungeonCreator>();
        Gizmos.DrawLine(
                     transform.position,
                    dungeon.AvailableRooms[RoomToGo].Entrance.transform.position
                    );
    }
}