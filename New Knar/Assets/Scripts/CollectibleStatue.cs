using UnityEngine;

public class CollectibleStatue : MonoBehaviour
{
    public static int statueCount = 0; // Contador estático para todas las estatuillas recolectadas

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            statueCount++;
            Destroy(gameObject); // Destruye la estatuilla al recogerla
        }
    }
}
