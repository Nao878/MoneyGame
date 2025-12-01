using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GomiBako : MonoBehaviour
{
    private int count = 10;
    TextMeshProUGUI countText;

    // 指定されたオブジェクトが当たったときにカウントを減らす
    // ここで指定するのは「ぶつかられる側（固定された方）」のオブジェクトです
    public GameObject targetObject;

    // ぶつかってくる（incoming）オブジェクトをタグで指定します。空文字にすると全てのオブジェクトを対象にします。
    // 例: "Enemy"
    public string incomingTag = "Enemy";

    void Start()
    {
        countText = GetComponent<TextMeshProUGUI>();
        countText.text = count.ToString("F0") + "体";

        // targetObject が指定されている場合は、そのオブジェクトにリレーコンポーネントを追加して
        // そのリレー経由で当たり判定を受け取る
        if (targetObject != null)
        {
            var relay = targetObject.GetComponent<TriggerRelay>();
            if (relay == null)
            {
                relay = targetObject.AddComponent<TriggerRelay>();
            }
            relay.owner = this;
        }
    }

    // このオブジェクトに対するトリガー（targetObject が未指定の場合のみ扱う）
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetObject == null)
        {
            // incomingTag が空であれば従来どおりどのオブジェクトでも減らす
            if (string.IsNullOrEmpty(incomingTag) || collision.CompareTag(incomingTag))
            {
                count--;
                countText.text = count.ToString("F0") + "体";

                // 当たってきたオブジェクトを削除
                Destroy(collision.gameObject);
            }
        }
    }

    // Relay から呼ばれるメソッド。targetObject に何かが当たったときに呼ばれる
    public void OnRelayedTrigger(Collider2D incoming)
    {
        // incomingTag が空であれば全対象。そうでなければタグで判定する
        if (string.IsNullOrEmpty(incomingTag) || incoming.CompareTag(incomingTag))
        {
            count--;
            countText.text = count.ToString("F0") + "体";

            // 当たってきたオブジェクトを削除
            Destroy(incoming.gameObject);
        }
    }

    void Update()
    {
        if (count <= 0)
        {
            SceneManager.LoadScene("ClearScene");
        }
    }
}
