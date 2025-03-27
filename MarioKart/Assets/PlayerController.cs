using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KartController : MonoBehaviour
{
    public Image BoostItemUI; // Image de l'item dans le Canvas
    public float speed = 10f;
    public float turnSpeed = 100f;
    public float friction = 0.98f;
    public float boostMultiplier = 2f;
    public float boostDuration = 2f;
    public float zoneboostMultiplier = 1.5f;
    public float zoneboostDuration = 3f;
    public float originalSpeed = 10f;

    private Rigidbody rb;
    private float currentSpeed;
    private bool isBoosting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = originalSpeed;

        // Masquer l'UI du boost au début
        BoostItemUI.gameObject.SetActive(false);
    }

    void Update()
    {
        rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);

        if (!isBoosting)
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
    public void PickUpBoostItem()
    {
        BoostItemUI.gameObject.SetActive(true); // Affiche l'UI du boost
    }

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
}