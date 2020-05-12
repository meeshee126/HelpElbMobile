using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject panda;

    
    public float lerpSpeed, xOffset, yOffset, zOffset;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!panda.activeInHierarchy)
            return;

        FollowPanda();
    }

    public void FollowPanda()
    {
        Vector3 offset = new Vector3(xOffset, yOffset, zOffset);

        Vector3 lerp = Vector3.Lerp(transform.position, panda.transform.position + offset, lerpSpeed * Time.deltaTime);

        transform.position = lerp;

        transform.LookAt(panda.transform.position);
    }
}
