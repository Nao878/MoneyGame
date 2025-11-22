using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
    public void Kirihiraku()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void Meteo()
    {
        SceneManager.LoadScene("Scene2");
    }
}
