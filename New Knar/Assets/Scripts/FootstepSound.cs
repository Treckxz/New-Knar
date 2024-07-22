using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al Audio Source
    public AudioClip footstepClip;  // Clip de sonido de paso
    public float stepInterval = 0.5f; // Intervalo de tiempo entre pasos

    private float stepTimer;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (footstepClip == null)
        {
            Debug.LogError("Footstep clip not assigned!");
        }
        else
        {
            audioSource.clip = footstepClip;
        }

        // Asegúrate de que el Play On Awake esté desmarcado
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Comprueba si se presionan las teclas "A" o "D"
        if (IsMoving())
        {
            stepTimer += Time.deltaTime;

            if (stepTimer > stepInterval)
            {
                PlayFootstepSound();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f; // Reinicia el temporizador si el jugador no se está moviendo
            StopFootstepSound();
        }
    }

    bool IsMoving()
    {
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
    }

    void PlayFootstepSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void StopFootstepSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
