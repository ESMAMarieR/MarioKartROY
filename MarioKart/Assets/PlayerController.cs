using UnityEngine;
using System.Collections;

public class KartController : MonoBehaviour
{
    public float speed = 10f;  // Vitesse de base
    public float turnSpeed = 100f;  // Vitesse de rotation
    public float friction = 0.98f;  // Ralentissement progressif
    public float boostMultiplier = 1.5f;  // Multiplicateur de vitesse
    public float boostDuration = 2f;  // Durée du boost

    private Rigidbody rb;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);

        // Avancer (Z) ou Reculer (S)
        if (Input.GetKey(KeyCode.Z))
        {
            currentSpeed = speed; // Marche avant
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = -speed; // Marche arrière
        }
        else
        {
            currentSpeed *= friction;  // Ralentissement progressif
        }


        // Tourner (Q et D)
        float turn = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            turn = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turn = 1f;
        }

        // Appliquer les mouvements
        rb.velocity = transform.forward * currentSpeed;
        transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime);
    }

    public void ApplyBoost()
    {
        StartCoroutine(BoostCoroutine());
    }

    private IEnumerator BoostCoroutine()
    {
        float originalSpeed = speed;
        speed *= boostMultiplier;
        yield return new WaitForSeconds(boostDuration);
        speed = originalSpeed;
    }
}
