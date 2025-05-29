using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 패턴, 스테틱은 인스펙터 표시X
    [Header("# 게임 컨트롤롤")]
    public float gameTime; // 게임 시간
    public float maxGameTime = 2 * 10f; // 최대 게임 시간
    [Header("# 플레이어 정보")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; // 레벨업에 필요한 경험치
    [Header("# 게임 오브젝트")]
    public PollManager pool;
    public Player player;

    void Awake()
    {
        instance = this; // 싱글톤 패턴
    }

    void Update()
    {
        gameTime += Time.deltaTime; // 타이머 증가

        if (gameTime >= maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
    public void GetExp()
    {
        exp++; // 경험치 증가
        if (exp >= nextExp[level]) // 현재 레벨의 경험치가 다음 레벨의 경험치 이상이면
        {
            level++; // 레벨 증가
            exp = 0; // 경험치 초기화
        }
    }
}
