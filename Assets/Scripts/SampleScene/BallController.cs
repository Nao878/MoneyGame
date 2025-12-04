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
        if (collision.gameObject.name == "Twice")
        {
            // If the ball prefab has a Rigidbody2D, instantiate it in world space so physics can push enemies.
            if (ball != null && ball.GetComponent<Rigidbody2D>() != null)
            {
                Vector3 spawnPos = thisTransform != null ? thisTransform.position : transform.position;
                GameObject go = Instantiate(ball, spawnPos, thisTransform != null ? thisTransform.rotation : transform.rotation);

                audioSource.PlayOneShot(sound1);

                PlayerController playerController;
                GameObject playerObject = GameObject.Find("Player");
                if (playerObject != null)
                {
                    playerController = playerObject.GetComponent<PlayerController>();
                    if (playerController != null)
                        playerController.ballCount++;
                }
            }
            else
            {
                // Fallback for UI-style ball: place under Canvas using previous conversion logic
                GameObject canvasGO = GameObject.Find("Canvas");
                if (canvasGO == null)
                {
                    Debug.LogWarning("Canvas not found");
                    return;
                }

                Canvas canvas = canvasGO.GetComponent<Canvas>();
                RectTransform canvasRect = canvasGO.GetComponent<RectTransform>();

                Camera cam = null;
                if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
                {
                    cam = Camera.main;
                }

                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cam, thisTransform.position);
                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, cam, out localPoint);

                GameObject go = Instantiate(ball);
                go.transform.SetParent(canvasGO.transform, false);

                RectTransform goRect = go.GetComponent<RectTransform>();
                if (goRect != null)
                {
                    goRect.anchoredPosition = localPoint;
                }
                else
                {
                    go.transform.localPosition = new Vector3(localPoint.x, localPoint.y, 0f);
                }

                audioSource.PlayOneShot(sound1);

                PlayerController playerController;
                GameObject playerObject = GameObject.Find("Player");
                if (playerObject != null)
                {
                    playerController = playerObject.GetComponent<PlayerController>();
                    if (playerController != null)
                        playerController.ballCount++;
                }
            }
        }
    }
}
