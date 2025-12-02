using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 20.0f;
    public float horizontalSpeed = 20.0f;
    private float horizontalDirection = 0f;
    private float directionChangeTimer = 0f;
    private float directionChangeInterval = 1.0f;

    private Collider2D targetCollider; // �����擾
    private EnemyGenerator generator; // EnemyGenerator�Q��
    private Rigidbody2D rb; // �����ړ��p

    void Start()
    {
        // EnemyGenerator��eCanvas����擾
        generator = GetComponentInParent<EnemyGenerator>();
        // DeleteZone��Collider2D�������擾
        GameObject deleteZoneObj = GameObject.Find("DeleteZone");
        if (deleteZoneObj != null)
        {
            targetCollider = deleteZoneObj.GetComponent<Collider2D>();
        }

        // Rigidbody2D���擾�i�Ȃ���Γ��I�ɒǉ��j�B�����x�[�X�̈ړ��ɐ؂�ւ���B
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0f; // �d�͖���
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // �����ړ��ł��ђʂ�}��
        rb.freezeRotation = true;
    }

    void Update()
    {
        // 1�b���Ƃɍ��E�����������_���ύX
        directionChangeTimer += Time.deltaTime;
        if (directionChangeTimer >= directionChangeInterval)
        {
            horizontalDirection = Random.Range(-1, 2); // -1, 0, 1
            directionChangeTimer = 0f;
        }
    }

    void FixedUpdate()
    {
        // ���������ňړ��iTranslate�ł͕����Փ˂����蔲���邱�Ƃ�����j
        if (rb != null)
        {
            Vector2 velocity = new Vector2(horizontalDirection * horizontalSpeed, moveSpeed);
            rb.linearVelocity = velocity;
        }
        else
        {
            // �߂��̈��S��i�O�̂��߁j
            Vector2 move = new Vector2(horizontalDirection * horizontalSpeed, moveSpeed);
            transform.Translate(move * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (targetCollider != null && other == targetCollider)
        {
            // EnemyGenerator�̃J�E���g�����炷
            if (generator != null)
            {
                generator.SetMaxEnemyCount(generator.maxEnemyCount - 1);
            }
            Destroy(gameObject);
        }
    }
}
