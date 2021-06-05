using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    [Serializable]
    public struct RoomTemplate
    {
        public DoorDirection Direction;
        public BaseRoom[] Rooms;
    }
    public List<RoomTemplate> RoomsTemplate;
    public BaseRoom StartRoom;
    public List<BaseRoom> AvailableRooms = new List<BaseRoom>();
    public int MaxRooms = 10;
    public int RoomSize = 10;
    public int RandomSeed;


    private System.Random RoomRandom;
    private Queue<BaseRoom> RoomsToProcess = new Queue<BaseRoom>();

    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (RandomSeed <= 0)
            RandomSeed = DateTime.Now.Millisecond;
        RoomRandom = new System.Random(RandomSeed);
        RoomsToProcess.Enqueue(StartRoom);
        yield return StartCoroutine(CreateRoom());
    }
    IEnumerator CreateRoom()
    {
        while (RoomsToProcess.Count >= 0 && AvailableRooms.Count < MaxRooms)
        {
            var room = RoomsToProcess.Dequeue();
            for (int i = 0; i < room.Doors.Length; i++)
            {
                var roomToGo = room.Doors[i].Direction;
                room.Doors[i].SetRoomToGo(i);
                var templates = RoomsTemplate.Where(p => p.Direction == roomToGo).FirstOrDefault();
                int roomIdx = RoomRandom.Next(0, templates.Rooms.Length);
                var roomToCreate = templates.Rooms[roomIdx];
                var newRoom = Instantiate(roomToCreate);
                newRoom.transform.position = room.transform.position + GetOffset(roomToGo);
                newRoom.RoomIndex = AvailableRooms.Count;
                newRoom.PreviousRoomIndex = room.RoomIndex;
                AvailableRooms.Add(newRoom);
                if (newRoom.Doors.Length > 0)
                    RoomsToProcess.Enqueue(newRoom);
                yield return null;
            }
        }
        yield return null;
    }
    Vector3 GetOffset(DoorDirection direction)
    {
        switch (direction)
        {
            case DoorDirection.Back:
                return new Vector3(0, 0, -RoomSize);
            case DoorDirection.Front:
                return new Vector3(0, 0, RoomSize);
            case DoorDirection.Up:
                return new Vector3(0, RoomSize, 0);
            case DoorDirection.Down:
                return new Vector3(0, -RoomSize, 0);
            case DoorDirection.Right:
                return new Vector3(RoomSize, 0, 0);
            case DoorDirection.Left:
                return new Vector3(-RoomSize, 0, 0);
        }
        Debug.LogError("impossible situation");
        return new Vector3(-RoomSize * 10, 0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 1; i < AvailableRooms.Count; i++)
        {
            Gizmos.DrawLine(
                AvailableRooms[AvailableRooms[i].RoomIndex].Entrance.transform.position, 
                AvailableRooms[AvailableRooms[i].PreviousRoomIndex].Entrance.transform.position
                );
        }
    }

}
