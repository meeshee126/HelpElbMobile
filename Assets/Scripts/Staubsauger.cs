using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staubsauger : MonoBehaviour
{
    public GameObject camera;

    private void Update()
    {
        transform.position = camera.transform.position + new Vector3(30, -7f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Object")
        {
            Destroy(other.gameObject);
        }
    }
}
