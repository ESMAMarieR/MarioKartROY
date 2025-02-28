using UnityEngine;

public class BonusBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KartController kart = other.GetComponent<KartController>();
            if (kart != null)
            {
                kart.ApplyBoxBoost();
            }
            Destroy(gameObject);
        }
    }
}
