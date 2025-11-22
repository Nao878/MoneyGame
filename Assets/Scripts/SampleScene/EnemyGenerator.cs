using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab; // Inspectorで設定
    public int maxEnemyCount = 10; // デフォルト召喚数
    public float spawnInterval = 1.0f; // 生成間隔
    public float spawnY = -4.5f; // 画面下部Y座標
    public float minX = -7.0f; // X座標範囲
    public float maxX = 7.0f;
    public Transform canvasTransform; // CanvasのTransform参照

    private int spawnedCount = 0;
    private float timer = 0f;

    void Update()
    {
        if (spawnedCount >= maxEnemyCount) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            float x = Random.Range(minX, maxX);
            Vector2 spawnPos = new Vector2(x, spawnY);
            GameObject enemy = Instantiate(enemyPrefab, canvasTransform);
            var rectTransform = enemy.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = spawnPos;
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0f); // Z座標を0に固定
            }
            spawnedCount++;
            timer = 0f;
        }
    }

    // 他スクリプトから召喚数を変更する場合用
    public void SetMaxEnemyCount(int count)
    {
        maxEnemyCount = count;
    }
}
