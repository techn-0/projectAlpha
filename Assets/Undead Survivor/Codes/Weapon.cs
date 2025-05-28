using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; // 무기 ID
    public int prefabId; // 프리팹 ID
    public float damage; // 무기 데미지
    public int count; // 개수
    public float speed; // 공격 속도

    float timer; // 타이머
    Player player; // 플레이어 컴포넌트

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

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
                timer += Time.deltaTime; // 타이머 증가
                if (timer >= speed) // 타이머가 속도보다 크거나 같으면
                {
                    timer = 0; // 타이머 초기화
                    Fire(); // 배치 함수 호출
                }
                break;
        }

        // test
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바 키를 누르면
        {
            LevelUp(10, 1); // 레벨업
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
                speed = 0.3f;
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


            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // Bullet 컴포넌트 초기화, -1은 무한관통(per), 근접공격
        }
    }
    void Fire()
    {
        if (!player.scanner.nearestTargets) // 가장 가까운 타겟이 없으면
            return;

        Vector3 targetPos = player.scanner.nearestTargets.position; // 가장 가까운 타겟의 위치
        Vector3 dir = (targetPos - transform.position).normalized; // 방향 벡터 계산

        Transform bullet = GameManager.instance.pool.GetObject(prefabId).transform; // 새로운 총알 오브젝트 생성
        bullet.position = transform.position; // 위치 설정
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // 방향 설정
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
