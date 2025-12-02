using UnityEngine;
using TMPro;

// Attach this to the "Damage" GameObject. When another collider enters its trigger,
// the HP value is decreased by 1.
public class Damage : MonoBehaviour
{
    // HP starts at 10
    public int HP = 10;

    // Reference to the on-screen TMP text that shows the HP (assign in Inspector)
    public TMP_Text HPText;

    void Start()
    {
        // If not assigned in Inspector, try to find a suitable TMP text object in the scene.
        if (HPText == null)
        {
            HPText = GameObject.Find("HPText")?.GetComponent<TMP_Text>();
        }

        if (HPText == null)
        {
            foreach (var t in FindObjectsOfType<TMP_Text>())
            {
                if (!string.IsNullOrEmpty(t.text) && t.text.StartsWith("HP"))
                {
                    HPText = t;
                    break;
                }
            }
        }

        UpdateHPText();
    }

    // Update the TMP text if available
    void UpdateHPText()
    {
        if (HPText != null)
            HPText.text = "HP:" + HP;
    }

    // Use trigger-based collisions (common for gameplay events).
    void OnTriggerEnter2D(Collider2D other)
    {
        // Decrease HP by 1 on any incoming collision
        HP = HP - 1;
        Debug.Log(gameObject.name + " HP decreased to " + HP);
        UpdateHPText();

        // Remove the incoming object after processing
        if (other != null && other.gameObject != null)
        {
            Destroy(other.gameObject);
        }
    }

    // Also handle non-trigger collisions if the object uses normal colliders
    void OnCollisionEnter2D(Collision2D collision)
    {
        HP = HP - 1;
        Debug.Log(gameObject.name + " HP decreased to " + HP);
        UpdateHPText();

        // Remove the incoming object after processing
        if (collision != null && collision.gameObject != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
