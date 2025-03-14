using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class KartController : MonoBehaviour
{
    public GameObject BoostItem;
    public float speed = 10f; //vitesse normal du kart
    public float turnSpeed = 100f; //vitesse quand le kart tourne
    public float friction = 0.98f; //rallentissement quand on touche plus S
    public float boostMultiplier = 10.5f; //Multiple la vitesse
    public float boostDuration = 10f; //Le temps que le boost dure
    public float slow = -4f; //rallentissement du kart avant de reculer
    public float zoneboostMultiplier = 1f; //Multiplication vitesse de la zonne boost
    public float zoneboostDuration = 10f; //Le temps que le boost dure

    private bool HasBoostItem = false; //item inventaire cacher
    //public LayerMask boostLayer;

    private Rigidbody rb;
    private float currentSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        BoostItem.SetActive(false);
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
            currentSpeed = -speed / 2;
        }
        else
        {
            currentSpeed *= friction;
        
        if (Mathf.Abs(currentSpeed) < 0.1f)
        {
            currentSpeed = 0f;
        }
    }
        if (HasBoostItem && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ActivateBoost());
        }

        // else if (HasBoostItem = false)Input.GetKeyDown(key: KeyCode.W);
        //currentSpeed = speed;

        // Safonctionne mais sa avance tout seul


        float turn = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            turn = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turn = 1f;
        }

        //modif
        //rb.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        rb.velocity = transform.forward * currentSpeed;
        transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime);


    }

    /*public void ApplyBoxBoost() //box qui donnais un boost dés qu'on le traverse
    {
        {
            StartCoroutine(BoostCoroutine());
        }
    }*/
    public void ItemBoostApply()
    {
        { 
            StartCoroutine(ActivateBoost());
        }

    }
    private IEnumerator ActivateBoost()
    {
        float originalSpeed = currentSpeed;
        HasBoostItem = false;
        BoostItem.SetActive(false);
        currentSpeed *= boostMultiplier;

        yield return new WaitForSeconds(boostDuration);
        currentSpeed = originalSpeed;
    }

    public void ApplyZoneBoost()
    {
        StartCoroutine(BoostZoneCoroutine());
    }

    private IEnumerator BoostZoneCoroutine()
    {
        float originalSpeed = currentSpeed;
        currentSpeed *= zoneboostMultiplier;

        yield return new WaitForSeconds(zoneboostDuration);
        currentSpeed = originalSpeed;
    }


    /*private IEnumerator BoostCoroutine()
    {
        float originalSpeed = speed;
        speed *= boostMultiplier;
        yield return new WaitForSeconds(boostDuration);
        speed = originalSpeed;


        speed *= zoneboostMultiplier;
        yield return new WaitForSeconds(zoneboostDuration);
        speed = originalSpeed;
    }*/






}
