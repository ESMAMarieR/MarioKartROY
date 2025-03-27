using UnityEngine;

public class BonusBox : MonoBehaviour
{
    public GameObject BoostItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { BoostItem.SetActive(true);
            gameObject.SetActive(false);

        }
    }


    /*private void OnTriggerEnter(Collider other)
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
    }*/
}
