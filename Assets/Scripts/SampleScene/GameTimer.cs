using UnityEngine;
using TMPro;

// Attach this to any GameObject in the scene. It finds a TMP text object on the
// canvas and displays the remaining game time. Initial value defaults to 30 seconds.
public class GameTimer : MonoBehaviour
{
    // Initial remaining time in seconds
    public float RemainingSeconds = 30f;

    // Reference to the on-screen TMP text that shows the time (assign in Inspector)
    public TMP_Text TimeText;

    float _time;

    void Start()
    {
        _time = RemainingSeconds;

        // Try to find common name first
        if (TimeText == null)
        {
            TimeText = GameObject.Find("TimeText")?.GetComponent<TMP_Text>();
        }

        // Fall back to searching any TMP text that already contains a hint
        if (TimeText == null)
        {
            foreach (var t in FindObjectsOfType<TMP_Text>())
            {
                if (string.IsNullOrEmpty(t.text))
                    continue;

                var txt = t.text.Trim();
                if (txt.StartsWith("Time") || txt.StartsWith("Time:") || txt.StartsWith("Timer") || txt.StartsWith("Žc") || txt.StartsWith("Žc‚è") || txt.StartsWith("ŽžŠÔ"))
                {
                    TimeText = t;
                    break;
                }
            }
        }

        UpdateTimeText();
    }

    void Update()
    {
        if (_time <= 0f)
            return;

        _time -= Time.deltaTime;
        if (_time < 0f) _time = 0f;

        UpdateTimeText();
    }

    void UpdateTimeText()
    {
        if (TimeText == null)
            return;

        // Show as integer seconds remaining (you can change format if desired)
        int secs = Mathf.CeilToInt(_time);
        TimeText.text = "Time:" + secs;
    }
}
