using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValseController : MonoBehaviour
{
    private float scale = 0;

    void FixedUpdate()
    {
        scale += Mathf.Pow(Time.deltaTime, Time.deltaTime);
        transform.localScale = new Vector3(scale ,scale, 1);
    }
}
