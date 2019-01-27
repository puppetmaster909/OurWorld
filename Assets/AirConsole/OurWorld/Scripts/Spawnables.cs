using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnables : MonoBehaviour
{
    public GameObject square;
    public GameObject cylinder;
    public GameObject origin;

    //public Transform[] spawnPoints;
    public List<Transform> spawnPoints = new List<Transform>();

    private bool validSpawn = false;
    private Transform spawnPT;
    private List<string> usedSpawnPTS = new List<string>();

    List<GameObject> spawnableObjects = new List<GameObject>();

    public void ButtonInput(string input)
    {
        switch (input)
        {
            case "cylinder":
                spawnObject(input);
                break;
            case "square":
                spawnObject(input);
                break;
            case "button-up":
                break;
            default:
                Debug.Log("ERROR AT BUTTON INPUT");
                break;
        }
    }

    public void spawnObject(string objectName)
    {
        GameObject temp = new GameObject();

        for (int i = 0; i < spawnableObjects.Count; i++)
        {
            if(spawnableObjects[i].name == objectName)
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
             } else
             {
                Instantiate(temp, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                usedSpawnPTS.Add(spawnPoints[spawnPointIndex].name);
                Debug.Log("SpawnPT Name: " + spawnPoints[spawnPointIndex].name);
                validSpawn = true;
             }
            if(usedSpawnPTS.Count == spawnPoints.Count)
            {
                Debug.Log("Planet's full!");
                break;
            }
         }
        validSpawn = false;

        //Debug.Log("Current Spawn PT: " + spawnPointIndex);
        //Instantiate(temp, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation, origin.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnableObjects.Add(square);
        spawnableObjects.Add(cylinder);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
