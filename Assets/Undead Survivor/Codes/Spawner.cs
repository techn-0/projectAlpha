using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPoint; // 오브젝트를 생성할 위치

    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>()[0]; // 자식 오브젝트 중 첫번째를 생성 위치로 설정
    }
    void Update()
    {
        timer += Time.deltaTime; // 타이머 증가

        if (timer >= 1f) // 1초마다
        {
            Spawn(); // 오브젝트 생성
            timer = 0f; // 타이머 초기화
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.GetObject(Random.Range(0, 2));
        enemy.transform.position = spawnPoint.GetChild(Random.Range(0, spawnPoint.childCount)).position; // 랜덤한 자식 오브젝트 위치에 생성
    }
}
