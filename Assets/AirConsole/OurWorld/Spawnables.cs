using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnables : MonoBehaviour
{
    public GameObject square;
    public GameObject cylinder;
    public GameObject origin;
    private Transform spawnPT;

    List<GameObject> spawnableObjects = new List<GameObject>();

    public void ButtonInput(string input)
    {
        switch (input)
        {
            case "tringle":
                spawnObject("cylinder");
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
            //Debug.Log("ListName: " + spawnableObjects[i].name + " vs " + objectName);
            if(spawnableObjects[i].name == objectName)
            {
                //Debug.Log("SPAWN OBJECT NAME: " + spawnableObjects[i].name);
                temp = spawnableObjects[i];
                //--Debug.Log("TEMP NAME: " + temp.name);
            }
        } 
        Instantiate(temp, origin.transform.position, origin.transform.rotation);
        //Debug.Log("Object name: " + objectName + " NAME OF OBJECT: " + temp.name);
        Debug.Log(objectName + " Spawned!");
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnableObjects.Add(square);
        spawnableObjects.Add(cylinder);
        spawnPT = origin.transform;
        //spawnPT = new Vector3(origin.transform.position.x, origin.transform.position.y + 5, origin.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
