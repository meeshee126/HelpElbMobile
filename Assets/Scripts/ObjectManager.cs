using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ObjectManager : MonoBehaviour
{
    Rigidbody rb;

    public int points;

    public BoxCollider boxCollider;
    
    public float speed;
    bool hitted;

    bool pointsGetted;

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
        if(collision.collider.tag == "Ramp")
        {
            boxCollider.enabled = false;
            hitted = true;
            rb.velocity = new Vector3();
            rb.isKinematic = true;
            Debug.Log("hit");

            BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();

            if(colliders.Length != 0)
            {
                foreach (BoxCollider collider in colliders)
                {
                    collider.enabled = true;
                }
            }

           
        }
       
    }
 

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Panda")
        {
            BoxCollider[] colliders = GetComponents<BoxCollider>();

            if (colliders.Length != 0)
            {
                foreach (BoxCollider collider in colliders)
                {
                   
                    
                    collider.enabled = false;
                }
                Score score = GameObject.Find("GameManager").GetComponent<Score>();
                score.points += points;
            }

              

            Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();

            if(rbs.Length != 0)
            {
                foreach (Rigidbody rb in rbs)
                {
                    rb.isKinematic = false;
                }
            }

            
        }
    }
}
