using UnityEngine;

public class Enermy : MonoBehaviour
{
    public float speed; // 이동 속도
    public Rigidbody2D target; // 플레이어의 Rigidbody2D 컴포넌트

    bool isLive = true; // 플레이어가가 살아있는지

    Rigidbody2D rigid;
    SpriteRenderer spriteRender;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (!isLive) return; // 플레이어가 죽었으면 이동하지 않음

        Vector2 dirVec = target.position - rigid.position; // 플레이어와의 방향 벡터
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 이동할 위치 계산
        rigid.MovePosition(rigid.position + nextVec); // 위치 이동
        rigid.linearVelocity = Vector2.zero; // 물리적인 힘을 주지 않음
    }

    void LateUpdate()
    {
        if (!isLive) return; // 플레이어가 죽었으면 실행하지 않음
        spriteRender.flipX = target.position.x < rigid.position.x; // 플레이어 위치에 따라 스프라이트 뒤집기
    }
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>(); // 플레이어의 Rigidbody2D 컴포넌트 할당
    }
}
