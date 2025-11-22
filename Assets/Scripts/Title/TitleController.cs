using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public GameObject active2;
    private int i = 0;

    public void Click()
    {
        active2.SetActive(true);
        i++;

        if (i >= 2)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
