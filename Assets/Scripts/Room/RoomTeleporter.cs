using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RoomTeleporter : Singleton<RoomTeleporter>
{
    public DungeonCreator DungeonData;
    


    public void Teleport(IEntity player, int roomIndex)
    {
        StartCoroutine(COTeleport(player, roomIndex));
    }

    IEnumerator COTeleport(IEntity player, int roomIndex)
    {
        player.Transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(.2f);


        var nextDungeon = DungeonData.AvailableRooms[roomIndex];
        var currentDungeon = DungeonData.AvailableRooms[nextDungeon.PreviousRoomIndex];
        nextDungeon.gameObject.SetActive(true);
        currentDungeon.gameObject.SetActive(false);

        player.Transform.position = nextDungeon.Entrance.transform.position;

        yield return new WaitForSeconds(.2f);
        player.Transform.gameObject.SetActive(true);
    }
}