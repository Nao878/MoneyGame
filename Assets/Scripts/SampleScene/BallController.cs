using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class BallController : MonoBehaviour
{
    public GameObject ball;
    public Transform thisTransform;
    public AudioClip sound1;
    AudioSource audioSource;
    public bool ballSE = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (ballSE)
        {
            audioSource.PlayOneShot(sound1);
            ballSE = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Image6")
        {
            Vector3 pos = thisTransform.position;
            pos.x = pos.x * 4.7f;
            pos.y = pos.y * 4.4f;
            pos.z = 0;
            GameObject go = Instantiate(ball, pos, thisTransform.rotation);
            GameObject canvas = GameObject.Find("Canvas");
            go.transform.SetParent(canvas.transform, false);
            audioSource.PlayOneShot(sound1);

            PlayerController playerController;
            GameObject imageBallRed = GameObject.Find("ImageBallRed");
            playerController = imageBallRed.GetComponent<PlayerController>();
            playerController.ballCount++;
        }
    }
}
