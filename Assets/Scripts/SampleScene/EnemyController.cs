using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 20.0f;
    public float horizontalSpeed = 20.0f;
    private float horizontalDirection = 0f;
    private float directionChangeTimer = 0f;
    private float directionChangeInterval = 1.0f;

    private Collider2D targetCollider; // 自動取得
    private EnemyGenerator generator; // EnemyGenerator参照

    void Start()
    {
        // EnemyGeneratorを親Canvasから取得
        generator = GetComponentInParent<EnemyGenerator>();
        // DeleteZoneのCollider2Dを自動取得
        GameObject deleteZoneObj = GameObject.Find("DeleteZone");
        if (deleteZoneObj != null)
        {
            targetCollider = deleteZoneObj.GetComponent<Collider2D>();
        }
    }

    void Update()
    {
        // 1秒ごとに左右方向をランダム変更
        directionChangeTimer += Time.deltaTime;
        if (directionChangeTimer >= directionChangeInterval)
        {
            horizontalDirection = Random.Range(-1, 2); // -1, 0, 1
            directionChangeTimer = 0f;
        }

        // 上方向＋左右方向に移動
        Vector2 move = new Vector2(horizontalDirection * horizontalSpeed, moveSpeed);
        transform.Translate(move * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (targetCollider != null && other == targetCollider)
        {
            // EnemyGeneratorのカウントを減らす
            if (generator != null)
            {
                generator.SetMaxEnemyCount(generator.maxEnemyCount - 1);
            }
            Destroy(gameObject);
        }
    }
}
