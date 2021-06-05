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
    public Dictionary<string, BaseRoom> RoomGrid = new Dictionary<string, BaseRoom>();
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
        string grid = StartRoom.transform.position.x + "-" + StartRoom.transform.position.y + "-" + StartRoom.transform.position.z;
        RoomGrid.Add(grid, StartRoom);
        AvailableRooms.Add(StartRoom);
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
                var roomToGoPosition = room.transform.position + GetOffset(roomToGo);
                string grid = roomToGoPosition.x + "-" + roomToGoPosition.y + "-" + roomToGoPosition.z;
                Debug.Log($"preparing room grid[{grid}]");
                if (RoomGrid.ContainsKey(grid))
                {
                    var newRoom = RoomGrid[grid];
                    room.Doors[i].SetRoomToGo(newRoom.RoomIndex);
                }
                else
                {
                    var templates = RoomsTemplate.Where(p => p.Direction == roomToGo).FirstOrDefault();
                    Debug.Log($"will create Direction[{templates.Direction}]");
                    int roomIdx = RoomRandom.Next(0, templates.Rooms.Length);
                    var roomToCreate = templates.Rooms[roomIdx];
                    int roomToGoIdx = AvailableRooms.Count;
                    var newRoom = Instantiate(roomToCreate);
                    room.Doors[i].SetRoomToGo(roomToGoIdx);
                    newRoom.transform.position = roomToGoPosition;
                    newRoom.RoomIndex = roomToGoIdx;
                    newRoom.PreviousRoomIndex = room.RoomIndex;
                    AvailableRooms.Add(newRoom);
                    RoomGrid.Add(grid, newRoom);
                    Debug.Log($"Room created grid[{grid}]");
                    if (newRoom.Doors.Length > 0)
                        RoomsToProcess.Enqueue(newRoom);
                }

                //yield return null;
            }
        }
        for (int i = 1; i < AvailableRooms.Count; i++)
        {
            //AvailableRooms[i].gameObject.SetActive(false);
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
        if (!Application.isPlaying)
            return;

        Gizmos.color = Color.green;
        for (int i = 1; i < AvailableRooms.Count; i++)
        {
            //for (int j = 0; j < AvailableRooms[i].Doors.Length; j++)
            //{
            //    var door = AvailableRooms[i].Doors[j];
            //    Gizmos.DrawLine(
            //         door.transform.position,
            //        AvailableRooms[door.RoomToGo].Entrance.transform.position
            //        );
            //    //Gizmos.DrawSphere(door.transform.position, 1.1f);
            //}
            Gizmos.DrawLine(
                AvailableRooms[AvailableRooms[i].RoomIndex].Entrance.transform.position, 
                AvailableRooms[AvailableRooms[i].PreviousRoomIndex].Entrance.transform.position
                );
        }
    }

}
