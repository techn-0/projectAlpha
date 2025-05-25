using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPoint; // 오브젝트를 생성할 위치
    public SpawnData[] spawnData;

    int level;
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>()[0]; // 자식 오브젝트 중 첫번째를 생성 위치로 설정
    }
    void Update()
    {
        timer += Time.deltaTime; // 타이머 증가
        level = Mathf.Min((int)(GameManager.instance.gameTime / 10f), spawnData.Length - 1); // 게임 시간에 따라 레벨 결정


        if (timer >= spawnData[level].SpawnTime)
        {
            Spawn(); // 오브젝트 생성
            timer = 0f; // 타이머 초기화
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.GetObject(0);
        enemy.transform.position = spawnPoint.GetChild(Random.Range(0, spawnPoint.childCount)).position; // 랜덤한 자식 오브젝트 위치에 생성
        enemy.GetComponent<Enermy>().Init(spawnData[level]); // Enermy 컴포넌트 초기화
    }
}


[System.Serializable]
public class SpawnData
{
    public float SpawnTime; // 생성 시간
    public int spriteType; // 스프라이트 타입
    public int health; // 체력
    public float speed; // 속도
}
