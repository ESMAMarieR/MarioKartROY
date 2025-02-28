using UnityEngine;
using System.Collections;

public class KartController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 100f;
    public float friction = 0.98f;
    public float boostMultiplier = 10.5f;
    public float boostDuration = 10f;
    public float slow = -4f;
    public float stop = 0f;
    public float zoneboostMultiplier = 1f;
    public float zoneboostDuration = 10f;
    //public LayerMask boostLayer;

    private Rigidbody rb;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);

       
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed = speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = slow;
            currentSpeed = -5f;
         //   currentSpeed = -speed;
        //}
        //else if (Input.GetKey(KeyCode.Space))
        //{
        //    currentSpeed = stop;
        //    //   currentSpeed = -speed;
        }
        else
        {
            currentSpeed *= friction;
        }


        
        float turn = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            turn = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turn = 1f;
        }

       
        rb.velocity = transform.forward * currentSpeed;
        transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime);
    }

    public void ApplyBoxBoost()
    {
        StartCoroutine(BoostCoroutine());
    }

    private IEnumerator BoostCoroutine()
    {
        float originalSpeed = speed;
        speed *= boostMultiplier;
        yield return new WaitForSeconds(boostDuration);
        speed = originalSpeed;


        speed *= zoneboostMultiplier;
        yield return new WaitForSeconds(zoneboostDuration);
        speed = originalSpeed;
    }
    public void ApplyZoneBoost()
    {
        StartCoroutine(BoostZoneCoroutine());
    }

    private IEnumerator BoostZoneCoroutine()
    {
        float originalSpeed = speed;
        speed *= zoneboostMultiplier;
        yield return new WaitForSeconds(zoneboostDuration);
        speed = originalSpeed;
    }
}
