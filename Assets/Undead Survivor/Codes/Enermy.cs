using UnityEngine;

public class Enermy : MonoBehaviour
{
    // ----------------------------------------------------------
    public float speed; // 이동 속도
    public float attackRange; // 공격 범위(체스말로 변경할때 쓸거같아서 일단 추가)
    public float health; // 체력
    public float maxHealth; // 최대 체력
    // -----------------------------------------------------------

    public RuntimeAnimatorController[] animController; // 애니메이션 컨트롤러
    public Rigidbody2D target; // 플레이어의 Rigidbody2D 컴포넌트

    bool isLive; // 플레이어가가 살아있는지

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRender;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        isLive = true; // 활성화되면 살아있음
        health = maxHealth; // 체력 초기화
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animController[data.spriteType]; // 애니메이션 컨트롤러 설정
        speed = data.speed; // 이동 속도 설정
        maxHealth = data.health; // 최대 체력 설정
        health = maxHealth; // 체력 초기화
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) // 총알과 충돌했을 때
        {
            health -= collision.GetComponent<Bullet>().damage; // 체력 감소

            if (health > 0)
            {
                //hit aciton
            }
            else
            {
                // 쥬금
                Dead();

            }

        }
    }
    void Dead()
    {
        gameObject.SetActive(false); // 오브젝트 비활성화
    }

}
