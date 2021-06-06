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
    private bool IsInside = false;

    public void SetRoomToGo(int roomToGo)
    {
        RoomToGo = roomToGo;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == k.TagPlayer)
            IsInside = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == k.TagPlayer)
            IsInside = false;
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == k.TagPlayer)
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            RoomTeleporter.Instance.Teleport(other.GetComponent<IEntity>(), RoomToGo);
    //        }
    //    }
    //}
    private void Update()
    {
        if (IsInside && Input.GetKeyDown(KeyCode.E))
        {
            
            var players = GameObject.FindGameObjectWithTag(k.TagPlayer);
            RoomTeleporter.Instance.Teleport(players.GetComponent<IEntity>(), RoomToGo);
            IsInside = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            return;
        
        Gizmos.color = Color.green;
        var dungeon = GameObject.FindObjectOfType<DungeonCreator>();
        if (dungeon == null ||dungeon.AvailableRooms.Count <= 0)
            return;
        Gizmos.DrawLine(
                     transform.position,
                    dungeon.AvailableRooms[RoomToGo].Entrance.transform.position
                    );
    }
}