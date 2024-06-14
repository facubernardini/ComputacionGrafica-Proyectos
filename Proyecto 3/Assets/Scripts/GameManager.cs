using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GUIManager guiManager;

    public int timeBetweenAnomalies;
    public int maxAnomalies;
    
    public Room[] rooms;
    private int currentRoom;
    private int activeAnomalies = 0;

    private int anomaliesFound = 0;

    void Start()
    {
        ActivateCamera(currentRoom);
        InvokeRepeating("CreateAnomaly", timeBetweenAnomalies, timeBetweenAnomalies);
    }

    public void ActivateCamera(int room)
    {
        foreach(Room r in rooms)
        {
            r.GetCamera().SetActive(false);
        }
        rooms[room].GetCamera().SetActive(true);
        guiManager.UpdateRoomLabel(rooms[room].roomName);
    }

    public void CreateAnomaly()
    {
        int randomRoom = Random.Range(0, rooms.Length);
        if (randomRoom != currentRoom)
        {
            if (rooms[randomRoom].ActivateAnomaly()){
                activeAnomalies++;
            }
            else
            {
                CreateAnomaly();
            }
        }
        else
        {
            CreateAnomaly();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeCameraForwards();
        }else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeCameraBackwards();
        }

        CheckGameOver();
    }

    void CheckGameOver()
    {
        activeAnomalies = 0;
        foreach(Room r in rooms)
        {
            activeAnomalies += r.GetActiveAnomaliesNumber();
        }
        if (activeAnomalies > maxAnomalies)
        {
            GameOver();
        }
        guiManager.UpdateAnomaliesNumber(activeAnomalies);
    }

    private void GameOver()
    {
        guiManager.ShowGameOver(anomaliesFound);
        CancelInvoke();
    }

    public void ChangeCameraForwards()
    {
        currentRoom = (currentRoom + 1) % rooms.Length;
        ActivateCamera(currentRoom);
    }

    public void ChangeCameraBackwards()
    {
        currentRoom = (currentRoom - 1);
        if (currentRoom == -1) currentRoom = rooms.Length - 1;
        ActivateCamera(currentRoom);
    }

    public void ReportAnomaly(string roomName, string anomalyName)
    {
        bool anomalyPresent = false;
        foreach (Room r in rooms)
        {
            if (r.roomName.Equals(roomName))
            {
                anomalyPresent = r.CheckForAnomaly(anomalyName);
            }
        }
        if (anomalyPresent)
        {
            anomaliesFound++;
            guiManager.AnomalyRemoved();
        }
        else
        {
            guiManager.AnomalyNotFound();
        }
    }

    public void YouWon()
    {
        CancelInvoke();
    }

    public int GetAnomaliesFound()
    {
        return anomaliesFound;
    }
}
