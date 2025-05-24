using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec; // public 붙이면 인스펙터에서 확인 가능
    public float speed; // 플레이어 이동 속도
    Rigidbody2D rigid;
    SpriteRenderer spriteRender;
    Animator anima;


    // 초기화는 Awake에서 자주함
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        anima = GetComponent<Animator>();
    }

    // void Update()
    // {
    //     inputVec.x = Input.GetAxisRaw("Horizontal"); // Raw 붙으면 미끄러지듯 움직이는게 없어짐
    //     inputVec.y = Input.GetAxisRaw("Vertical");

    // }

    void FixedUpdate()
    {
        Vector2 nexstVec = inputVec * speed * Time.fixedDeltaTime; // 물리프레임 하나가 소비된시간을 곱해줌
        // //  움직임 구현 방법 3가지

        // // 1. 물리적인 힘을 주는 것
        // rigid.AddForce(inputVec * 10f);

        // // 2. 속도 제어
        // rigid.linearVelocity = inputVec;

        // 3. 위치 이동
        rigid.MovePosition(rigid.position + nexstVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>(); // 노멀라이즈 내장
    }


    // 프레임 종료후 다음 프레임 넘어가기 직전에 실행되는 생명주기 함수
    void LateUpdate()
    {
        anima.SetFloat("Speed", inputVec.magnitude); // 애니메이션 변수 Speed 설정
        if (inputVec.x != 0)
        {
            spriteRender.flipX = inputVec.x < 0; // 왼쪽으로 이동시 스프라이트를 뒤집음
        }
    }
}
