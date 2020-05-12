using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRamp : MonoBehaviour
{
    public GameObject panda;

    void Start()
    {
        panda = GameObject.Find("Main Camera");
    }

    void Update()
    {
        

        if(Vector3.Distance(this.transform.position, panda.transform.position) > 250)
        {
            Destroy(this.gameObject);
        }

    }
}
