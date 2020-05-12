using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDestroyObject : MonoBehaviour
{
    Rigidbody rb;
    public BoxCollider boxCollider;

    public float speed;
    bool hitted;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!hitted)
        {
            // rb.velocity  = (Vector3.down * speed * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector3();
            boxCollider.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ramp")
        {
            boxCollider.enabled = false;
            hitted = true;
            rb.velocity = new Vector3();
            rb.isKinematic = true;
            Debug.Log("hit");

            BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();

            if (colliders.Length != 0)
            {
                foreach (BoxCollider collider in colliders)
                {
                    collider.enabled = true;
                }
            }


        }

    }
}

