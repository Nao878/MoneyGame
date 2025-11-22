using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GomiBako : MonoBehaviour
{
    private int count = 10;
    TextMeshProUGUI countText;

    void Start()
    {
        countText = GetComponent<TextMeshProUGUI>();
        countText.text = count.ToString("F0") + "ŒÂ";
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        count--;
        countText.text = count.ToString("F0") + "ŒÂ";
    }

    void Update()
    {
        if (count <= 0)
        {
            SceneManager.LoadScene("ClearScene");
        }
    }
}
