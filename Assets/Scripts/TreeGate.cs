using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGate : MonoBehaviour
{
    public GameObject seedOne;
    public GameObject seedTwo;

    public List<GameObject> trees = new List<GameObject>();

    public Vector3 seedOnePosition;
    public Vector3 seedTwoPosition;

    public int points;

    void Start()
    {
        seedOnePosition = seedOne.transform.position;
        seedTwoPosition = seedTwo.transform.position;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Panda")
        {

            Destroy(seedOne);
            Destroy(seedTwo);

            Instantiate(trees[Random.Range(0, trees.Count)], seedOnePosition, Quaternion.identity, transform);
            Instantiate(trees[Random.Range(0, trees.Count)], seedTwoPosition, Quaternion.identity, transform);

            Score score = GameObject.Find("GameManager").GetComponent<Score>();
            score.points += points;

            Destroy(this);
        }
    }
}
