using UnityEngine;

// Attach this to the "Damage" GameObject. When another collider enters its trigger,
// the HP value is decreased by 1.
public class Damage : MonoBehaviour
{
    // HP starts at 10
    public int HP = 10;

    // Use trigger-based collisions (common for gameplay events).
    void OnTriggerEnter2D(Collider2D other)
    {
        // Decrease HP by 1 on any incoming collision
        HP = HP - 1;
        Debug.Log(gameObject.name + " HP decreased to " + HP);
    }

    // Also handle non-trigger collisions if the object uses normal colliders
    void OnCollisionEnter2D(Collision2D collision)
    {
        HP = HP - 1;
        Debug.Log(gameObject.name + " HP decreased to " + HP);
    }
}
