using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawnables : MonoBehaviour
{
    public GameObject square;
    public GameObject cylinder;
    public GameObject origin;
    public List<Transform> spawnPoints = new List<Transform>();
    public Text votingStatus;

    private Dictionary<int, string> votes = new Dictionary<int, string>();
    private bool validSpawn = false;
    //private Transform spawnPT;
    private List<string> usedSpawnPTS = new List<string>();
    private List<string> firstPair = new List<string>();

    List<GameObject> spawnableObjects = new List<GameObject>();

    public bool ButtonInput(int from, string input)
    {
        switch (input)
        {
            case "cylinder":
                SubmitVote(from, input);
                return true;
            case "square":
                SubmitVote(from, input);
                return true;
            case "button-up":
                return false;
            default:
                Debug.Log("ERROR AT BUTTON INPUT");
                return false;
        }
    }

    public void SubmitVote(int from, string objectName)
    {
        if (votes.ContainsKey(from))
        {
            Debug.Log(from + " Already Voted for: " + objectName);
        }
        else
        {
            votes.Add(from, objectName);
        }

        //Debug.Log("Current Spawn PT: " + spawnPointIndex);
        //Instantiate(temp, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation, origin.transform);
    }

    public void SpawnObject()
    {
        //count up votes
        string objectName = "";
        string option1Name = "";
        string option2Name = "";
        int option1Count = 0;
        int option2Count = 0;

        foreach (KeyValuePair<int, string> entry in votes)
        {
            if (option1Name == "")
            {
                option1Name = entry.Value;
                option1Count++;
                continue;
            } else if (option2Name == "" && entry.Value != option1Name)
            {
                option2Name = entry.Value;
                option2Count++;
                continue;
            } else if (option2Name == "" && entry.Value == option1Name)
            {
                if (firstPair.Contains(option1Name))
                {
                    int index = firstPair.IndexOf(option1Name);
                    option2Name = firstPair[firstPair.Count - index - 1];
                }
            }

            if (entry.Value == option1Name)
            {
                option1Count++;
            } else if (entry.Value == option2Name)
            {
                option2Count++;
            }
        }
        //Ballots counted
        Debug.Log("1: " + option2Count + " 2: " + option2Count);
        if (option1Count > option2Count)
        {
            objectName = option1Name;
        } else if (option1Count < option2Count)
        {
            objectName = option2Name;
        } else
        {
            votingStatus.text = "Tie, please vote again!";
            votes.Clear();
            return;
        }

        //Spawning section
        GameObject temp = new GameObject();

        for (int i = 0; i < spawnableObjects.Count; i++)
        {
            if (spawnableObjects[i].name == objectName)
            {
                temp = spawnableObjects[i];
            }
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Count);

        while (validSpawn == false)
        {
            if (usedSpawnPTS.Contains(spawnPoints[spawnPointIndex].name))
            {
                spawnPointIndex = Random.Range(0, spawnPoints.Count);
                validSpawn = false;
            }
            else
            {
                Instantiate(temp, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                usedSpawnPTS.Add(spawnPoints[spawnPointIndex].name);
                //Debug.Log("SpawnPT Name: " + spawnPoints[spawnPointIndex].name);
                validSpawn = true;
            }
            if (usedSpawnPTS.Count == spawnPoints.Count)
            {
                Debug.Log("Planet's full!");
                break;
            }
        }
        votingStatus.text = "You've chosen: " + objectName;
        validSpawn = false;
        votes.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnableObjects.Add(square);
        spawnableObjects.Add(cylinder);
        votingStatus.text = "Please cast your votes!";

        //Jank below
        firstPair.Add("cylinder");
        firstPair.Add("square");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
