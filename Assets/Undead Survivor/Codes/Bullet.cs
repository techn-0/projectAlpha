using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            rigid.linearVelocity = dir * 15f; // 방향 벡터에 속도를 곱해줌
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
        {
            return;
        }
        per--;

        if (per < 0)
        {
            rigid.linearVelocity = Vector2.zero; // 속도 초기화
            gameObject.SetActive(false);
        }
    }
}
