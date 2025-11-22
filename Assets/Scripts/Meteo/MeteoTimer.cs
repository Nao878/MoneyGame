using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeteoTimer : MonoBehaviour
{
    public float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 16)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
