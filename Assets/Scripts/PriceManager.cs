using UnityEngine;

public class PriceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] priceObjects = new GameObject[5];

    void Start()
    {
        // Ensure all prices are hidden at start
        HideAllPrices();
    }

    public void RevealPrice(int priceIndex)
    {
        if (priceIndex >= 0 && priceIndex < priceObjects.Length && priceObjects[priceIndex] != null)
        {
            priceObjects[priceIndex].SetActive(true);
            Debug.Log($"Price {priceIndex + 1} revealed");
        }
        else
        {
            Debug.LogWarning($"PriceManager: Invalid price index {priceIndex} or price object not assigned");
        }
    }

    public void HideAllPrices()
    {
        for (int i = 0; i < priceObjects.Length; i++)
        {
            if (priceObjects[i] != null)
            {
                priceObjects[i].SetActive(false);
            }
        }
        Debug.Log("All prices hidden");
    }

    public void SetPriceObject(int index, GameObject priceObject)
    {
        if (index >= 0 && index < priceObjects.Length)
        {
            priceObjects[index] = priceObject;
        }
        else
        {
            Debug.LogWarning($"PriceManager: Invalid index {index}");
        }
    }
}
