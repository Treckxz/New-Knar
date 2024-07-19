using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(player.maxHealth); // Causa la muerte instantánea del jugador
            }
        }
    }
}
