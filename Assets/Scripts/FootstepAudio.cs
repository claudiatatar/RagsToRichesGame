using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    public float stepInterval = 0.5f;
    public float groundCheckDistance = 1.1f;
    
    private float stepTimer;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb == null || SoundManager.Instance == null) return;

        // Check if grounded using a raycast downward
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        
        // Use horizontal velocity only (ignore vertical/falling)
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        bool isMoving = horizontalVelocity.magnitude > 0.1f;

        if (isMoving && isGrounded)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                SoundManager.Instance.PlaySound2D("Footstep");
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}