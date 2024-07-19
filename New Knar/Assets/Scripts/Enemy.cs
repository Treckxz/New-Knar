using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;
    private float initialY;
    private float initialZ;
    private Transform player;
    public float detectionRadius = 5.0f; // Radio de detección
    public float moveSpeed = 1.0f; // Velocidad de movimiento

    void Start()
    {
        ani = GetComponent<Animator>();
        initialY = transform.position.y;
        initialZ = transform.position.z;

        // Encuentra al jugador por su etiqueta
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Enemy_Sombria();
    }

    public void Enemy_Sombria()
    {
        cronometro += 1 * Time.deltaTime;
        if (cronometro >= 4)
        {
            rutina = Random.Range(0, 3);
            cronometro = 0;
        }

        if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            // Si el jugador está dentro del radio de detección, sigue al jugador
            FollowPlayer();
        }
        else
        {
            // Comportamiento normal del enemigo
            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                    ani.SetBool("walk", true);
                    break;
            }
        }

        // Fijar las posiciones en los ejes Y y Z
        Vector3 position = transform.position;
        position.y = initialY;
        position.z = initialZ;
        transform.position = position;
    }

    void FollowPlayer()
    {
        // Dirección solo en el eje X
        Vector3 direction = (player.position - transform.position);
        direction.y = 0; // No moverse en el eje Y
        direction.z = 0; // No moverse en el eje Z

        // Mirar hacia el jugador
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Mover hacia el jugador solo en el eje X
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        // Establecer la animación de caminar
        ani.SetBool("walk", true);
    }
}
