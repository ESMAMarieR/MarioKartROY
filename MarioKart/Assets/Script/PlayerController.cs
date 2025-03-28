using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KartController : MonoBehaviour
{
    public Image BoostItemUI; // Image de l'item dans le Canvas
    public Image PowerSizeUp;
    public Image PowerSizeDown;
    public float speed = 10f;
    public float turnSpeed = 100f;
    public float friction = 0.98f;
    public float boostMultiplier = 2f;
    public float boostDuration = 2f;
    public float zoneboostMultiplier = 1.5f;
    public float zoneboostDuration = 3f;
    public float originalSpeed = 10f;
    public float zonedebufMultiplier = 0.5f;
    public float zonedebufDuration = 3f;

    public float sizeKart = 0;

    public float itemSizeUPDuration = 3f;
    public float itemSizeDOWNDuration = 3f;

    public float itemSizeUPModification = 0f;
    public float itemSizeDOWNModification = 0f;


    private Rigidbody rb;
    private float currentSpeed;
    private bool isBoosting = false;
    private bool isDebuf = false;
    private bool isSizeUp = false;
    private bool isSizeDown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = originalSpeed;

        // Masquer l'UI du boost au début
        BoostItemUI.gameObject.SetActive(false);
        PowerSizeUp.gameObject.SetActive(false);//
        PowerSizeDown.gameObject.SetActive(false);//
    }

    void Update()
    {
        rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);

        if (!isBoosting && !isDebuf && !isSizeDown && !isSizeUp)
        {
            if (Input.GetKey(KeyCode.W))
            {
                currentSpeed = speed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                currentSpeed = -speed;
            }
            else
            {
                currentSpeed *= friction;
            }

            if (Mathf.Abs(currentSpeed) < 0.1f)
            {
                currentSpeed = 0f;
            }
        }

        // Activation du boost via Space uniquement si l'item est affiché dans le Canvas et qu'on avance
        if (BoostItemUI.gameObject.activeSelf && Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ActivateBoost());
        }

        if (PowerSizeUp.gameObject.activeSelf && Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Space))//
        {
            StartCoroutine(ActivateSizeUP());//
        }

        if (PowerSizeDown.gameObject.activeSelf && Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Space))//
        {
            StartCoroutine(ActivateSizeDown());//
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

    // Fonction appelée pour récupérer l'item de boost et l'afficher dans le Canvas
    /*public void PickUpBoostItem()
    {
        BoostItemUI.gameObject.SetActive(true); 
    }

    public void PickUpSizeUPItem()//
    {
        PowerSizeUp.gameObject.SetActive(true); //
    }

    public void PickUpSizeDownItem()//
    {
        PowerSizeDown.gameObject.SetActive(true); //
    }*/

    // Boost activé uniquement si l'item est affiché dans le Canvas
    private IEnumerator ActivateBoost()
    {
        BoostItemUI.gameObject.SetActive(false); // Cache l'UI après usage
        isBoosting = true;
        float originalSpeed = currentSpeed;
        currentSpeed *= boostMultiplier;

        yield return new WaitForSeconds(boostDuration);
        currentSpeed = originalSpeed;
        isBoosting = false;
    }

    private IEnumerator ActivateSizeUP()//
    {
        PowerSizeUp.gameObject.SetActive(false);
        //isSizeUp = true;
        float originalSpeed = currentSpeed;
        currentSpeed *= itemSizeUPModification;

        yield return new WaitForSeconds(itemSizeUPDuration);
        currentSpeed = originalSpeed;
        //isSizeUp = false;
    }

    private IEnumerator ActivateSizeDown()//
    {
        PowerSizeDown.gameObject.SetActive(false);
        //isSizeDown = true;
        float originalSpeed = currentSpeed;
        currentSpeed *= itemSizeDOWNModification;

        yield return new WaitForSeconds(itemSizeDOWNDuration);
        currentSpeed = originalSpeed;
        //isSizeDown = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoostZone")) // Zone de boost
        {
            ApplyZoneBoost();
        }
        else if (other.CompareTag("DebufZone")) // Zone de ralentissement
        {
            ApplyZoneDebuf();
        }
    }
    // Détection des zones de boost
    public void ApplyZoneBoost()
    {
        StartCoroutine(BoostZoneCoroutine());
    }


    private IEnumerator BoostZoneCoroutine()
    {
        if (Input.GetKey(KeyCode.W))
        {
            isBoosting = true;
            float originalSpeed = currentSpeed;
            currentSpeed *= zoneboostMultiplier;

            yield return new WaitForSeconds(zoneboostDuration);
            currentSpeed = originalSpeed;
            isBoosting = false;
        }
    }

    /*public void ApplyZoneDebuf()
    {
        isDebuf = true;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DebufZone"))
        {
            isDebuf = true;
            currentSpeed = speed * zonedebufMultiplier;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DebufZone"))
        {
            isDebuf = false;
            currentSpeed = speed;
        }
    }*/

    public void ApplyZoneDebuf()
     {
         StartCoroutine(DebufZoneCoroutine());
     }

     private IEnumerator DebufZoneCoroutine()
     {
         if (Input.GetKey(KeyCode.W))
         {
             isDebuf = true;
             float originalSpeed = currentSpeed;
             currentSpeed -= zonedebufMultiplier;

             yield return new WaitForSeconds(zonedebufDuration);
             currentSpeed = originalSpeed;
             isDebuf = false;
         }
     }
}