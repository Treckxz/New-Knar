using System.Collections;
using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    public Transform player; // Asigna el transform del jugador
    public float detectionRadius = 1.0f; // Radio de detección del jugador
    public float fallDelay = 1.0f; // Tiempo que tarda en caer después de la detección
    private bool isFalling = false;

    private void Update()
    {
        // Verifica si el jugador está debajo de las espinas
        if (player != null && !isFalling)
        {
            float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);
            if (distanceToPlayer <= detectionRadius)
            {
                StartCoroutine(Fall());
            }
        }
    }

    private IEnumerator Fall()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelay);
        // Activa la física para que las espinas caigan
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        // Opcional: Puedes destruir las espinas después de un tiempo
        // Destroy(gameObject, 2.0f);
    }
}

