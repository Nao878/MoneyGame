using UnityEngine;

// Attach this to the ball prefab. It will apply an initial downward impulse
// and a small continuous force so the ball moves down and can push enemies.
public class BallMovement : MonoBehaviour
{
    [Tooltip("Initial impulse applied once (Impulse)")]
    public float initialImpulse = 8f;

    [Tooltip("Continuous downward force applied each FixedUpdate")]
    public float continuousForce = 20f;

    [Tooltip("Maximum downward speed (absolute value)")]
    public float maxDownSpeed = 12f;

    [Tooltip("Time in seconds before the ball is auto-destroyed")]
    public float lifeTime = 8f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        // Ensure dynamic body so it interacts with enemies
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f; // use our forces, not Unity gravity
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sharedMaterial = rb.sharedMaterial; // no-op to avoid warnings
    }

    void Start()
    {
        if (rb != null)
        {
            rb.AddForce(Vector2.down * initialImpulse, ForceMode2D.Impulse);
        }

        if (lifeTime > 0f)
            Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        rb.AddForce(Vector2.down * continuousForce * Time.fixedDeltaTime, ForceMode2D.Force);

        // Clamp downward speed
        if (rb.linearVelocity.y < -maxDownSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxDownSpeed);
        }
    }
}
