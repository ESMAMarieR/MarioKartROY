using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class KartController : MonoBehaviour
{
    public Image BoostItemUI; 
    public Image PowerSizeUp; //grandi mais ranlenti
    public Image PowerSizeDown; //reduit mais speed++
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



    private Rigidbody rb;
    private float currentSpeed;
    private bool isBoosting = false;
    private bool isDebuf = false;
    private bool isSizeUp = false;
    private bool isSizeDown = false;

    public float groundCheckDistance = 1.0f; //pour que le kart ne decole pas du sol
    public LayerMask groundLayer;

    public Vector3 normalSize = new Vector3(1, 1, 1);
    public Vector3 smallSize = new Vector3(0.5f, 0.5f, 0.5f);
    public float itemSizeDOWNModification = 1.5f;
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        currentSpeed = originalSpeed;


        BoostItemUI.gameObject.SetActive(false);
        PowerSizeUp.gameObject.SetActive(false);//
        PowerSizeDown.gameObject.SetActive(false);//
    }

    void Update()
    {
        rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);

        rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration); 
        if (Vector3.Dot(transform.up, Vector3.down) > 0.5f)
        {
            StartCoroutine(ResetKart());
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer)) //look si le kart est sur le sol
        {

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

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

        if (PowerSizeDown.gameObject.activeSelf && Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ActivateSizeDown());
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


    private IEnumerator ResetKart() //pour retourner le kart si il est sur lui meme
    {
        yield return new WaitForSeconds(1f);
        transform.position += Vector3.up * 2f;
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private IEnumerator ActivateBoost()
    {
        BoostItemUI.gameObject.SetActive(false);
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

    private IEnumerator ActivateSizeDown()
    {
        PowerSizeDown.gameObject.SetActive(false); // Cache l'item après activation

        // Change la taille et augmente la vitesse
        transform.localScale = smallSize;
        float originalSpeed = currentSpeed;
        currentSpeed *= itemSizeDOWNModification;

        yield return new WaitForSeconds(itemSizeDOWNDuration);

        // Retour à la taille et vitesse normales
        transform.localScale = normalSize;
        currentSpeed = originalSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoostZone"))
        {
            ApplyZoneBoost();
        }
        else if (other.CompareTag("DebufZone"))
        {
            ApplyZoneDebuf();
        }
    }
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