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
                transform.Rotate(Vector3.back * speed * Time.deltaTime); // 무기 회전
                break;
            default:
                break;
        }

        // test
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바 키를 누르면
        {
            LevelUp(20, 5); // 레벨업
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage += damage; // 데미지 증가
        this.count += count; // 개수 증가

        if (id == 0)
            Placement(); // 무기 ID가 0일 때 배치 함수 호출
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                // 무기 ID가 0일 때의 초기화 로직
                speed = 150;
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
            Transform bullet;
            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i); // 기존 자식 오브젝트 재사용
            }
            else
            {
                bullet = GameManager.instance.pool.GetObject(prefabId).transform.transform; // 새로운 자식 오브젝트 생성
                bullet.parent = transform; // 부모 설정
            }

            // 위치와 회전 초기화
            bullet.localPosition = Vector3.zero; // 위치 초기화
            bullet.localRotation = Quaternion.identity; // 회전 초기화

            Vector3 rotVec = Vector3.forward * 360f * i / count; 
            bullet.Rotate(rotVec); // 회전 설정
            bullet.Translate(bullet.up * 1.5f, Space.World); // 위치 설정


            bullet.GetComponent<Bullet>().Init(damage, -1); // Bullet 컴포넌트 초기화, -1은 무한관통(per), 근접공격
        }
    }
}
