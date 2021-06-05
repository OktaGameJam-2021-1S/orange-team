using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RoomTeleporter : MonoBehaviour
{
    public DungeonCreator DungeonData;
    


    public void Teleport(int roomIndex)
    {
        
        
    }

    IEnumerator COTeleport(int roomIndex)
    {
        yield return null;
        
        var dungeon = DungeonData.AvailableRooms[roomIndex];
    }
}