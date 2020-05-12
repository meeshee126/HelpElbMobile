using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject panda;
    public GameObject pandaIntro;

    float count;

    void Start()
    {
        
    }

    void Update()
    {
        count += Time.deltaTime;
        if(count >= 2.5f)
        {
            pandaIntro.SetActive(false);
            panda.SetActive(true);
            Destroy(this);
        }
    }
}
