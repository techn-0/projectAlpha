using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec; // public 붙이면 인스펙터에서 확인 가능

    Rigidbody2D rigid;


    // 초기화는 Awake에서 자주함
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");

    }

    void FixedUpdate()
    {
        //  움직임 구현 방법 3가지

        // 1. 물리적인 힘을 주는 것
        rigid.AddForce(inputVec * 10f);

        // 2. 속도 제어
        rigid.linearVelocity = inputVec;

        // 3. 위치 이동
        rigid.MovePosition(rigid.position + inputVec);
    }
}
