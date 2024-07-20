using System.Collections;
using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    public Transform player; // Asigna el transform del jugador
    public float detectionRadius = 1.0f; // Radio de detección del jugador
    public float fallDelay = 1.0f; // Tiempo que tarda en caer después de la detección
    private bool isFalling = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody no encontrado en el objeto FallingSpikes.");
        }
        else
        {
            rb.isKinematic = true; // Asegurarse de que las espinas no se muevan al inicio
        }
    }

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
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si las espinas han tocado el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Detiene el movimiento de las espinas
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }
}
