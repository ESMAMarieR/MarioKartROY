using UnityEngine;

public class BonusBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si c'est le Kart
        {
            KartController kart = other.GetComponent<KartController>();
            if (kart != null)
            {
                kart.ApplyBoost();  // Applique le boost
            }
            Destroy(gameObject); // Supprime la caisse
        }
    }
}
