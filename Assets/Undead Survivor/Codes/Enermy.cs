using System.Collections;
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
    Collider2D collider;
    Animator anim;
    SpriteRenderer spriteRender;
    WaitForFixedUpdate wait;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }
    void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return; // 플레이어가 죽었거나 몹이 맞았을 때 실행하지 않음

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

        isLive = true;
        collider.enabled = true;
        rigid.simulated = true;
        spriteRender.sortingOrder = 2;
        anim.SetBool("Dead", false);

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
            StartCoroutine(KnockBack()); // 넉백 효과 적용

            if (health > 0)
            {
                anim.SetTrigger("Hit"); // 맞았을 때 애니메이션 트리거
            }
            else
            {
                // 쥬금
                isLive = false; // 살아있지 않음
                collider.enabled = false; // 충돌체 비활성화
                rigid.simulated = false; // 물리 시뮬레이션 비활성화
                spriteRender.sortingOrder = 1; // 스프라이트 정렬 순서 초기화
                anim.SetBool("Dead", true); // 죽었을 때 애니메이션 트리거
            }

        }
    }
    IEnumerator KnockBack()
    {
        yield return wait; // 물리 프레임 딜레이
        Vector3 playerPos = GameManager.instance.player.transform.position; // 플레이어 위치
        Vector3 dirVec = (transform.position - playerPos).normalized; // 플레이어와의 방향 벡터
        rigid.AddForce(dirVec * 3, ForceMode2D.Impulse); // 넉백 힘 적용
    }
    void Dead()
    {
        gameObject.SetActive(false); // 오브젝트 비활성화
    }

}
