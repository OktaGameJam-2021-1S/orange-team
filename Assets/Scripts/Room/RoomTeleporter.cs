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
    public AudioSource ShowAudio;

    private int CurrentRoomIndex = 0;
    private int PreviousRoomIndex;

    private void Start()
    {
        DungeonData = GameObject.FindObjectOfType<DungeonCreator>();
        Fade?.gameObject.SetActive(false);
    }
    public void Teleport(IEntity player, int roomIndex)
    {
        StartCoroutine(COTeleport(player, roomIndex));
    }

    IEnumerator COTeleport(IEntity player, int nextRoomIndex)
    {
        Debug.Log("Teleporting to: " + nextRoomIndex + " / " + DungeonData.AvailableRooms.Count);
        Fade?.gameObject.SetActive(true);
        if (Fade != null)
            Fade.SetTrigger("In");

        //player.Transform.gameObject.SetActive(false);
        player.Transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 pos = player.Transform.position;
        yield return new WaitForSeconds(.2f);
        SimpleObjectPooling.Instance.DisableAll();
        List<GameObject> sceneContet = new List<GameObject>();
        try
        {

            var nextDungeon = DungeonData.AvailableRooms[nextRoomIndex];
            var currentDungeon = DungeonData.AvailableRooms[CurrentRoomIndex];

            PreviousRoomIndex = CurrentRoomIndex;
            CurrentRoomIndex = nextRoomIndex;

            Debug.Log($"NEXT [{nextDungeon.name}]", nextDungeon);
            Debug.Log($"CURRENT [{currentDungeon.name}]", currentDungeon);
            nextDungeon.gameObject.SetActive(true);
            currentDungeon.gameObject.SetActive(false);

            if (nextDungeon.Doors.Length > 0)
            {
                int nearest = 0;
                float nearestDistance = Vector3.Distance(player.Transform.position, nextDungeon.Doors[0].transform.position);
                for (int i = 1; i < nextDungeon.Doors.Length; i++)
                {
                    float distance = Vector3.Distance(player.Transform.position, nextDungeon.Doors[i].transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearest = i;
                    }
                }
                pos = nextDungeon.Doors[nearest].transform.position;
            }
            else
            {
                pos = nextDungeon.Entrance.transform.position;                     
            }
            //pos = pos + Vector3.Normalize(player.Transform.position - pos) * 5f;
            player.Transform.position = pos;

            Traverse(nextDungeon.SceneContent, sceneContet);
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }

        
        if (Fade != null)
            Fade.SetTrigger("Out");
        if (sceneContet != null)
        {
            for (int i = 0; i < sceneContet.Count; i++)
            {
                sceneContet[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(.2f);

        if(ShowAudio != null)
            ShowAudio.Play();
        if (sceneContet != null)
        {
            for (int i = 0; i < sceneContet.Count; i++)
            {
                sceneContet[i].SetActive(true);
                player.Transform.position = pos;
                if(i % 3 == 0)
                    yield return null;
            }
        }

        Fade?.gameObject.SetActive(false);
        //player.Transform.gameObject.SetActive(true);
    }


    void Traverse(GameObject obj, List<GameObject> list)
    {
        foreach (Transform child in obj.transform)
        {
            if (!child.gameObject.activeSelf)
                continue;
            list.Add(child.gameObject);
            Traverse(child.gameObject, list);
        }
    }
}