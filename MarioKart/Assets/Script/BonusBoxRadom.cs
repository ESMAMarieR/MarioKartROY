using UnityEngine;

public class BonusBox : MonoBehaviour
{
    public GameObject BoostItem;
    public GameObject PowerSizeDown;
    public GameObject PowerSizeUp;

    private enum ItemBonusType {BoostItem, PowerSizeDown, PowerSizeUp }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayRandomeItem();
            Destroy(gameObject);
            // GiveRandomItem(other.GetComponent<KartController>());
            //Destroy(gameObject);
            //BoostItem.SetActive(true);
            //PowerSizeDown.SetActive(false);//
            //PowerSizeUp.SetActive(false);//
            //gameObject.SetActive(false);
            
        }

    }

    private void DisplayRandomeItem()
    {
        BoostItem.SetActive(false);
        PowerSizeDown.SetActive(false);//
        PowerSizeUp.SetActive(false);//

        int randomeItem = Random.Range(0, 4);

        if (randomeItem == 1)
        {
            BoostItem.gameObject.SetActive(true);
        }

        else if (randomeItem == 2)
        {
            PowerSizeDown.gameObject.SetActive(true);
        }

        else if (randomeItem == 3)
        {
            PowerSizeUp.gameObject.SetActive(true);
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
