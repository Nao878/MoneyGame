using UnityEngine;

// targetObject にアタッチして、当たり判定をオーナーに中継するためのコンポーネント
public class TriggerRelay : MonoBehaviour
{
    public GomiBako owner;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (owner != null)
        {
            owner.OnRelayedTrigger(collision);
        }
    }
}
