using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RoomTeleporter : Singleton<RoomTeleporter>
{
    private DungeonCreator DungeonData;
    public Animator Fade;

    private void Start()
    {
        DungeonData = GameObject.FindObjectOfType<DungeonCreator>();
        Fade?.gameObject.SetActive(false);
    }
    public void Teleport(IEntity player, int roomIndex)
    {
        StartCoroutine(COTeleport(player, roomIndex));
    }

    IEnumerator COTeleport(IEntity player, int roomIndex)
    {
        Debug.Log("Teleporting to: " + roomIndex + " / " + DungeonData.AvailableRooms.Count);
        Fade?.gameObject.SetActive(true);
        if (Fade != null)
            Fade.SetTrigger("In");
        //player.Transform.gameObject.SetActive(false);
        player.Transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(.2f);
        SimpleObjectPooling.Instance.DisableAll();
        try
        {
            var nextDungeon = DungeonData.AvailableRooms[roomIndex];
            var currentDungeon = DungeonData.AvailableRooms[nextDungeon.PreviousRoomIndex];
            nextDungeon.gameObject.SetActive(true);
            currentDungeon.gameObject.SetActive(false);

            int nearest = 0;
            float nearestDistance = Vector3.Distance(player.Transform.position, nextDungeon.Doors[0].transform.position);
            for (int i = 1; i < nextDungeon.Doors.Length; i++)
            {
                float distance = Vector3.Distance(player.Transform.position, nextDungeon.Doors[i].transform.position);
                if(distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = i;
                }
            }
            var pos = nextDungeon.Doors[nearest].transform.position;
            pos = pos + Vector3.Normalize(pos - player.Transform.position) * 1f;
            player.Transform.position = pos;
        }catch(Exception e)
        {
            Debug.LogError(e.Message);
        }


        if (Fade != null)
            Fade.SetTrigger("Out");
        yield return new WaitForSeconds(.2f);
        Fade?.gameObject.SetActive(false);
        //player.Transform.gameObject.SetActive(true);
    }
}