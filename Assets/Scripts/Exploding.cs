using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum explosionStrength { weak, normal, strong }

public class Exploding : MonoBehaviour
{
    [Header("Forces")]
    public float small_ExplosionForce;
    public float normal_ExplosionForce, big_ExplosionForce;
    [Header("Power")]
    public float small_ExplosionPower;
    public float normal_ExplosionPower, big_ExplosionPower;
    [Header("Radiuses")]
    public float small_ExplosionRadius;
    public float normal_ExplosionRadius, big_ExplosionRadius;
    [Header("Other")]
    public explosionStrength strength;

    private void FixedUpdate()
    {
        switch (strength)
        {
            case explosionStrength.weak:
                Small_Explosion(); break;
            case explosionStrength.normal:
                Normal_Explosion(); break;
            case explosionStrength.strong:
                Big_Explosion(); break;
        }
        Destroy(gameObject);
    }


    public void Small_Explosion()
    {
        // add all colliders which got a rigibody in this list
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, small_ExplosionRadius);
        foreach (Collider hit in colliders)
        {
            if (hit.name != "Panda" || hit.tag != "Ramp" || hit.tag != "Water" ||
                hit.tag != "Walls" || hit.tag != "Roof" || hit.gameObject.layer != LayerMask.NameToLayer("Object") ||
                hit.gameObject.layer != LayerMask.NameToLayer("Ground"))
            {
                AddRBs(hit.gameObject);
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                // all objects with a rigibody in the colliders list get an explosion impuls in his direction
                if (rb != null)
                    rb.AddExplosionForce(small_ExplosionPower, this.transform.position, small_ExplosionRadius, small_ExplosionForce, ForceMode.Impulse);
            }
        }
    }


    public void Normal_Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, normal_ExplosionRadius);
        foreach (Collider hit in colliders)
        {
            if (hit.name != "Panda" || hit.tag != "Ramp" || hit.tag != "Water" ||
                hit.tag != "Walls" || hit.tag != "Roof" || hit.gameObject.layer != LayerMask.NameToLayer("Object") ||
                hit.gameObject.layer != LayerMask.NameToLayer("Ground"))
            {
                AddRBs(hit.gameObject);
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(normal_ExplosionPower, this.transform.position, normal_ExplosionRadius, normal_ExplosionForce, ForceMode.Impulse);
            }
        }
    }


    public void Big_Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, big_ExplosionRadius);
        foreach (Collider hit in colliders)
        {
            if (hit.name != "Panda" || hit.tag != "Ramp" || hit.tag != "Water" ||
                hit.tag != "Walls" || hit.tag != "Roof" || hit.gameObject.layer != LayerMask.NameToLayer("Object") ||
                hit.gameObject.layer != LayerMask.NameToLayer("Ground"))
            {
                AddRBs(hit.gameObject);
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(big_ExplosionPower, this.transform.position, big_ExplosionForce, big_ExplosionRadius, ForceMode.Impulse);
            }
        }
    }

    
    public void AddRBs(GameObject sceneobj)
    {
        if (!sceneobj.GetComponent<Rigidbody>())
            sceneobj.AddComponent<Rigidbody>();
    }
}
