using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panda : MonoBehaviour
{
    [Header("Constant Forces")]
    public float forwardForce;
    private float e_forwardForce;
    public float horizontalForce, downwardForce, upwardForce;
    private float e_horizontalForce, e_downwardForce, e_upwardForce;

    [Header("Shortterm Impulses")]
    public float forwardImpulsePower;
    public float horizontalImpulsePower, downwardImpulsePower, upwardImpulsePower;

    [Header("Timers")]
    public float forwardImpulseTime;
    public float horizontalImpulseTime, downwardImpulseTime, upwardImpulseTime;
    private float e_horizontalImpulseTime, e_forwardImpulseTime, e_downwardImpulseTime, e_upwardImpulseTime;
    public float jumpAirTime, defaultAirTime;
    private float e_AirTime, e_JumpAirTime;
    public float dashRegenCooldown;
    private float e_dashRegenCooldown;

    [Header("Other values")]
    public Rigidbody rb;
    public int max_JumpCount, current_JumpCount, max_DashCount, current_DashCount;
    public float minVelocityBeforeForceMultiply, FallAfterJumpSpeed;
    public LayerMask layerMask;
    public RaycastHit[] hits;
    public float rayViewDistance;
    public bool land;
    public GameObject explodingForce;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetBasicValues();
    }


    void Update()
    {
        DashRegen();
        PrintDetails();
        TimersCountdown();
        CoditionExecutionManager();
    }


    /// <summary>
    /// Handles the right call of each force
    /// </summary>
    void CoditionExecutionManager()
    {
        // float moveX = Input.GetAxisRaw("Horizontal");
        float moveX = Input.acceleration.x;
        hits = Physics.RaycastAll(transform.position, new Vector3(0f, -1f, 0f), rayViewDistance, layerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            e_AirTime = defaultAirTime;
            // Touching the ground rn.
        }

        // Count Down before quick fall
        if (isTimeToFall()) rb.AddForce(new Vector3(0f, -1f, 0f) *
            (downwardForce / FallAfterJumpSpeed) * Time.deltaTime, ForceMode.Impulse);

        // Press Down == Break Movement
        if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") < 0f) return;
        else
        {
            if (current_DashCount > 0 && e_JumpAirTime > 0f)
            {
                // Press Left or Right == (short-term Side Dash, Movetowards Side)
                if (Input.GetButtonDown("Horizontal"))
                {
                    current_DashCount--;
                    e_horizontalImpulseTime = horizontalImpulseTime;
                    GameObject.Find("PlayerDashSFX").GetComponent<AudioSource>().Play();
                }
                // Press Up == short-term Forward Dash)
                if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0f)
                {
                    current_DashCount--;
                    e_forwardImpulseTime = forwardImpulseTime;
                    GameObject.Find("PlayerDashSFX").GetComponent<AudioSource>().Play();
                }
            }
            // Press Jump Button == Leaps Upwards
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && current_JumpCount < max_JumpCount)
            {
                current_JumpCount++;
                e_AirTime = defaultAirTime;
                e_JumpAirTime = jumpAirTime;
                e_upwardImpulseTime = upwardImpulseTime;
                GameObject.Find("PlayerJumpSFX").GetComponent<AudioSource>().Play();
            }

            if (current_JumpCount == 2 && (e_AirTime <= 0f && e_JumpAirTime <= 0f))
            {
                e_downwardImpulseTime = downwardImpulseTime;
            }

            ForwardForceHandler();

            // Impulses
            if (e_horizontalImpulseTime > 0f) ApplyHorizontalImpulse(moveX);
            if (e_forwardImpulseTime > 0f) ApplyForwardImpulse();
            if (e_downwardImpulseTime > 0f) ApplyDownwardImpulse();
            if (e_upwardImpulseTime > 0f) ApplyUpwardImpulse();

            // Forces
            if (e_forwardImpulseTime <= 0f) ApplyForwardForce();
            if (e_horizontalImpulseTime <= 0f) ApplyHorizontalForce(moveX);
            if (e_downwardImpulseTime <= 0f && e_upwardImpulseTime <= 0f) ApplyDownwardForce();
            if (e_upwardImpulseTime <= 0f) ApplyUpwardForce();
        }
    }


    private bool isTimeToFall()
    {
        if (e_AirTime <= 0f && e_JumpAirTime <= 0f)
        {
            return true;
        }
        if (e_AirTime > 0f) e_AirTime -= Time.deltaTime;
        if (e_JumpAirTime > 0f) e_JumpAirTime -= Time.deltaTime;
        return false;
    }


    private void DashRegen()
    {
        if (current_DashCount < max_DashCount)
        {
            if (e_dashRegenCooldown <= 0f)
            {
                e_dashRegenCooldown = dashRegenCooldown;
                current_DashCount++;
            }

            if (e_dashRegenCooldown > 0f)
            {
                e_dashRegenCooldown -= Time.deltaTime;
            }
        }
    }


    private void instantiateExplosion(explosionStrength strength)
    {
        GameObject tempGO = null;
        switch (strength)
        {
            case explosionStrength.weak:
                explodingForce.GetComponent<Exploding>().strength = explosionStrength.weak;
                tempGO = Instantiate(explodingForce, transform.position, Quaternion.identity);
                Debug.Log("Has been instantiated"); break;
            case explosionStrength.normal:
                explodingForce.GetComponent<Exploding>().strength = explosionStrength.normal;
                tempGO = Instantiate(explodingForce); break;
            case explosionStrength.strong:
                explodingForce.GetComponent<Exploding>().strength = explosionStrength.strong;
                tempGO = Instantiate(explodingForce); break;
        }
        //Destroy(mom);
    }


    private void TimersCountdown()
    {
        if (e_horizontalImpulseTime > 0f) e_horizontalImpulseTime -= Time.deltaTime;
        if (e_forwardImpulseTime > 0f) e_forwardImpulseTime -= Time.deltaTime;
        if (e_downwardImpulseTime > 0f) e_downwardImpulseTime -= Time.deltaTime;
        if (e_upwardImpulseTime > 0f) e_upwardImpulseTime -= Time.deltaTime;
    }


    private void OnCollisionEnter(Collision collision)
    {
        // I have a dream
        //if (current_JumpCount == 1) instantiateExplosion(explosionStrength.weak);
        //if (current_JumpCount == 2) instantiateExplosion(explosionStrength.normal);
        //if (current_JumpCount == 3) instantiateExplosion(explosionStrength.strong);

        //if (current_JumpCount == 1 || current_JumpCount == 2 || current_JumpCount == 3)
        //    rb.velocity = new Vector3(0f, 0f, 0f);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            current_JumpCount = 0;
            GameObject.Find("PlayerLandSFX").GetComponent<AudioSource>().Play();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Roof")
        {
            current_JumpCount = 3;
            e_downwardImpulseTime = downwardImpulseTime;
            Debug.Log("SLAM DUNK!");
        }
    }


    private void ForwardForceHandler()
    {
        if (rb.velocity.x > minVelocityBeforeForceMultiply * -1f)
        {
            e_forwardForce *= 1.01f;
            e_upwardForce *= 1.015f;
        }
        if (rb.velocity.x < minVelocityBeforeForceMultiply * -1f)
        {
            e_forwardForce = forwardForce;
            e_upwardForce = upwardForce;
        }
    }


    #region Forces USE THE FORCE LUKE!
    private void ApplyForwardForce()
    {
        if (e_forwardForce == forwardForce)
            rb.AddForce(new Vector3(1f, 0f, 0f) * forwardForce * Time.deltaTime * -1f, ForceMode.Force);
        if (e_forwardForce > forwardForce)
        {
            rb.AddForce(new Vector3(1f, 0f, 0f) * e_forwardForce * Time.deltaTime * -1f, ForceMode.Force);
            rb.AddForce(new Vector3(0f, 1f, 0f) * e_upwardForce * Time.deltaTime * -1f, ForceMode.Force);
        }
    }


    void ApplyHorizontalForce(float moveX)
    {
        rb.AddForce(new Vector3(0f, 0f, moveX) * horizontalForce * Time.deltaTime, ForceMode.Force);
        //rb.AddTorque(new Vector3(moveX, 0, 0) * tourqueForce * Time.deltaTime, ForceMode.Force);
    }


    private void ApplyDownwardForce()
    {
        if (current_JumpCount < 0)
            rb.AddForce(new Vector3(0f, -1f, 0f) * downwardForce * Time.deltaTime, ForceMode.Force);
    }


    private void ApplyUpwardForce()
    {
        rb.AddForce(new Vector3(0f, 1f, 0f) * upwardForce * Time.deltaTime, ForceMode.Force);
    }
    #endregion


    #region Impulses DON'T GIVE TO YOUR IMPULSES LUKE!
    private void ApplyForwardImpulse()
    {
        rb.AddForce(new Vector3(1f, 0f, 0f) * forwardImpulsePower * Time.deltaTime * -1f, ForceMode.Impulse);
    }

    private void ApplyHorizontalImpulse(float moveX)
    {
        rb.AddForce(new Vector3(0f, 0f, moveX) * horizontalImpulsePower * Time.deltaTime, ForceMode.Impulse);
    }

    private void ApplyDownwardImpulse()
    {
        rb.AddForce(new Vector3(0f, -1f, 0f) * downwardImpulsePower * Time.deltaTime, ForceMode.Impulse);
    }

    private void ApplyUpwardImpulse()
    {
        rb.AddForce(new Vector3(0f, 1f, 0f) * upwardImpulsePower * Time.deltaTime, ForceMode.Impulse);
    }
    #endregion
    

    private void SetBasicValues()
    {
        e_forwardForce = forwardForce;
        e_horizontalForce = horizontalForce;
        e_downwardForce = downwardForce;
        e_upwardForce = upwardForce;
        current_DashCount = max_DashCount;
    }


    /// <summary>
    /// Prints Details About ball status
    /// </summary>
    private void PrintDetails()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(rb.GetPointVelocity(transform.position));
            Debug.Log(rb.velocity);
        }
    }
}
