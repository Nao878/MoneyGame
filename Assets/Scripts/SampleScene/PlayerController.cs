using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 80.0f;
    private Rigidbody2D rb = null;
    [SerializeField] GameObject ballPrefab,canvas,twicaZone2,twicaZone3,countor;
    [SerializeField] Transform shotPoint;
    public int ballCount,finishCount = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.linearVelocity = new Vector2(-speed, 0)*Time.deltaTime*100;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.linearVelocity = new Vector2(speed, 0)*Time.deltaTime*100;
        }

        if (Input.GetKeyDown(KeyCode.Space) && finishCount < 10)
        {
            // Convert the world position of the shotPoint to the canvas local position
            // so we can place the UI ball without using a magic multiplier.
            Vector3 worldPos = shotPoint.position;
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();

            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, Camera.main, out localPoint);

            GameObject go = Instantiate(ballPrefab, canvas.transform);
            RectTransform goRect = go.GetComponent<RectTransform>();
            if (goRect != null)
                goRect.anchoredPosition = localPoint;
            else
                go.transform.localPosition = new Vector3(localPoint.x, localPoint.y, 0);

            BallController ballController = go.GetComponent<BallController>();
            if (ballController != null)
                ballController.ballSE = true;

            finishCount++;
        }

        if (Input.GetKeyDown(KeyCode.T) && ballCount == 0)
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (!Input.anyKey)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }

        if (ballCount > 2)
        {
            twicaZone2.SetActive(true);
            Debug.Log("TwiceZone2");
            countor.SetActive(false);
        }

        if (ballCount > 4)
        {
            twicaZone3.SetActive(true);
            Debug.Log("TwiceZone3");
        }

        if (ballCount > 200)
        {
            SceneManager.LoadScene("Scene2");
        }
    }
}