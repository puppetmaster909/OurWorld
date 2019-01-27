using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GameLogic : MonoBehaviour
{
    public Spawnables spawnList;
    public AirConsole myConsole;
    
    

    List<int> playerIds = new List<int>();
    Dictionary<int, bool> playersVoted = new Dictionary<int, bool>();

    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onConnect += OnConnect;
    }

    void OnReady(string code)
    {
        List<int> connectedDevices = AirConsole.instance.GetControllerDeviceIds();
        foreach(int deviceID in connectedDevices)
        {
            AddNewPlayer(deviceID);
        }
    }

    void OnConnect(int device)
    {
        AddNewPlayer(device);
    }

    private void AddNewPlayer(int deviceID)
    {
        if (playerIds.Contains(deviceID))
        {
            return;
        }

        playerIds.Add(deviceID);
        playersVoted.Add(deviceID, false);

    }

    void OnMessage(int fromDeviceID, JToken data)
    {
        // DEBUG FOR CHECKING WHICH PLAYER SENT WHAT DATA
        //Debug.Log("message from " + fromDeviceID + ", data: " + data);

        if (playerIds.Contains(fromDeviceID) && data["action"] != null)
        {
            if (playersVoted[fromDeviceID] == false)
            {
                playersVoted[fromDeviceID] = spawnList.ButtonInput(fromDeviceID, data["action"].ToString());
            }
        }
        
        if (CheckIfAllVoted(playersVoted) == true)
        {
            spawnList.SpawnObject();

            ClearVotes(playersVoted);
            Debug.Log("Players may now vote again!");
        }

    }

    void ClearVotes(Dictionary<int, bool> playersVoted)
    {
        List<int> temp = new List<int>();
        foreach(KeyValuePair<int, bool> player in playersVoted)
        {
            temp.Add(player.Key);
        }
        foreach(int t in temp)
        {
            playersVoted[t] = false;
        }
    }

    bool CheckIfAllVoted(Dictionary<int, bool> playersVoted)
    {
        int votedCount = 0;
        foreach(KeyValuePair<int, bool> entry in playersVoted)
        {
            if (entry.Value == true)
            {
                votedCount++;
            }
        }
        //Debug.Log("Voted Count: " + votedCount);
        if (votedCount == playersVoted.Count)
        {
            return true;
        } else
        {
            return false;
        }
    }

    void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
            AirConsole.instance.onReady -= OnReady;
            AirConsole.instance.onConnect -= OnConnect;
        }
    }
}
