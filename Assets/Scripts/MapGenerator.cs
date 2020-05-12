using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    RampList rampList;

    List<GameObject> ramps;
  

    Mesh mesh;

    public Vector3 spawnPostion;

    GameObject ramp;

    void Start()
    {
        rampList = GameObject.Find("GameManager").GetComponent<RampList>();
        ramps = rampList.ramps;
        mesh = GetComponent<MeshFilter>().sharedMesh;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(rampList.count == 8)
        {
            spawnPostion = new Vector3(-93.06f, -4.72f, 0);
            ramp = Instantiate(rampList.endScene, this.transform.position + spawnPostion, Quaternion.Euler(-90f, 0, 0), GameObject.Find("Terrains").transform);
            ramp.AddComponent<MapGenerator>();
            rampList.count++;
            Destroy(this);
        }

        else if (other.name == "Panda" && this.gameObject.name == "TutorialTerrain(Prefab)")
        {
            spawnPostion = new Vector3(-127.74f, -1.5f, 0);
            ramp = Instantiate(ramps[Random.Range(0, ramps.Count)], this.transform.position + spawnPostion, Quaternion.Euler(0f,0, -15f), GameObject.Find("Terrains").transform);
            ramp.AddComponent<MapGenerator>();
            rampList.count++;
            Destroy(this);
        }

        else if (other.name == "Panda" && rampList.count < 8)
        {
            spawnPostion = new Vector3(-127.74f, 0, 0);
            ramp = Instantiate(ramps[Random.Range(0, ramps.Count)], this.transform.position + spawnPostion, Quaternion.Euler(0f, 0, -15f), GameObject.Find("Terrains").transform);
            ramp.AddComponent<MapGenerator>();
            GameObject.Find("GameManager").GetComponent<RampList>().count++;
            Destroy(this);
        }
    }
}
