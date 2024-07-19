using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public float playerSpeed;
    public float gravity = 9.8f;
    public float jumpForce;
    public float pushForce = 2.0f;
    public Camera mainCam;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;
    private Vector3 playerInput;
    private float fallVelocity;
    public Transform respawnPoint;

    public int maxHealth = 4;
    private int currentHealth;
    public HealthBar healthBar;

    // Animator component reference
    private Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); // Initialize animator
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

        // Encuentra el punto de reaparición en la escena
        if (respawnPoint == null)
        {
            respawnPoint = FindObjectOfType<RespawnPoint>().transform;
        }
    }

    void Update()
    {
        if (characterController.enabled)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            movePlayer = new Vector3(moveHorizontal, 0, 0) * playerSpeed;
            SetGravity();
            characterController.Move(movePlayer * Time.deltaTime);

            // Flip the player based on movement direction
            if (moveHorizontal > 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (moveHorizontal < 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }

            camDirection();
            movePlayer = playerInput.x * camRight;
            movePlayer = movePlayer * playerSpeed;
            movePlayer.z = 0;
            PlayerJump();
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            // Update animations
            UpdateAnimations();
        }
    }

    void camDirection()
    {
        if (mainCam != null)
        {
            camForward = mainCam.transform.forward;
            camRight = mainCam.transform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward = camForward.normalized;
            camRight = camRight.normalized;
            playerInput = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        }
        else
        {
            Debug.Log("Referencia a la cámara (mainCam) no definida en el script del Player.");
        }
    }

    public void PlayerJump()
    {
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
        }

        fallVelocity -= gravity * Time.deltaTime;
        Vector3 movePlayer = new Vector3(0, fallVelocity, 0);
        characterController.Move(movePlayer * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthBar.SetHealth(currentHealth);

        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Jugador ha muerto");
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        characterController.enabled = false;
        yield return new WaitForSeconds(1.0f);
        transform.position = respawnPoint.position;
        characterController.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        Debug.Log("El jugador ha reaparecido");
    }

    void SetGravity()
    {
        if (characterController.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
        {
            return;
        }

        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);
        body.velocity = pushDir * pushForce;
    }

    // Update the animations based on the player's state
    void UpdateAnimations()
    {
        // Check if the player is moving
        bool isWalking = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f;
        animator.SetBool("isWalking", isWalking);

        // Check if the player is jumping
        bool isJumping = !characterController.isGrounded && fallVelocity > 0;
        animator.SetBool("isJumping", isJumping);
    }
}
