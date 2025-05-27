using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; // 무기 ID
    public int prefabId; // 프리팹 ID
    public float damage; // 무기 데미지
    public int count; // 개수
    public float speed; // 공격 속도

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime); // 무기 회전
                break;
            default:
                break;
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                // 무기 ID가 0일 때의 초기화 로직
                speed = -150;
                Placement(); // 배치 함수 호출
                break;
            default:
                break;
        }
    }

    void Placement() // 배치
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet = GameManager.instance.pool.GetObject(prefabId).transform; // 풀에서 오브젝트 가져오기
            bullet.parent = transform; // 현재 오브젝트의 자식으로 설정

            Vector3 rotVec = Vector3.forward * 360f * i / count; 
            bullet.Rotate(rotVec); // 회전 설정
            bullet.Translate(bullet.up * 1.5f, Space.World); // 위치 설정


            bullet.GetComponent<Bullet>().Init(damage, -1); // Bullet 컴포넌트 초기화, -1은 무한관통(per), 근접공격
        }
    }
}
